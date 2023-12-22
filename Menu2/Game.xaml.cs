﻿using System;
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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
            MainFrame.Content = new GamePlay();
        }
        private void KeyDownK(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                hotSettings window = new hotSettings();
                window.Show();
            }
        }
    }
}