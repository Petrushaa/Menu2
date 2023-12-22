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
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private float SpeedX, SpeedY;
        Player player;
        Collisia collisia;
        //private void WindowMaximized()
        //{
        //    Создаем новый объект окна gamePlay
        //    this.WindowState = WindowState.Maximized;
        //}
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player.KeyboardUp(sender, e);
        }
        private void KeyBoardDown(object sender, KeyEventArgs e)
        {
            player.KeyBoardDown(sender, e);
        }
        public GamePlay()
        {
            InitializeComponent();
            //WindowMaximized();
            GameScreen.Focus();
            player = new Player(ImagePlayer, Character);
            collisia = new Collisia(GameScreen, Character, SpeedX, SpeedY, player);
            GameTimer.Interval = TimeSpan.FromMilliseconds(1);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
        }
        private void GameTick(object sender, EventArgs e)
        {
            if ((Canvas.GetLeft(Character) < 0) || (Canvas.GetTop(Character) < 0))
            {
                GameTimer.Stop();
                NavigationService.Navigate(new Maze1());
            }
            player.Move();
            SpeedX = player.X;
            SpeedY = player.Y;
            collisia.AllCollisia = GameScreen.Children.OfType<Rectangle>();
            collisia._SpeedX = SpeedX;
            collisia._SpeedY = SpeedY;
            collisia._Object = Character;
            Canvas.SetLeft(Character, Canvas.GetLeft(Character) + SpeedX);
            collisia.Collide("x");
            Canvas.SetTop(Character, Canvas.GetTop(Character) - SpeedY);
            collisia.Collide("y");
        }
    }
}
