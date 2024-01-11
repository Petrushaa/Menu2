using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;

namespace Menu2.Classes
{
    internal class CollisiaMaze
    {
        public Image object1;//Объект для которого задается коллизия
        public PlayerMaze player;
        public Canvas canvas;
        public List<UIElement> elementsCopy;
        public List<mob> mobs;
        private List<Bullet> bullets;
        // Индексы для отслеживания текущего изображения в каждой коллекции
        int rightIndex = 0;
        int leftIndex = 0;
        Label lbAmmo;

        public CollisiaMaze(Canvas canvas, Image object1, PlayerMaze player, List<Bullet> bullets, Label lbAmmo) //конструктор
        {
            //Конструктор присваиваем все переменные
            this.object1 = object1;
            this.player = player;
            this.canvas = canvas;
            this.bullets = bullets;
            this.lbAmmo = lbAmmo;


        }
        public void Code()
        {
            foreach (UIElement x in elementsCopy)
            {
                if (x is Image code && (string)code.Tag == "code")
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);
                    Rect ToCollide = new Rect(Canvas.GetLeft(code), Canvas.GetTop(code), code.RenderSize.Width, code.RenderSize.Height);
                    if (PlayerHB.IntersectsWith(ToCollide))//Проверяем пересекаются ли хитбоксы объекта (персонажа) с нашей коллизией
                    {
                        GamePlay.countKeys += 1;
                        code.Source = null;
                        canvas.Children.Remove(x);
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
                            Canvas.SetLeft(object1, Canvas.GetLeft(object1) - player.SpeedX);
                            Canvas.SetLeft(canvas, Canvas.GetLeft(canvas) + player.SpeedX); //Тут мы его передвигаем обратно по кординате х
                            player.SpeedX = 0;  //Обнуление переменных _SpeedX и _SpeedY в методе Collide происходит для того, чтобы предотвратить дальнейшее движение
                                                //объекта в направлении, в котором была обнаружена коллизия. Если вы не обнулите эти переменные, то объект продолжит
                                                //двигаться в направлении, в котором была обнаружена коллизия, что может привести к нежелательным последствиям .
                        }
                        else //Если мы передали, что передвинулись по кординате у , то
                        {
                            Canvas.SetTop(object1, Canvas.GetTop(object1) + player.SpeedY);
                            Canvas.SetTop(canvas, Canvas.GetTop(canvas) - player.SpeedY);//Тут мы его передвигаем обратно по кординате у
                            player.SpeedY = 0;
                        }
                    }
                }
                if (x is Image trap && (string)trap.Tag =="trap")
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect ToCollide = new Rect(Canvas.GetLeft(trap), Canvas.GetTop(trap), trap.Width, trap.Height);//Создаем хитбокс коллизии, т.е. нашего ректа
                    if (PlayerHB.IntersectsWith(ToCollide))//Проверяем пересекаются ли хитбоксы объекта (персонажа) с нашей коллизией
                    {
                        player.Health -= 1;
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
                        PlayerMaze.ammo += 5;
                        lbAmmo.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    }
                }
                //Убийство гриверов
                foreach (mob mobe in mobs.ToList()) // Используйте ToList(), чтобы избежать ошибки изменения коллекции во время итерации
                {
                    Image griver = mobe.griver;
                    if (x is Image bul && (string)bul.Tag == "bullet")
                    {
                        Rect bul2 = new Rect(Canvas.GetLeft(bul), Canvas.GetTop(bul), bul.Width, bul.Height);
                        Rect zomb2 = new Rect(Canvas.GetLeft(griver), Canvas.GetTop(griver), griver.Width, griver.Height);
                        if (bul2.IntersectsWith(zomb2))
                        {
                            mobe.TakeDamage(20); // Гривер получает 20 урона
                            if (mobe.Health <= 0)
                            {
                                canvas.Children.Remove(griver);
                                mobs.Remove(mobe); // Удалите этого Гривера из списка mobs
                                //mob newMob = new mob(canvas, rand);
                                //mobs.Add(newMob);
                                //newMob.makeGrivers();
                            }
                            canvas.Children.Remove(x);
                            break;
                        }
                    }
                }
                 if (x is Rectangle wall && (string)wall.Tag == "Collide")//можно поменять цикл и иф местами
                 {
                    foreach (Bullet bull in bullets.ToList()) 
                    {
                        Image bullet = bull.bullet;
                        Rect bulHB = new Rect(Canvas.GetLeft(bullet), Canvas.GetTop(bullet), bullet.Width, bullet.Height); ;//создаем хитбокс объекта (персонажа) 
                        Rect wallHB = new Rect(Canvas.GetLeft(wall), Canvas.GetTop(wall), wall.Width, wall.Height);
                        if (bulHB.IntersectsWith(wallHB))
                        {
                            bullet.Source = null;
                            canvas.Children.Remove(bullet);
                            bullets.Remove(bull);
                            break;
                        }
                    }
                 }
            }
            //ии гриверов  
            foreach (mob mobe in mobs)
            {
                Image mobb = mobe.griver;
                double playerCenterX = Canvas.GetLeft(object1) + object1.Width / 2;
                double playerCenterY = Canvas.GetTop(object1) + object1.Height / 2;
                double mobCenterX = Canvas.GetLeft(mobb) + mobb.Width / 2;
                double mobCenterY = Canvas.GetTop(mobb) + mobb.Height / 2;

                if (mobCenterX < playerCenterX)
                {
                    Canvas.SetLeft(mobb, Canvas.GetLeft(mobb) + mob.griverSpeed);
                    mobe.direction = "Right";
                }
                else if (mobCenterX > playerCenterX)
                {
                    Canvas.SetLeft(mobb, Canvas.GetLeft(mobb) - mob.griverSpeed);
                    mobe.direction = "Left";
                }

                if (mobCenterY < playerCenterY)
                {
                    Canvas.SetTop(mobb, Canvas.GetTop(mobb) + mob.griverSpeed);
                }
                else if (mobCenterY > playerCenterY)
                {
                    Canvas.SetTop(mobb, Canvas.GetTop(mobb) - mob.griverSpeed);
                }

                Rect PlayerHB = new Rect(playerCenterX, playerCenterY, object1.Width, object1.Height);
                Rect GriverHB = new Rect(mobCenterX, mobCenterY, mobb.Width, mobb.Height/1.5);

                if (PlayerHB.IntersectsWith(GriverHB))
                {
                    player.Health -= 1;
                }
            }
        }
    }
}
