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
    public partial class Lift : Window
    {
        Game game;
        string code = "";
        public Lift(Game game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            if (e.Key == Key.Enter)
            {
                if (code == GamePlay.kode)
                {
                    TheEnd end = new TheEnd();
                    end.Show();
                    this.Close();
                    game.Close();
                }
                else
                {
                    lbLose.Content = "Error";
                }
            }
        }

        private void one_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "1";
                lbLose.Content = code;
            }
        }

        private void two_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "2";
                lbLose.Content = code;
            }
        }

        private void three_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "3";
                lbLose.Content = code;
            }
        }

        private void four_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "4";
                lbLose.Content = code;
            }
        }

        private void five_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "5";
                lbLose.Content = code;
            }
        }

        private void six_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "6";
                lbLose.Content = code;
            }
        }

        private void seven_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "7";
                lbLose.Content = code;
            }
        }

        private void eight_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "8";
                lbLose.Content = code;
            }
        }

        private void nine_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "9";
                lbLose.Content = code;
            }
        }

        private void null_Click(object sender, RoutedEventArgs e)
        {
            if (code.Length != 4)
            {
                code += "0";
                lbLose.Content = code;
            }

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            code = "";
            lbLose.Content = "";
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (code == GamePlay.kode)
            {
                TheEnd end = new TheEnd();
                end.Show();
                this.Close();
                game.Close();
            }
            else
            {
                lbLose.Content = "Error";
            }
        }
    }
}
