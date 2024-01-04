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
        public DispatcherTimer GameTimer = new DispatcherTimer();
        Player player2;
        Collisia collisia;
        bool gameOver;
        mob mobe;
        hotSettings hotset;
        RandomMaze randomMaze;
        List<mob> mobs = new List<mob>();
        Random rand = new Random();
        public Maze1()
        {
            InitializeComponent();
            randomMaze = new RandomMaze(windowMaze, GameScreen);
            GameScreen.Focus();
            collisia = new Collisia(GameScreen, Character, player2, rand);
            player2 = new Player(GameScreen, Character, collisia);
            collisia.player = player2;
            RestartGame();
            GameTimer.Interval = TimeSpan.FromMilliseconds(5);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
            hotset = new hotSettings(GameTimer);
            randomMaze.StartMaze();
            Canvas.SetZIndex(Character, 1);
            Canvas.SetZIndex(healthBar, 1);
            Canvas.SetZIndex(lbHealth, 1);
            Canvas.SetZIndex(lbAmmo, 1);
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
            if (player2.Health > 0)
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
                healthBar.Value = 0;
            }
            player2.Move();//активируем метод движения нашего игрока
            collisia.mobs = mobs;
            collisia.griversCollide();
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
            if (e.Key == Key.Escape)
            {
                player2.UpKeyPressed = false;
                player2.DownKeyPressed = false;
                player2.LeftKeyPressed = false;
                player2.RightKeyPressed = false;
                hotset.Visibility = Visibility.Visible;
                GameTimer.Stop();
            }
        }
        public void RestartGame()
        {
            Character.Source = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));
            foreach (mob mobe in mobs)
            {
                GameScreen.Children.Remove(mobe.griver);
            }
            mobs.Clear(); // Очистите список mobs
            for (int i = 0; i < 3; i++)
            {
                mob newMob = new mob(GameScreen, rand);
                mobs.Add(newMob);
                newMob.makeGrivers();
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
