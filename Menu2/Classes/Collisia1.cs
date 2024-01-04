using Menu2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Menu2
{
    internal class Collisia
    {
        public Image object1;//Объект для которого задается коллизия
        public Player player;
        public Canvas canvas;
        public List<UIElement> elementsCopy;
        public List<mob> mobs;
        Random rand;
        public Collisia(Canvas canvas, Image object1, Player player, Random rand = null) //конструктор
        {
            //Конструктор присваиваем все переменные
            this.object1 = object1;
            this.player = player;
            this.canvas = canvas;
            this.rand = rand;
        }
        public void griversCollide()
        {
            foreach (UIElement x in elementsCopy)
            {
                //ии гриверов
                if (x is Image imageG && (string)imageG.Tag == "griver")
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect GriverHB = new Rect(Canvas.GetLeft(imageG), Canvas.GetTop(imageG), imageG.Width, imageG.Height);
                    if (PlayerHB.IntersectsWith(GriverHB))
                    {
                        player.Health -= 1;
                    }
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(object1))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + mob.zombieSpeed);
                        ((Image)x).Source = new BitmapImage(new Uri("zRight.png", UriKind.RelativeOrAbsolute));
                    }
                    if (Canvas.GetTop(x) > Canvas.GetTop(object1))
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) - mob.zombieSpeed);
                        ((Image)x).Source = new BitmapImage(new Uri("zDown.png", UriKind.RelativeOrAbsolute));
                    }
                    if (Canvas.GetTop(x) < Canvas.GetTop(object1))
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + mob.zombieSpeed);
                        ((Image)x).Source = new BitmapImage(new Uri("zUp.png", UriKind.RelativeOrAbsolute));
                    }
                    if (Canvas.GetLeft(x) > Canvas.GetLeft(object1))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - mob.zombieSpeed);
                        ((Image)x).Source = new BitmapImage(new Uri("zLeft.png", UriKind.RelativeOrAbsolute));
                    }
                }

                foreach (mob mobe in mobs.ToList()) // Используйте ToList(), чтобы избежать ошибки изменения коллекции во время итерации
                {
                    Image griver = mobe.griver;
                    if (x is Image bul && (string)bul.Tag == "bullet")
                    {
                        Rect bul2 = new Rect(Canvas.GetLeft(bul), Canvas.GetTop(bul), bul.Width, bul.Height);
                        Rect zomb2 = new Rect(Canvas.GetLeft(griver), Canvas.GetTop(griver), griver.Width, griver.Height);
                        if (bul2.IntersectsWith(zomb2))
                        {
                            mobe.TakeDamage(50); // Гривер получает 50 урона
                            if (mobe.Health <= 0)
                            {
                                canvas.Children.Remove(griver);
                                mobs.Remove(mobe); // Удалите этого Гривера из списка mobs
                                mob newMob = new mob(canvas, rand);
                                mobs.Add(newMob);
                                newMob.makeGrivers();
                            }
                            canvas.Children.Remove(x);
                        }
                    }
                }
            }
        }
        public void Collide(string Dir) // Сам метод коллизии
        {
            foreach (UIElement x in elementsCopy) 
            {
                if (x is Rectangle imagex && (string)imagex.Tag == "Collide") //Если у ректангле тег Коллизии, то 
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect ToCollide = new Rect(Canvas.GetLeft(imagex), Canvas.GetTop(imagex), imagex.RenderSize.Width, imagex.RenderSize.Height);//Создаем хитбокс коллизии, т.е. нашего ректа
                    if (PlayerHB.IntersectsWith(ToCollide))//Проверяем пересекаются ли хитбоксы объекта (персонажа) с нашей коллизией
                    {
                        if (Dir == "x")//Если мы передали, что передвинулись по кординате х, то
                        {
                            Canvas.SetLeft(object1, Canvas.GetLeft(object1) - player.SpeedX); //Тут мы его передвигаем обратно по кординате х
                            player.SpeedX = 0;  //Обнуление переменных _SpeedX и _SpeedY в методе Collide происходит для того, чтобы предотвратить дальнейшее движение
                                                //объекта в направлении, в котором была обнаружена коллизия. Если вы не обнулите эти переменные, то объект продолжит
                                                //двигаться в направлении, в котором была обнаружена коллизия, что может привести к нежелательным последствиям .
                        }
                        else ////Если мы передали, что передвинулись по кординате у , то
                        {
                            Canvas.SetTop(object1, Canvas.GetTop(object1) + player.SpeedY);//Тут мы его передвигаем обратно по кординате у
                            player.SpeedY = 0;
                        }
                    }
                }
                if (x is Image imagey && (string)imagey.Tag == "ammo") //Если у ректангле тег Коллизии, то 
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect ToCollide = new Rect(Canvas.GetLeft(imagey), Canvas.GetTop(imagey), imagey.Width, imagey.Height);//Создаем хитбокс коллизии, т.е. нашего ректа
                    if (PlayerHB.IntersectsWith(ToCollide))//Проверяем пересекаются ли хитбоксы объекта (персонажа) с нашей коллизией
                    {
                        canvas.Children.Remove(imagey);
                        imagey.Source = null;
                        Player.ammo += 5;
                    }
                }
            }
        }
    }
}

