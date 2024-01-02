using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Menu2.Classes
{
    // Класс MobViewModel
    public class MobViewModel : INotifyPropertyChanged
    {
        private Mob mob; // поле для хранения моба

        public event PropertyChangedEventHandler PropertyChanged; // событие для уведомления об изменении свойств

        public event EventHandler Dead; // событие для уведомления о смерти моба

        public MobViewModel(Mob mob) // конструктор с параметром моба
        {
            this.mob = mob; // сохраняем моба в поле
        }

        // Свойство для получения имени моба
        public string Name
        {
            get { return mob.Name; }
        }

        // Свойство для получения максимального хп моба
        public int MaxHealth
        {
            get { return mob.MaxHealth; }
        }

        // Свойство для получения и установки текущего хп моба
        public int CurrentHealth
        {
            get { return mob.CurrentHealth; }
            set
            {
                if (value != mob.CurrentHealth) // если значение изменилось
                {
                    mob.CurrentHealth = value; // обновляем значение в мобе
                    OnPropertyChanged("CurrentHealth"); // вызываем метод для уведомления об изменении свойства
                    if (value == 0) // если хп моба стало нулем
                    {
                        OnDead(); // вызываем метод для уведомления о смерти моба
                    }
                }
            }
        }

        // Свойство для получения изображения моба
        public string Image
        {
            get { return mob.Image; }
        }

        // Метод для вызова события PropertyChanged
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // если есть подписчики на событие
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // вызываем событие с указанием имени свойства
            }
        }

        // Метод для вызова события Dead
        protected void OnDead()
        {
            if (Dead != null) // если есть подписчики на событие
            {
                Dead(this, EventArgs.Empty); // вызываем событие с пустым аргументом
            }
        }
    }

}
