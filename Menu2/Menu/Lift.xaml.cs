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
    /// <summary>
    /// Логика взаимодействия для Lift.xaml
    /// </summary>
    public partial class Lift : Window
    {
        public Lift()
        {
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
                if (GamePlay.countKeys == 4)
                {
                    //ВЫИГРЫШ 
                }
                else
                {
                    lbLose.Opacity = 1;
                    
                }
            }
        }
    }
}
