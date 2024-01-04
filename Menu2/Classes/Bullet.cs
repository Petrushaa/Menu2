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
using System.Windows.Shapes;
namespace Menu2.Classes
{
    internal class Bullet
    {
        Canvas canvas = new Canvas();
        public string direction;
        public double bulletLeft;
        public double bulletTop;
        private int speed = 20;
        private Image bullet = new Image();
        public DispatcherTimer bulletTimer = new DispatcherTimer();
        private Random rnd = new Random();
        private Image character;
        private int bulletHeight;
        private int bulletWidth;
        public Bullet(Canvas canvas, Image character)
        {
            this.canvas = canvas;
            this.character = character;
            bulletHeight = ((int)character.Height) / 2;
            bulletWidth = ((int)character.Width) / 2;
        }
        public void MakeBullet()
        {
            //Загружаем картинку из ресурсов проекта
            bullet.Height = bulletHeight;
            bullet.Width = bulletWidth;
            bullet.Tag = "bullet";
            Canvas.SetZIndex(bullet, 1); //выдвигаем на передний план
            Canvas.SetLeft(bullet, bulletLeft);
            Canvas.SetTop(bullet, bulletTop);
            canvas.Children.Add(bullet);
            bulletTimer.Interval = TimeSpan.FromMilliseconds(speed);
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }
        private void BulletTimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
            {
                bullet.Source = new BitmapImage(new Uri("bulletL.png", UriKind.RelativeOrAbsolute));
                Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) - speed);
            }
            if (direction == "right")
            {
                bullet.Source = new BitmapImage(new Uri("bulletR.png", UriKind.RelativeOrAbsolute));
                Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + speed);
            }
            if (direction == "up")
            {
                bullet.Source = new BitmapImage(new Uri("bulletU.png", UriKind.RelativeOrAbsolute));
                Canvas.SetTop(bullet, Canvas.GetTop(bullet) - speed);
            }
            if (direction == "down")
            {
                bullet.Source = new BitmapImage(new Uri("bulletD.png", UriKind.RelativeOrAbsolute));
                Canvas.SetTop(bullet, Canvas.GetTop(bullet) + speed);
            }
            if (Canvas.GetLeft(bullet) < 10 || Canvas.GetLeft(bullet) > (canvas.ActualWidth - 50) || Canvas.GetTop(bullet) > (canvas.ActualHeight - 50) || Canvas.GetTop(bullet) < 10) //ограничения по окну
            {
                bulletTimer.Stop();
                bullet.Source = null;
                bulletTimer = null;
                canvas.Children.Remove(bullet);
            }
        }
        public void DropAmmo()
        {
            Image ammo = new Image();
            // Загружаем картинку из ресурсов проекта
            ammo.Source = new BitmapImage(new Uri("ammo.png", UriKind.RelativeOrAbsolute));
            ammo.Tag = "ammo";
            ammo.Height = ((int)character.Height);
            ammo.Width = ((int)character.Width);
            Canvas.SetTop(ammo, rnd.Next(10, Convert.ToInt32(canvas.ActualHeight - ammo.Height)));
            Canvas.SetLeft(ammo, rnd.Next(10, Convert.ToInt32(canvas.ActualWidth - ammo.Width)));
            canvas.Children.Add(ammo);
            Canvas.SetZIndex(ammo, 1);
            Canvas.SetZIndex(character, 1);
        }
    }
}
