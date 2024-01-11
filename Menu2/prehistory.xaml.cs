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

namespace Menu2
{
    /// <summary>
    /// Логика взаимодействия для prehistory.xaml
    /// </summary>
    public partial class prehistory : Page
    {
        Game game;
        public prehistory(Game game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GamePlay(game));
        }


    }
}
