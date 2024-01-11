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
using System.Windows.Threading;

namespace Menu2
{
    internal class Player
    {
        public float SpeedY = 0, SpeedX = 0, Friction = 0.77f, Speed = 2f;
        public bool UpKeyPressed = false, DownKeyPressed = false, LeftKeyPressed = false, RightKeyPressed = false;
        public Image Character;
        public Collisia collisia;
        List<BitmapImage> animations = new List<BitmapImage>();
        public DispatcherTimer animTimer = new DispatcherTimer();
        int steps = 0;
        public Player(Canvas canvas, Image Character, Collisia collisia)
        {
            this.Character = Character;
            this.collisia = collisia;
            animTimer.Interval = TimeSpan.FromMilliseconds(75);
            animTimer.Tick += heroTick;
            animTimer.Start();
            FillList();
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
            animations.Add(new BitmapImage(new Uri("Right1.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("Right2.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("Left1.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("Left2.png", UriKind.Relative)));
        }

        private void heroTick(object sender, EventArgs e)
        {

            if (DownKeyPressed)
            {
                AnimateHero(0, 3);
            }
            if (UpKeyPressed)
            {
                AnimateHero(4, 5);
            }
            if (RightKeyPressed)
            {
                AnimateHero(6, 7);
            }
            if (LeftKeyPressed)
            {
                AnimateHero(8, 9);
            }
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
            }
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                RightKeyPressed = true;
            }
        }
    }
}
