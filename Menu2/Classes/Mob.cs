using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Menu2.Classes
{
    internal class mob
    {
        Random rand = new Random();
        List<Image> griverList;
        Canvas canvas = new Canvas();
        Image character;
        Player player;
        public static int zombieSpeed = 1;
        public mob(List<Image> griverList, Canvas canvas, Image character, Player player)
        {
            this.griverList = griverList;
            this.canvas = canvas;
            this.character = character;
            this.player = player;
        }
        public void makeGrivers()
        {
            Image griver = new Image();
            griver.Tag = "griver";
            griver.Source = new BitmapImage(new Uri("zombie.png", UriKind.Relative));
            Canvas.SetLeft(griver, rand.Next(50, Convert.ToInt32(canvas.Width - 50)));
            Canvas.SetTop(griver, rand.Next(50, Convert.ToInt32(canvas.Height - 50)));
            griver.Height = 50;
            griver.Width = 50;
            griverList.Add(griver);
            canvas.Children.Add(griver);
            Canvas.SetZIndex(griver, 1);
        }
    }
}
