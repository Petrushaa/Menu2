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
    /// Логика взаимодействия для Settings_Page.xaml
    /// </summary>
    public partial class Settings_Page : Page
    {
        public Settings_Page()
        {
            InitializeComponent();
            // Создание объекта ImageBrush с ссылкой на изображение

            // Установка изображения в качестве заднего фона для грида

            btGamePlay.BorderBrush = Brushes.White;
            btGamePlay.Background = Brushes.Black;
            btGamePlay.Foreground = Brushes.White;

            btGraphics.BorderBrush = Brushes.White;
            btGraphics.Background = Brushes.Black;
            btGraphics.Foreground = Brushes.White;

            btVolume.BorderBrush = Brushes.White;
            btVolume.Background = Brushes.Black;
            btVolume.Foreground = Brushes.White;

            btBack.BorderBrush = Brushes.White;
            btBack.Background = Brushes.Black;
            btBack.Foreground = Brushes.White;
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
