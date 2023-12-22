using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Menu2
{
    internal class Player
    {

        private bool _UpKeyPressed, _DownKeyPressed, _LeftKeyPressed, _RightKeyPressed;
        public float _SpeedX, _SpeedY, _Friction, _Speed;
        private Rectangle _Character;
        ImageBrush _ObjectImage;
        public Player(ImageBrush ObjectImage, Rectangle Character, float SpeedY = 0, float SpeedX = 0, bool UpKeyPressed = false, bool DownKeyPressed = false, bool LeftKeyPressed = false, bool RightKeyPressed = false, float Friction = 0.77f, float Speed = 2f)
        {
            _Speed = Speed;
            _SpeedY = SpeedY;
            _SpeedX = SpeedX;
            _Friction = Friction;
            _UpKeyPressed = UpKeyPressed;
            _DownKeyPressed = DownKeyPressed;
            _LeftKeyPressed = LeftKeyPressed;
            _RightKeyPressed = RightKeyPressed;
            _Character = Character;
            _ObjectImage = ObjectImage;
        }
        public float X { get; set; }
        public float Y { get; set; }

        public void Move()
        {
            if (_UpKeyPressed)
            {
                _SpeedY += _Speed;
            }
            if (_RightKeyPressed)
            {   
                _SpeedX += _Speed;
            }
            if (_LeftKeyPressed)
            {
                _SpeedX -= _Speed;
            }
            if (_DownKeyPressed)
            {
                _SpeedY -= _Speed;
            }
            _SpeedX = _SpeedX * _Friction;
            _SpeedY = _SpeedY * _Friction;
            X = _SpeedX;
            Y = _SpeedY;

            //Canvas.SetLeft(_Character, Canvas.GetLeft(_Character) + _SpeedX);
            //Collide("x");
            //Canvas.SetTop(_Character, Canvas.GetTop(_Character) - _SpeedY);
            //Collide("y");
        }
        public void KeyboardUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.W || e.Key == Key.Up)
            {
                _UpKeyPressed = false;
            }
            if (e.Key == Key.S || e.Key == Key.Down)
            {
                _DownKeyPressed = false;
            }
            if (e.Key == Key.A || e.Key == Key.Left)
            {
                _LeftKeyPressed = false;
            }
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                _RightKeyPressed = false;
            }
        }

        public void KeyBoardDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.W || e.Key == Key.Up)
            {
                _UpKeyPressed = true;
            }
            if (e.Key == Key.S || e.Key == Key.Down)
            {
                _DownKeyPressed = true;
            }
            if (e.Key == Key.A || e.Key == Key.Left)
            {
                _LeftKeyPressed = true;

                _ObjectImage.ImageSource = new BitmapImage(new Uri("characterLeft.png", UriKind.RelativeOrAbsolute));//Устанавливаем свойство ImageSource объекта ImageBrush на новое изображение
            }
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                _RightKeyPressed = true;
                _ObjectImage.ImageSource = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));//Устанавливаем свойство ImageSource объекта ImageBrush на новое изображение
            }
        }
    }
}
