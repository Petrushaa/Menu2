using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Menu2.Classes
{
    internal class PlayerMaze
    {
        public bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        public float SpeedX, SpeedY, Friction;
        public Image Character;
        public static string facing = "up";
        private Canvas canvas;
        public static int ammo = 10;
        public CollisiaMaze collisia;
        public int Health = 100;
        public float Speed = 2f;
        List<BitmapImage> animations = new List<BitmapImage>();
        public DispatcherTimer animTimer = new DispatcherTimer();
        int steps = 0;
        public PlayerMaze(Canvas canvas, Image Character, CollisiaMaze collisia, float SpeedY = 0, float SpeedX = 0, bool UpKeyPressed = false, bool DownKeyPressed = false, bool LeftKeyPressed = false, bool RightKeyPressed = false, float Friction = 0.77f)
        {
            this.SpeedY = SpeedY;
            this.SpeedX = SpeedX;
            this.Friction = Friction;
            this.UpKeyPressed = UpKeyPressed;
            this.DownKeyPressed = DownKeyPressed;
            this.LeftKeyPressed = LeftKeyPressed;
            this.RightKeyPressed = RightKeyPressed;
            this.Character = Character;
            this.canvas = canvas;
            this.collisia = collisia;
            animTimer.Interval = TimeSpan.FromMilliseconds(100);
            animTimer.Tick += heroTick;
            animTimer.Start();
            FillList();
        }
        public void AnimateHero(int start, int end)
        {
            steps++;

            if (steps > end || steps < start)
            {
                steps = start;
            }
            Character.Source = animations[steps];
        }
        public void FillList()
        {
            animations.Add(new BitmapImage(new Uri("hero1.PNG", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("hero2.PNG", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("hero3.PNG", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("hero4.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("heroBack1.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("heroBack2.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("stayBack.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("stayForward.png", UriKind.Relative)));
        }

        private void heroTick(object sender, EventArgs e)
        {
            if (UpKeyPressed)
            {
                AnimateHero(4, 5);
            }

            if (DownKeyPressed)
            {
                AnimateHero(0, 3);
            }
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
            Canvas.SetLeft(canvas, Canvas.GetLeft(canvas) - SpeedX);
            Canvas.SetLeft(Character, Canvas.GetLeft(Character) + SpeedX);
            collisia.Collide("x");
            Canvas.SetTop(canvas, Canvas.GetTop(canvas) + SpeedY);
            Canvas.SetTop(Character, Canvas.GetTop(Character) - SpeedY);
            collisia.Collide("y");
        }
        public void KeyboardUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.W )
            {
                UpKeyPressed = false;
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = false;
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = false;
            }
        }
        public void KeyBoardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = true;
                facing = "up";
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = true;
                facing = "down";
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = true;
                facing = "left";
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = true;
                facing = "right";
            }
        }
    }
}
