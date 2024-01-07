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
        public static int griverSpeed = 2;
        public Image griver;// Добавлено свойство griver
        public int Health = 100;// Добавлено свойство Health
        Random rand;
        public static Image character;
        public string direction;
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
            griver.Source = new BitmapImage(new Uri("griver1.png", UriKind.Relative));
            Canvas.SetLeft(griver, rand.Next(0, (int)canvas.Width));
            Canvas.SetTop(griver, rand.Next(0, (int)canvas.Height));
            griver.Height = character.Height*4;
            griver.Width = character.Width*4;
            canvas.Children.Add(griver);
            Canvas.SetZIndex(griver, 1);
        }
    }
}
