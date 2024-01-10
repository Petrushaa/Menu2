using Menu2.Classes;
using Menu2.Maze;
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
    public partial class GamePlay : Page
    {
        public static DispatcherTimer GameTimer = new DispatcherTimer();
        Player player;
        Collisia collisia;
        hotSettings hotset;
        Maze1 mazeUp;
        MazeD mazeDown;
        MazeL mazeLeft;
        MazeR mazeRight;
        public static int countKeys = 0;
        public static Image hero;
        private bool isFKeyPressed = false;

        

        public GamePlay()
        {
            InitializeComponent();
            hero = Character;
            GameScreen.Focus();

            collisia = new Collisia(GameScreen, Character, player);
            player = new Player(GameScreen, Character, collisia);
            collisia.player = player;
            hotset = new hotSettings(GameTimer);
            mazeUp = new Maze1("up", this);
            mazeRight = new MazeR("right", this);
            mazeDown = new MazeD("down", this);
            mazeLeft = new MazeL("left", this);
            GameTimer.Interval = TimeSpan.FromMilliseconds(5);
            GameTimer.Tick += GameTick;
            GameTimer.Start();

        }



        private void GameTick(object sender, EventArgs e)
        {
            lbCount.Content = "Count of codes: " + Convert.ToString(countKeys);
            collisia.elementsCopy = GameScreen.Children.OfType<Rectangle>().ToList();
            if (Canvas.GetTop(Character) < 0)//верх
            {
                GameTimer.Stop();
                
                player.UpKeyPressed = false;
                player.DownKeyPressed = false;
                player.RightKeyPressed = false;
                player.LeftKeyPressed = false;
                Maze1.GameTimer.Start();
                Maze1.griverTimer.Start();
                Maze1.timer.Start();
                Maze1.TimerSpawn.Start();
                Canvas.SetTop(Maze1.hero, Canvas.GetTop(Maze1.hero) - 35);
                Game.frame.NavigationService.Navigate(mazeUp);
            }
            else if (Canvas.GetLeft(Character) > GameScreen.Width)//право
            {
                GameTimer.Stop();
                player.UpKeyPressed = false;
                player.DownKeyPressed = false;
                player.RightKeyPressed = false;
                player.LeftKeyPressed = false;
                MazeR.GameTimer.Start();
                MazeR.griverTimer.Start();
                MazeR.timer.Start();
                MazeR.TimerSpawn.Start();
                Canvas.SetLeft(MazeR.hero, Canvas.GetLeft(MazeR.hero) + 35);
                NavigationService.Navigate(mazeRight);
            }
            else if (Canvas.GetTop(Character) > GameScreen.Height)//низ
            {
                GameTimer.Stop();
                player.UpKeyPressed = false;
                player.DownKeyPressed = false;
                player.RightKeyPressed = false;
                player.LeftKeyPressed = false;
                MazeD.GameTimer.Start();
                MazeD.griverTimer.Start();
                MazeD.timer.Start();
                MazeD.TimerSpawn.Start();
                Canvas.SetTop(MazeD.hero, Canvas.GetTop(MazeD.hero) + 35);
                NavigationService.Navigate(mazeDown);
            }
            if ((Canvas.GetLeft(Character) < 0)) //лево
            {
                GameTimer.Stop();
                player.UpKeyPressed = false;
                player.DownKeyPressed = false;
                player.RightKeyPressed = false;
                player.LeftKeyPressed = false;
                MazeL.GameTimer.Start();
                MazeL.griverTimer.Start();
                MazeL.timer.Start();
                MazeL.TimerSpawn.Start();
                Canvas.SetLeft(MazeL.hero, Canvas.GetLeft(MazeL.hero) - 35);
                NavigationService.Navigate(mazeLeft);
            }
            player.Move();//активируем метод движения нашего игрока
        }
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player.KeyboardUp(sender, e);
            if (e.Key == Key.F)
            {
                isFKeyPressed = false;
            }
        }

        private void KeyBoardDown(object sender, KeyEventArgs e)
        {
            player.KeyBoardDown(sender, e);
            if (e.Key == Key.Escape)
            {
                player.UpKeyPressed = false;
                player.DownKeyPressed = false;
                player.LeftKeyPressed = false;
                player.RightKeyPressed = false;
                player.UpKeyPressed = false;
                hotset.Visibility = Visibility.Visible;
                GameTimer.Stop();
            }
            if (e.Key == Key.F && !isFKeyPressed)
            {
                collisia.Lift();
                isFKeyPressed = true;
                player.UpKeyPressed = false;
                player.DownKeyPressed = false;
                player.LeftKeyPressed = false;
                player.RightKeyPressed = false;
            }
        }

        public void RestartGame()
        {
            mazeUp = new Maze1("up", this);
            mazeRight = new MazeR("right", this);
            mazeDown = new MazeD("down", this);
            mazeLeft = new MazeL("left", this);
        }
    }
}
