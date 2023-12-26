using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Menu2.Classes
{
    internal class mob
    {
        Random rand = new Random();
        List<Image> griverList;
        Canvas canvas = new Canvas();
        Rectangle player;
        public mob(List<Image> griverList, Canvas canvas, Rectangle player)
        {
            this.griverList = griverList;
            this.canvas = canvas;
            this.player = player;
        }
        private void makeGrivers()
        {
            Image griver = new Image();
            griver.Tag = "zombie";
            griver.Source = new BitmapImage(new Uri("zombie.png", UriKind.Relative));
            Canvas.SetLeft(griver, rand.Next(-500, 500));
            Canvas.SetTop(griver, rand.Next(-500, 500));
            griver.Height = 50;
            griver.Width = 50;
            griverList.Add(griver);
            canvas.Children.Add(griver);
            Canvas.SetZIndex(griver, 1);
        }
    }
}
