using Menu2.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Menu2
{
    public partial class Maze1 : Page
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        Player player2;
        Collisia collisia;
        bool gameOver;
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        List<Image> griverList = new List<Image>();
        Random rnd = new Random();
        public Maze1()
        {
            InitializeComponent();
            GameScreen.Focus();
            collisia = new Collisia(GameScreen, Character, player2);
            player2 = new Player(GameScreen, Character, collisia);
            collisia.player = player2;
            GameTimer.Interval = TimeSpan.FromMilliseconds(1);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
        }
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player2.KeyboardUp(sender, e);
            if (e.Key == Key.Space && Player.ammo > 0)
            {
                Player.ammo--;
                player2.ShootBullet();
                if (Player.ammo < 1)
                {
                    Bullet ammo = new Bullet(GameScreen);
                    DropAmmo();
                }
            }
        }
        private void KeyBoardDown(object sender, KeyEventArgs e)
        {
            player2.KeyBoardDown(sender, e);
        }
        private void GameTick(object sender, EventArgs e)
        {
            collisia.elementsCopy = GameScreen.Children.Cast<UIElement>().ToList(); //передаем список из всех дочерних элементов на канвасе
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
            }
            lbAmmo.Content = "Ammo: " + Player.ammo;
            if ((Canvas.GetLeft(Character) > GameScreen.ActualWidth) || (Canvas.GetTop(Character) > GameScreen.ActualHeight))
            {
                GameTimer.Stop();
                NavigationService.Navigate(new GamePlay());
            }//переход на другую локацию
            player2.Move();//активируем метод движения нашего игрока
    


        }
        private void RestartGame()
        {

        }
        public void DropAmmo()
        {
            Image ammo = new Image();
            // Загружаем картинку из ресурсов проекта
            ammo.Source = new BitmapImage(new Uri("ammo.png", UriKind.RelativeOrAbsolute));
            ammo.Tag = "ammo";
            ammo.Height = 20;
            ammo.Width = 20;
            Canvas.SetTop(ammo, rnd.Next(10, Convert.ToInt32(GameScreen.ActualHeight - ammo.Height)));
            Canvas.SetLeft(ammo, rnd.Next(10, Convert.ToInt32(GameScreen.ActualWidth - ammo.Width)));
            GameScreen.Children.Add(ammo);
            Canvas.SetZIndex(ammo, 1);
            Canvas.SetZIndex(Character, 1);
        }
    }
}
