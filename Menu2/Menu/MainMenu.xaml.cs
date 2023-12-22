using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public static Game AboutWindow;
        public MainMenu()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            // Создание объекта ImageBrush с ссылкой на изображение

        }

        private void btSettings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings_Page());
        }

        private void btGiveUp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btPlay_Click(object sender, RoutedEventArgs e)
        {
            if (AboutWindow == null)
            {
                AboutWindow = new Game();
                AboutWindow.Show();
                Window.GetWindow(this).Close(); // Закрыть текущее окно
            }
            else AboutWindow.Activate();
        }
    }
}
