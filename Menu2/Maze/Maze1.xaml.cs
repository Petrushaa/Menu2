using Menu2.Classes;
using Menu2.Menu;
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
        List<Image> griverList = new List<Image>();
        mob mobe;
        Random rnd = new Random();
        public Maze1()
        {
            InitializeComponent();
            GameScreen.Focus();
            collisia = new Collisia(GameScreen, Character, player2);
            player2 = new Player(GameScreen, Character, collisia);
            collisia.player = player2;
            mobe = new mob(griverList, GameScreen, Character, player2);
            collisia.mobe = mobe;
            RestartGame();
            GameTimer.Interval = TimeSpan.FromMilliseconds(1);
            GameTimer.Tick += GameTick;
            GameTimer.Start();


        }
        private void GameTick(object sender, EventArgs e)
        {
            collisia.elementsCopy = GameScreen.Children.Cast<UIElement>().ToList(); //передаем список из всех дочерних элементов на канвасе
            lbAmmo.Content = "Ammo: " + Player.ammo;
            if ((Canvas.GetLeft(Character) > GameScreen.ActualWidth) || (Canvas.GetTop(Character) > GameScreen.ActualHeight))
            {
                GameTimer.Stop();
                // Get the navigation service from the current page
                // Navigate to the GamePlay page
                NavigationService.Navigate(new GamePlay());
            }//переход на другую локацию
            if (player2.Health > 1)
            {
                healthBar.Value = player2.Health;
            }
            else
            {
                gameOver = true;
                player2.UpKeyPressed = false;
                player2.DownKeyPressed = false;
                player2.RightKeyPressed = false;
                player2.LeftKeyPressed = false;
                GameTimer.Stop();
                // Передаем ссылку на текущее окно в конструктор второго окна
                gameOver gOver = new gameOver(this);
                gOver.Show();
            }
            player2.Move();//активируем метод движения нашего игрока
        }
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player2.KeyboardUp(sender, e);
            if (e.Key == Key.Space && Player.ammo > 0 && gameOver == false)
            {
                Player.ammo--;
                player2.ShootBullet();
                if (Player.ammo < 1)
                {
                    Bullet ammo = new Bullet(GameScreen, Character);
                    ammo.DropAmmo();
                }
            }
        }
        private void KeyBoardDown(object sender, KeyEventArgs e)
        {
            if (gameOver == true)
            {
                return;
            }
            player2.KeyBoardDown(sender, e);
        }
        public void RestartGame()
        {
            Character.Source = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));
            foreach (Image i in griverList)
            {
                GameScreen.Children.Remove(i);
            }
            griverList.Clear();
            for (int i = 0; i < 3; i++)
            {
                mobe.makeGrivers();
            }
            player2.UpKeyPressed = false;
            player2.DownKeyPressed = false;
            player2.LeftKeyPressed = false;
            player2.RightKeyPressed = false;
            gameOver = false;
            player2.Health = 100;
            Player.ammo = 10;
            GameTimer.Start();
        }
    }
}
