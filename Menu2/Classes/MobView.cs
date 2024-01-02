using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Menu2.Classes
{
    public class MobView : UserControl
    {
        public MobView(MobViewModel viewModel) // конструктор с параметром модели представления моба
        {
            this.DataContext = viewModel; // устанавливаем контекст данных для привязки

            // Создаем элемент Image для отображения изображения моба
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(viewModel.Image, UriKind.Relative)); // устанавливаем источник изображения из модели представления
            image.Width = 100; // устанавливаем ширину изображения
            image.Height = 100; // устанавливаем высоту изображения
            image.Margin = new Thickness(10); // устанавливаем отступы изображения

        }
    }
}