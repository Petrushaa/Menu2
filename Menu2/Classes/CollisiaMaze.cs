using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;

namespace Menu2.Classes
{
    internal class CollisiaMaze
    {
        public Image object1;//Объект для которого задается коллизия
        public PlayerMaze player;
        public Canvas canvas;
        public List<UIElement> elementsCopy;
        public List<mob> mobs;
        Random rand;
        private List<BitmapImage> rightImages;
        private List<BitmapImage> downImages;
        private List<BitmapImage> upImages;
        private List<BitmapImage> leftImages;
        private List<Bullet> bullets;
        // Индексы для отслеживания текущего изображения в каждой коллекции
        int rightIndex = 0;
        int downIndex = 0;
        int upIndex = 0;
        int leftIndex = 0;

        public CollisiaMaze(Canvas canvas, Image object1, PlayerMaze player, List<Bullet> bullets, Random rand = null) //конструктор
        {
            //Конструктор присваиваем все переменные
            this.object1 = object1;
            this.player = player;
            this.canvas = canvas;
            this.rand = rand;
            this.bullets = bullets;

            rightImages = LoadImages("right");
            downImages = LoadImages("down");
            upImages = LoadImages("up");
            leftImages = LoadImages("left");

        }
        private List<BitmapImage> LoadImages(string direction)
        {
            List<BitmapImage> images = new List<BitmapImage>();

            for (int i = 1; i <= 3; i++) // Предположим, у вас есть 3 изображения для каждого направления
            {
                string path = $"z{direction}{i}.png"; // Предположим, что ваши изображения названы zRight1.png, zRight2.png, zRight3.png и т.д.
                BitmapImage image = new BitmapImage(new Uri(path, UriKind.Relative));
                images.Add(image);
            }

            return images;
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
                        ((Image)x).Source = rightImages[rightIndex];
                        rightIndex = (rightIndex + 1) % rightImages.Count;
                    }
                    if (Canvas.GetTop(x) > Canvas.GetTop(object1))
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) - mob.zombieSpeed);
                        ((Image)x).Source = rightImages[downIndex];
                        downIndex = (downIndex + 1) % rightImages.Count;
                    }
                    if (Canvas.GetTop(x) < Canvas.GetTop(object1))
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + mob.zombieSpeed);
                        ((Image)x).Source = upImages[upIndex];
                        upIndex = (upIndex + 1) % upImages.Count;
                    }
                    if (Canvas.GetLeft(x) > Canvas.GetLeft(object1))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - mob.zombieSpeed);
                        ((Image)x).Source = leftImages[leftIndex];
                        leftIndex = (leftIndex + 1) % leftImages.Count;
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
                                mob newMob = new mob(canvas, rand);
                                mobs.Add(newMob);
                                newMob.makeGrivers();
                            }
                            canvas.Children.Remove(x);
                        }
                    }
                }
                foreach (Bullet bull in bullets.ToList()) //можно поменять цикл и иф местами
                {
                    if (x is Rectangle wall && (string)wall.Tag == "Collide")
                    {
                        Image bullet = bull.bullet;
                        Rect bulHB = new Rect(Canvas.GetLeft(bullet), Canvas.GetTop(bullet), bullet.Width, bullet.Height); ;//создаем хитбокс объекта (персонажа) 
                        Rect wallHB = new Rect(Canvas.GetLeft(wall), Canvas.GetTop(wall), wall.Width, wall.Height);
                        if (bulHB.IntersectsWith(wallHB))
                        {
                            bullet.Source = null;
                            canvas.Children.Remove(bullet);
                            bullets.Remove(bull);
                        }
                    }
                }

            }
        }
    }
}
