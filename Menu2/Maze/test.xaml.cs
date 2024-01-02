using Menu2.Classes;
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

namespace Menu2.Maze
{
    /// <summary>
    /// Логика взаимодействия для test.xaml
    /// </summary>
    // Пример использования MobView в главном окне
    public partial class test : Window
    {
        public test()
        {
            InitializeComponent();

            // Создаем экземпляр моба
            Mob zombie = new Mob
            {
                Name = "zombie",
                MaxHealth = 100,
                CurrentHealth = 50,
                Image = "zombie.png"
            };

            // Создаем экземпляр модели представления моба
            MobViewModel viewModel = new MobViewModel(zombie);

            // Создаем экземпляр представления моба
            MobView view = new MobView(viewModel);

            // Добавляем представление моба в главное окно
            this.Content = view;
            // Подписываемся на событие Dead модели представления моба
            viewModel.Dead += ViewModel_Dead;
        }
        // Обработчик события Dead модели представления моба
        private void ViewModel_Dead(object sender, EventArgs e)
        {
            // Получаем модель представления моба из параметра sender
            MobViewModel viewModel = sender as MobViewModel;

            // Если модель представления моба не null
            if (viewModel != null)
            {
                // Удаляем представление моба из главного окна
                this.Content = null;

                // Отписываемся от события Dead модели представления моба
                viewModel.Dead -= ViewModel_Dead;
            }
        }


    }
}
