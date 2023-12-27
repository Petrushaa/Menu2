using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Menu2
{
    internal class Collisia
    {
        //public Canvas _Canvas;//Канвас пока не используется, вместо него юзаем AllCollisia
        public Rectangle _Object;//Объект для которого задается коллизия
        public float _SpeedX;//Кордината по х
        public float _SpeedY;//Кордината по у
        private Player _Player;
        Canvas canvas;
        public List<UIElement> elementsCopy;

        public Collisia(List<UIElement> elementsCopy, Canvas canvas, Rectangle object1, float SpeedX, float SpeedY, Player player) //конструктор
        {
            //_Canvas = Canvas; //Конструктор присваиваем все переменные
            _Object = object1; 
            _SpeedX = SpeedX;
            _SpeedY = SpeedY;
            _Player = player;
            this.canvas = canvas;
            this.elementsCopy = elementsCopy;
        }
        public IEnumerable<Rectangle> AllCollisia { get; set; } //Все объекты коллизии
        public void Collide(string Dir) // Сам метод коллизии
        {
            foreach (UIElement x in elementsCopy) //Проверяем КАЖДЫЙ ректангле, в списке AllCollisia
            {
                if (x is Rectangle imagex && (string)imagex.Tag == "Collide") //Если у ректангле тег Коллизии, то 
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(_Object), Canvas.GetTop(_Object), _Object.Width, _Object.Height);//создаем хитбокс объекта (персонажа) 
                    Rect ToCollide = new Rect(Canvas.GetLeft(imagex), Canvas.GetTop(imagex), imagex.RenderSize.Width, imagex.RenderSize.Height);//Создаем хитбокс коллизии, т.е. нашего ректа

                    if (PlayerHB.IntersectsWith(ToCollide))//Проверяем пересекаются ли хитбоксы объекта (персонажа) с нашей коллизией
                    {
                        if (Dir == "x")//Если мы передали, что передвинулись по кординате х, то
                        {
                            Canvas.SetLeft(_Object, Canvas.GetLeft(_Object) - _SpeedX); //Тут мы его передвигаем обратно по кординате х
                            _Player._SpeedX = 0;  //Обнуление переменных _SpeedX и _SpeedY в методе Collide происходит для того, чтобы предотвратить дальнейшее движение
                                                 //объекта в направлении, в котором была обнаружена коллизия. Если вы не обнулите эти переменные, то объект продолжит
                                                 //двигаться в направлении, в котором была обнаружена коллизия, что может привести к нежелательным последствиям .
                        }
                        else ////Если мы передали, что передвинулись по кординате у , то
                        {
                            Canvas.SetTop(_Object, Canvas.GetTop(_Object) + _SpeedY);//Тут мы его передвигаем обратно по кординате у
                            _Player._SpeedY = 0;
                        }
                    }
                }
                if (x is Rectangle imagey && (string)imagey.Tag == "ammo") //Если у ректангле тег Коллизии, то 
                {
                    Rect PlayerHB = new Rect(Canvas.GetLeft(_Object), Canvas.GetTop(_Object), _Object.Width, _Object.Height);//создаем хитбокс объекта (персонажа) 
                    Rect ToCollide = new Rect(Canvas.GetLeft(imagey), Canvas.GetTop(imagey), imagey.Width, imagey.Height);//Создаем хитбокс коллизии, т.е. нашего ректа

                    if (PlayerHB.IntersectsWith(ToCollide))//Проверяем пересекаются ли хитбоксы объекта (персонажа) с нашей коллизией
                    {
                        canvas.Children.Remove(imagey);
                        imagey.Fill = null;
                        Player.ammo += 5;
                       
                    }
                }
            }
        }
    }
}
