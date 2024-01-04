using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Menu2.Classes
{
    internal class mob
    {
        Canvas canvas = new Canvas();
        public static int zombieSpeed = 1;
        public Image griver;// Добавлено свойство griver
        public int Health = 100;// Добавлено свойство Health
        Random rand;
        public mob(Canvas canvas, Random rand)
        {
            this.canvas = canvas;
            this.rand = rand;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                canvas.Children.Remove(griver);
            } 
        }
        public void makeGrivers()
        {
            griver = new Image(); // Используйте свойство griver
            griver.Tag = "griver";
            griver.Source = new BitmapImage(new Uri("zUp.png", UriKind.Relative));
            Canvas.SetLeft(griver, rand.Next(0, 1280));
            Canvas.SetTop(griver, rand.Next(0, 720));
            griver.Height = 50;
            griver.Width = 50;
            canvas.Children.Add(griver);
            Canvas.SetZIndex(griver, 1);
        }
    }
}
