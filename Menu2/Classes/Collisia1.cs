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

        public Collisia(Canvas canvas, Image object1, Player player) //конструктор
        {
            //Конструктор присваиваем все переменные
            this.object1 = object1;
            this.player = player;
            this.canvas = canvas;


        }
        public void Lift(Game game)
        {
            foreach (var x in canvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "Lift")
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(object1), Canvas.GetTop(object1), object1.Width, object1.Height);//создаем хитбокс объекта (персонажа) 
                    Rect LiftHB = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.RenderSize.Width, x.RenderSize.Height);//Создаем хитбокс коллизии, т.е. нашего ректа
                    if (PlayerHB.IntersectsWith(LiftHB))
                    {
                        Lift lift = new Lift(game);
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

