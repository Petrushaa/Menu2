using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
namespace Menu2.Classes
{
    internal class Bullet
    {
        Canvas canvas;
        public string direction;
        public double bulletLeft;
        public double bulletTop;
        private int speed = 20;
        private Image bullet = new Image();
        private DispatcherTimer bulletTimer = new DispatcherTimer();
        private Random rnd = new Random();
        public Bullet(Canvas canvas)
        {
            this.canvas = canvas;
        }
        public void MakeBullet(Canvas canvas)
        {
            bullet.Height = 20;
            bullet.Width = 20;
            bullet.Tag = "Bullet";
            canvas.Children.Add(bullet);
            Canvas.SetLeft(bullet, bulletLeft);
            Canvas.SetTop(bullet, bulletTop);
            Canvas.SetZIndex(bullet, 1);
            bulletTimer.Interval = TimeSpan.FromMilliseconds(20);
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }
        private void BulletTimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
            {
                Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) - speed);
            }
            if (direction == "right")
            {
                Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + speed);
            }
            if (direction == "up")
            {
                Canvas.SetLeft(bullet, Canvas.GetTop(bullet) - speed);
            }
            if (direction == "down")
            {
                Canvas.SetLeft(bullet, Canvas.GetTop(bullet) + speed);
            }
            if ((Canvas.GetLeft(bullet) < 10) || (Canvas.GetLeft(bullet) > 860) || (Canvas.GetTop(bullet) > 600)) //ограничения по окну
            {
                bulletTimer.Stop();
                bulletTimer = null;
                bullet = null;
            }
        }
        //private void DropAmmo()
        //{
        //    Image ammo = new Image();
        //    ammo.Source = new BitmapImage(new Uri("ammo.png", UriKind.Relative));
        //    Canvas.SetLeft(rnd.Next(10, Canvas.HeightProperty - ammo.Width));
            
        //}
    }
}
