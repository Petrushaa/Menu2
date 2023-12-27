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
        private Rectangle bullet = new Rectangle();
        private DispatcherTimer bulletTimer = new DispatcherTimer();
        private Random rnd = new Random();
        public Bullet(Canvas canvas)
        {
            this.canvas = canvas;
        }
        public void MakeBullet()
        {
            // Создаем новый объект ImageBrush
            ImageBrush myImageBrush = new ImageBrush();

            // Загружаем картинку из ресурсов проекта
            myImageBrush.ImageSource = new BitmapImage(new Uri("characterLeft.png", UriKind.RelativeOrAbsolute));

            // Заполняем прямоугольник картинкой
            bullet.Fill = myImageBrush;
            bullet.Height = 20;
            bullet.Width = 20;
            bullet.Tag = "bullet";
            Canvas.SetLeft(bullet, bulletLeft);
            Canvas.SetTop(bullet, bulletTop);
            Canvas.SetZIndex(bullet, 1);
            canvas.Children.Add(bullet);
            bulletTimer.Interval = TimeSpan.FromMilliseconds(speed);
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
                Canvas.SetTop(bullet, Canvas.GetTop(bullet) - speed);
            }
            if (direction == "down")
            {
                Canvas.SetTop(bullet, Canvas.GetTop(bullet) + speed);
            }
            if (Canvas.GetLeft(bullet) < 10 || Canvas.GetLeft(bullet) > (canvas.ActualWidth - 50) || Canvas.GetTop(bullet) > (canvas.ActualHeight - 50) || Canvas.GetTop(bullet) < 10) //ограничения по окну
            {
                bulletTimer.Stop();
                bullet.Fill = null;
                bulletTimer = null;
                canvas.Children.Remove(bullet);
            }
        }
    }
}
