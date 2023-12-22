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

namespace Menu2
{
    /// <summary>
    /// Логика взаимодействия для hotSettings.xaml
    /// </summary>
    public partial class hotSettings : Window
    {
        public hotSettings()
        {
            InitializeComponent();
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btResume_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
