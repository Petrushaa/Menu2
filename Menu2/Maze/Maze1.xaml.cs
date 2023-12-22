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
        private float SpeedX, SpeedY;
        Player player2;
        Collisia collisia;
        //private void WindowMaximized()
        //{
        //    Создаем новый объект окна gamePlay
        //    this.WindowState = WindowState.Maximized;
        //}
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player2.KeyboardUp(sender, e);
        }
        private void KeyBoardDown(object sender, KeyEventArgs e)
        {
            player2.KeyBoardDown(sender, e);
        }
        public Maze1()
        {
            InitializeComponent();
            GameScreen.Focus();
            player2 = new Player(ImagePlayer, Character);
            collisia = new Collisia(GameScreen, Character, SpeedX, SpeedY, player2);
            GameTimer.Interval = TimeSpan.FromMilliseconds(1);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
        }

        private void windowMaze_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GameTick(object sender, EventArgs e)
        {
            if ((Canvas.GetLeft(Character) > GameScreen.ActualWidth) || (Canvas.GetTop(Character) > GameScreen.ActualHeight))
            {

                GameTimer.Stop();
                NavigationService.Navigate(new GamePlay());
            }
            player2.Move();
            SpeedX = player2.X;
            SpeedY = player2.Y;
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
