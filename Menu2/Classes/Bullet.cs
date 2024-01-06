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
        Canvas canvas;
        public string direction;
        public double bulletLeft;
        public double bulletTop;
        private int speed = 20;
        public Image bullet = new Image();
        private int bulletHeight;
        private int bulletWidth;
        List<Bullet> bullets;
        public Bullet(Canvas canvas, Image character, List<Bullet> bullets)
        {
            this.canvas = canvas;
            bulletHeight = ((int)character.Height) / 2;
            bulletWidth = ((int)character.Width) / 2;
            this.bullets = bullets;
        }
        public void MakeBullet()
        {
            //Загружаем картинку из ресурсов проекта
            bullet.Source = new BitmapImage(new Uri("bulletR.png", UriKind.Relative));
            bullet.Height = bulletHeight;
            bullet.Width = bulletWidth;
            bullet.Tag = "bullet";
            Canvas.SetZIndex(bullet, 1); //выдвигаем на передний план
            Canvas.SetLeft(bullet, bulletLeft);
            Canvas.SetTop(bullet, bulletTop);
            canvas.Children.Add(bullet);
        }
        public void MoveBullet()
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
        }
    }
}