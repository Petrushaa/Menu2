using Menu2.Menu;
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
    public partial class Game : Window
    {
        public static Frame frame { get; set; }
        public Game()
        {
            InitializeComponent();
            MainFrame.Content = new prehistory(this);
            frame = MainFrame;
        }
    }
}
