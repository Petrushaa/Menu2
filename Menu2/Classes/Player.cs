using Menu2.Classes;
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
        public bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        public float SpeedX, SpeedY, Friction, Speed;
        public Image Character;
        public Collisia collisia;
        public Player(Canvas canvas, Image Character, Collisia collisia, float SpeedY = 0, float SpeedX = 0, bool UpKeyPressed = false, bool DownKeyPressed = false, bool LeftKeyPressed = false, bool RightKeyPressed = false, float Friction = 0.77f, float Speed = 2f)
        {
            this.Speed = Speed;
            this.SpeedY = SpeedY;
            this.SpeedX = SpeedX;
            this.Friction = Friction;
            this.UpKeyPressed = UpKeyPressed;
            this.DownKeyPressed = DownKeyPressed;
            this.LeftKeyPressed = LeftKeyPressed;
            this.RightKeyPressed = RightKeyPressed;
            this.Character = Character;
            this.collisia = collisia;
        }
        public void Move()
        {
            if (UpKeyPressed)
            {
                SpeedY += Speed;
            }
            if (RightKeyPressed)
            {
                SpeedX += Speed;
            }
            if (LeftKeyPressed)
            {
                SpeedX -= Speed;
            }
            if (DownKeyPressed)
            {
                SpeedY -= Speed;
            }
            SpeedX = SpeedX * Friction;
            SpeedY = SpeedY * Friction;
            Canvas.SetLeft(Character, Canvas.GetLeft(Character) + SpeedX);
            collisia.Collide("x");
            Canvas.SetTop(Character, Canvas.GetTop(Character) - SpeedY);
            collisia.Collide("y");
        }
        public void KeyboardUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.W || e.Key == Key.Up)
            {
                UpKeyPressed = false;
            }
            if (e.Key == Key.S || e.Key == Key.Down)
            {
                DownKeyPressed = false;
            }
            if (e.Key == Key.A || e.Key == Key.Left)
            {
                LeftKeyPressed = false;
            }
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                RightKeyPressed = false;
            }
        }
        public void KeyBoardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.Up)
            {
                UpKeyPressed = true;
            }
            if (e.Key == Key.S || e.Key == Key.Down)
            {
                DownKeyPressed = true;
            }
            if (e.Key == Key.A || e.Key == Key.Left)
            {
                LeftKeyPressed = true;
                Character.Source = new BitmapImage(new Uri("characterLeft.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                RightKeyPressed = true;
                Character.Source = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
