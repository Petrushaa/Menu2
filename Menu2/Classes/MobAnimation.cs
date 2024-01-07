using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Menu2.Classes
{
    //internal class MobAnimation
    //{
    //    DispatcherTimer timer;
    //    Image griver;
    //    List<string> animations;
    //    int steps = 0;
    //    int slowDownFrameRate = 0;

    //    public MobAnimation(Image griver)
    //    {
    //        SetUp();
    //        this.griver = griver;
    //        timer = new DispatcherTimer();
    //        timer.Interval = TimeSpan.FromMilliseconds(400); // Установите интервал, который вам подходит
    //        timer.Tick += Timer_Tick;
    //        timer.Start();
    //    }

    //    //private void SetUp()
    //    //{
    //    //    animations = new List<string>() { "griver1.png", "griver2.png", "griver3.png", "griver4.png", "griver5.png", "griver6.png" };
    //    //}

    //    private void Timer_Tick(object sender, EventArgs e)
    //    {
    //        AnimateGriver(0, animations.Count - 1);
    //    }

    //    public void AnimateGriver(int start, int end)
    //    {
    //        steps++;

    //        if (steps > end || steps < start)
    //        {
    //            steps = start;
    //        }
    //        griver.Source = new BitmapImage(new Uri(animations[steps], UriKind.Relative));
    //    }
    //}

}
