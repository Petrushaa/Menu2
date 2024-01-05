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
        PlayerMaze player2;
        CollisiaMaze collisia;
        bool gameOver;
        hotSettings hotset;
        RandomMaze randomMaze;
        List<mob> mobs = new List<mob>();
        Random rand = new Random();
        List<Bullet> bullets = new List<Bullet>();
        public Maze1()
        {
            InitializeComponent();
            randomMaze = new RandomMaze(maincanvas);
            GameScreen.Focus();
            collisia = new CollisiaMaze(maincanvas, Character, player2, bullets, rand);
            player2 = new PlayerMaze(maincanvas, Character, collisia);
            collisia.player = player2;
            mob.character = Character;
            RestartGame();
            GameTimer.Start();
            GameTimer.Interval = TimeSpan.FromMilliseconds(5);
            GameTimer.Tick += GameTick;
            hotset = new hotSettings(GameTimer);
            randomMaze.StartMaze();
            Canvas.SetZIndex(Character, 1);
            Canvas.SetZIndex(healthBar, 1);
            Canvas.SetZIndex(lbHealth, 1);
            Canvas.SetZIndex(lbAmmo, 1);
        }
        private void GameTick(object sender, EventArgs e)
        {
            collisia.elementsCopy = maincanvas.Children.Cast<UIElement>().ToList(); //передаем список из всех дочерних элементов на канвасе
            lbAmmo.Content = "Ammo: " + PlayerMaze.ammo;
            if ((Canvas.GetLeft(Character) > maincanvas.ActualWidth) || (Canvas.GetTop(Character) > maincanvas.ActualHeight))
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
            collisia.mobs = mobs;
            player2.Move();//активируем метод движения нашего игрока
            BulletTimer_Tick();
        }
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            player2.KeyboardUp(sender, e);
            if (e.Key == Key.Space && PlayerMaze.ammo > 0 && gameOver == false)
            {
                PlayerMaze.ammo--;
                ShootBullet();
                if (PlayerMaze.ammo < 1)
                {
                    
                    DropAmmo();
                }
            }
        }
        public void ShootBullet()
        {
            Bullet shootBullet = new Bullet(maincanvas, Character, bullets);
            bullets.Add(shootBullet);
            shootBullet.direction = PlayerMaze.facing;
            shootBullet.bulletLeft = Canvas.GetLeft(Character) + (Character.Width / 2); //выбираем координаты для спавна пули
            shootBullet.bulletTop = Canvas.GetTop(Character) + (Character.Height / 2); //коорды = положение перса + половина его3 размеров
            shootBullet.MakeBullet();
        }
        public void DropAmmo()
        {
            double spawnX, spawnY;
            bool isColliding;
            Image ammo = new Image();
            // Загружаем картинку из ресурсов проекта
            ammo.Source = new BitmapImage(new Uri("ammo.png", UriKind.RelativeOrAbsolute));
            ammo.Tag = "ammo";
            ammo.Height = ((int)Character.Height);
            ammo.Width = ((int)Character.Width);
            do
            {
                isColliding = false;
                // Генерируем случайные координаты для спавна пули
                spawnX = rand.Next((int)Canvas.GetLeft(Character)-1000, (int)Canvas.GetLeft(Character) + 1000);
                spawnY = rand.Next((int)Canvas.GetTop(Character)-1000, (int)Canvas.GetTop(Character) + 1000);

                Rect bulletSpawnArea = new Rect(spawnX, spawnY, ammo.Width, ammo.Height);

                // Проверяем столкновение со всеми препятствиями
                foreach (UIElement element in maincanvas.Children)
                {
                    if (element is Rectangle rectangle && (string)rectangle.Tag == "Collide")
                    {
                        Rect rectangleArea = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width, rectangle.Height);
                        if (bulletSpawnArea.IntersectsWith(rectangleArea))
                        {
                            isColliding = true;
                            break;
                        }
                    }
                }
            } while (isColliding);
            Canvas.SetTop(ammo, spawnY);
            Canvas.SetLeft(ammo, spawnX);
            maincanvas.Children.Add(ammo);
            Canvas.SetZIndex(ammo, 1);
            Canvas.SetZIndex(Character, 1);
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
        public void BulletTimer_Tick()
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.MoveBullet();
            }
        }
        public void RestartGame()
        {
            Character.Source = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));
            foreach (mob mobe in mobs)
            {
                maincanvas.Children.Remove(mobe.griver);
            }
            mobs.Clear(); // Очистите список mobs
            for (int i = 0; i < 3; i++)
            {
                mob newMob = new mob(maincanvas, rand);
                mobs.Add(newMob);
                newMob.makeGrivers();
            }
            player2.UpKeyPressed = false;
            player2.DownKeyPressed = false;
            player2.LeftKeyPressed = false;
            player2.RightKeyPressed = false;
            gameOver = false;
            player2.Health = 100;
            PlayerMaze.ammo = 10;
            GameTimer.Start();
        }
    }
}
