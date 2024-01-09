using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu2.Menu
{
    public partial class gameOver : Window
    {
        GamePlay spawn;
        public gameOver(GamePlay spawn)
        {
            InitializeComponent();
            this.spawn = spawn;
        }
        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btRestart_Click(object sender, RoutedEventArgs e)
        {
            GamePlay.GameTimer.Start();
            GamePlay.countKeys = 0;
            Canvas.SetLeft(GamePlay.hero, 935);
            Canvas.SetTop(GamePlay.hero, 540);
            spawn.RestartGame();
            Game.frame.NavigationService.Navigate(spawn);
            this.Close();
        }
    }
}
