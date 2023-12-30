using Menu2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public mob mobe;

        public Collisia(Canvas canvas, Image object1, Player player, mob mobe = null, List<UIElement> elementsCopy = null) //конструктор
        {
            //Конструктор присваиваем все переменные
            this.object1 = object1; 
            this.player = player;
            this.canvas = canvas;
            this.elementsCopy = elementsCopy;
            this.mobe = mobe;
        }
        public void Collide(string Dir) // Сам метод коллизии
        {
            foreach (UIElement x in elementsCopy) //Проверяем КАЖДЫЙ ректангле, в списке AllCollisia
            {
                if (x is Image imagex && (string)imagex.Tag == "Collide") //Если у ректангле тег Коллизии, то 
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
                if (x is Image imageG && (string)imageG.Tag == "griver")
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect GriverHB = new Rect(Canvas.GetLeft(imageG), Canvas.GetTop(imageG), imageG.Width, imageG.Height);
                    if(PlayerHB.IntersectsWith(GriverHB))
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
                foreach (UIElement j in elementsCopy)
                {
                    if (j is Image bul && (string)bul.Tag == "bullet" && x is Image zomb && (string)zomb.Tag == "griver")
                    {
                        Rect bul2 = new Rect(Canvas.GetLeft(bul), Canvas.GetTop(bul), bul.Width, bul.Height);//создаем хитбокс объекта (персонажа) 
                        Rect zomb2 = new Rect(Canvas.GetLeft(zomb), Canvas.GetTop(zomb), zomb.Width, zomb.Height);
                        if (bul2.IntersectsWith(zomb2))
                        {
                            canvas.Children.Remove(x); //удаляем зомби
                            canvas.Children.Remove(j); //удаляем пулю
                            mobe.makeGrivers();
                        }
                    }
                }
            }
        }
    }
}
