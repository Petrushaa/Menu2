using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu2.Classes
{
    public class Mob
    {
        public string Name { get; set; } // имя моба
        public int MaxHealth { get; set; } // максимальное хп моба
        public int CurrentHealth { get; set; } // текущее хп моба
        public string Image { get; set; } // путь к изображению моба
        public int Speed { get; set; }// скорость моба
    }
}
