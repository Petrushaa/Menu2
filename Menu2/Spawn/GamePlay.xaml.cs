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
        public DispatcherTimer GameTimer = new DispatcherTimer();
        Player player;
        Collisia collisia;
        hotSettings hotset;
        public GamePlay()
        {
            InitializeComponent();
            GameScreen.Focus();
            collisia = new Collisia(GameScreen, Character, player);
            player = new Player(GameScreen, Character, collisia);
            collisia.player = player;
            GameTimer.Interval = TimeSpan.FromMilliseconds(5);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
            hotset = new hotSettings(GameTimer);
        }
        private void GameTick(object sender, EventArgs e)
        {
            collisia.elementsCopy = GameScreen.Children.OfType<Rectangle>().ToList();
            if ((Canvas.GetLeft(Character) < 0) || (Canvas.GetTop(Character) < 0) || (Canvas.GetLeft(Character) > GameScreen.Width) || (Canvas.GetTop(Character) > GameScreen.Height))
            {
                GameTimer.Stop();
                NavigationService.Navigate(new Maze1());
            }
            player.Move();//активируем метод движения нашего игрока
        }
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player.KeyboardUp(sender, e);
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
        }
    }
}
