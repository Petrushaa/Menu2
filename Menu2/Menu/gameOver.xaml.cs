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
using System.Windows.Shapes;

namespace Menu2.Menu
{
    public partial class gameOver : Window
    {
        Maze1 maze;
        public gameOver()
        {
            InitializeComponent();
            maze = new Maze1();
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btResume_Click(object sender, RoutedEventArgs e)
        {
            maze.RestartGame();
        }
    }
}
