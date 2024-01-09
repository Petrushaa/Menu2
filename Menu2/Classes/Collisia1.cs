using Menu2.Classes;
using Menu2.Menu;
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
using System.Windows.Threading;

namespace Menu2
{
    internal class Collisia
    {
        public Image object1;//Объект для которого задается коллизия
        public Player player;
        public Canvas canvas;
        public List<Rectangle> elementsCopy;
        Random rand;
        private List<BitmapImage> rightImages;
        private List<BitmapImage> downImages;
        private List<BitmapImage> upImages;
        private List<BitmapImage> leftImages;
        // Индексы для отслеживания текущего изображения в каждой коллекции
        int rightIndex = 0;
        int downIndex = 0;
        int upIndex = 0;
        int leftIndex = 0;

        public Collisia(Canvas canvas, Image object1, Player player) //конструктор
        {
            //Конструктор присваиваем все переменные
            this.object1 = object1;
            this.player = player;
            this.canvas = canvas;

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
        public void Lift()
        {
            foreach (var x in canvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "Lift")
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect LiftHB = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.RenderSize.Width, x.RenderSize.Height);//Создаем хитбокс коллизии, т.е. нашего ректа
                    if (PlayerHB.IntersectsWith(LiftHB))
                    {
                        Lift lift = new Lift();
                        lift.Show();
                    }
                }
            }
        }
        public void Collide(string Dir) // Сам метод коллизии
        {
            foreach (Rectangle x in elementsCopy) 
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
                            player.SpeedX = 0;//Тут мы его передвигаем обратно по кординате х
                              //Обнуление переменных _SpeedX и _SpeedY в методе Collide происходит для того, чтобы предотвратить дальнейшее движение
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
            }
        }
    }
}

