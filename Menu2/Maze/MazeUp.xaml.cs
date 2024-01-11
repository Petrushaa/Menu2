using Menu2.Classes;
using Menu2.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace Menu2
{
    public partial class Maze1 : Page
    {
        public static DispatcherTimer TimerSpawn = new DispatcherTimer();
        public static DispatcherTimer GameTimer = new DispatcherTimer();
        public static DispatcherTimer griverTimer = new DispatcherTimer();
        public static DispatcherTimer timer;
        DateTime start;
        PlayerMaze player2;
        CollisiaMaze collisia;
        bool gameOver, NitroUsed;
        hotSettings hotset;
        RandomMaze randomMaze;
        List<mob> mobs = new List<mob>();
        Random rand = new Random();
        List<Bullet> bullets = new List<Bullet>();
        int steps = 0;
        List<BitmapImage> animations = new List<BitmapImage>();
        public static Image hero;
        private bool isFKeyPressed = false;
        GamePlay spawn;
        public int number;
        public Maze1(string direction, GamePlay spawn, int number)
        {
            InitializeComponent();
            this.spawn = spawn;
            this.number = number;

            hero = Character;
            griverTimer.Interval = TimeSpan.FromMilliseconds(100);
            griverTimer.Tick += griverTick;
            TimerSpawn.Interval = TimeSpan.FromMinutes(1);
            TimerSpawn.Tick += griverSpawn;

            GameTimer.Interval = TimeSpan.FromMilliseconds(5);
            GameTimer.Tick += GameTick;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            randomMaze = new RandomMaze(maincanvas, direction);
            GameScreen.Focus();
            collisia = new CollisiaMaze(maincanvas, Character, player2, bullets, lbAmmo);
            player2 = new PlayerMaze(maincanvas, Character, collisia);
            collisia.player = player2;
            mob.character = Character;
            hotset = new hotSettings(GameTimer);
            randomMaze.StartMaze();
            Canvas.SetZIndex(Character, 1);
            DropPinCode();
            
            start = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                SpawnTraps();
            }

            for (int i = 0; i < 10; i++)
            {
                DropAmmo();
            }
            
            player2.Health = 100;
            PlayerMaze.ammo = 5;


            FillList();
            mob newMob = new mob(maincanvas, rand);
            mobs.Add(newMob);
            newMob.makeGrivers();
        }
        
        private void FillList()
        {
            animations.Add(new BitmapImage(new Uri("griver1.PNG", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("griver2.PNG", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("griver3.PNG", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("griver4.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("griver5.png", UriKind.Relative)));
            animations.Add(new BitmapImage(new Uri("griver6.png", UriKind.Relative)));
        }
        private void griverSpawn(object sender, EventArgs e)
        {
            mob newMob = new mob(maincanvas, rand);
            mobs.Add(newMob);
            newMob.makeGrivers();
        }
        private void griverTick(object sender, EventArgs e)
        {
            foreach (mob mobe in mobs)
            {
                Image mobb = mobe.griver;

                    if (mobe.direction == "Right")
                    {
                        //право
                        AnimateGriver(3, 5, mobb);
                    }
                    if (mobe.direction == "Left")
                    {
                        //лево
                        AnimateGriver(0, 2, mobb);
                    }
            }
            if (NitroUsed == true)
            {
                Nitro.Value -= 4;
                if (Nitro.Value <= 0)
                {
                    player2.Speed = 2;
                    NitroUsed = false;
                }
            }
            else
            {
                Nitro.Value += 1;
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - start;
            lblTimer.Content = String.Format("{0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
        }
        public void AnimateGriver(int start, int end, Image griver)
        {
            steps++;

            if (steps > end || steps < start)
            {
                steps = start;
            }
            griver.Source = animations[steps];
        }
        private void GameTick(object sender, EventArgs e)
        {
            collisia.elementsCopy = maincanvas.Children.Cast<UIElement>().ToList(); //передаем список из всех дочерних элементов на канвасе
            lbAmmo.Content = "Ammo: " + PlayerMaze.ammo;
            if (Canvas.GetTop(Character) > maincanvas.ActualHeight)
            {
                GameTimer.Stop();
                griverTimer.Stop();
                player2.UpKeyPressed = false;
                player2.DownKeyPressed = false;
                player2.RightKeyPressed = false;
                player2.LeftKeyPressed = false;
                GamePlay.GameTimer.Start();
                Canvas.SetTop(GamePlay.hero, Canvas.GetTop(GamePlay.hero) + 35);
                NavigationService.GoBack();
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
                griverTimer.Stop();
                timer.Stop();
                TimerSpawn.Stop();
                //Передаем ссылку на текущее окно в конструктор второго окна
                gameOver gOver = new gameOver(spawn);
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
                    lbAmmo.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
            if (e.Key == Key.LeftShift || Nitro.Value < 0)
            {
                if (Nitro.Value >= 10)
                {
                    player2.Speed = 4;
                    NitroUsed = true;
                }
                NitroUsed = false;
                player2.Speed = 2;
            }
            if (e.Key == Key.F)
            {
                isFKeyPressed = false;
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
            if (e.Key == Key.LeftShift && Nitro.Value > 0)
            {
                player2.Speed = 4;
                NitroUsed = true;
            }
            if (e.Key == Key.F && !isFKeyPressed)
            {
                collisia.Code(1, number);
                isFKeyPressed = true;
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
                spawnX = rand.Next(10, (int)maincanvas.Width);
                spawnY = rand.Next(10, (int)maincanvas.Height);


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
        public void DropPinCode()
        {
            double spawnX, spawnY;
            bool isColliding;
            Image code = new Image();
            // Загружаем картинку из ресурсов проекта
            code.Source = new BitmapImage(new Uri($"Code{number}(1).png", UriKind.RelativeOrAbsolute));
            code.Tag = "code";
            code.Height = ((int)Character.Height);
            code.Width = ((int)Character.Width);
            do
            {
                isColliding = false;
                // Генерируем случайные координаты для спавна 
                //spawnX = rand.Next(10, (int)maincanvas.Width);
                //spawnY = rand.Next(10, (int)maincanvas.Height);
                spawnX = Canvas.GetLeft(Character);
                spawnY = Canvas.GetTop(Character);
                Rect codeSpawnArea = new Rect(spawnX, spawnY, code.Width, code.Height);

                // Проверяем столкновение со всеми препятствиями
                foreach (UIElement element in maincanvas.Children)
                {
                    if (element is Rectangle rectangle && (string)rectangle.Tag == "Collide")
                    {
                        Rect rectangleArea = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width, rectangle.Height);
                        if (codeSpawnArea.IntersectsWith(rectangleArea))
                        {
                            isColliding = true;
                            break;
                        }
                    }
                }
            } while (isColliding);
            Canvas.SetTop(code, spawnY);
            Canvas.SetLeft(code, spawnX);
            maincanvas.Children.Add(code);
            Canvas.SetZIndex(code, 1);
            Canvas.SetZIndex(Character, 1);
        }
        public void SpawnTraps()
        {
            double spawnX, spawnY;
            bool isColliding;
            Image trap = new Image();
            // Загружаем картинку из ресурсов проекта
            trap.Source = new BitmapImage(new Uri("Spikes.png", UriKind.RelativeOrAbsolute));
            trap.Tag = "trap";
            trap.Height = (int)Character.Height*2;
            trap.Width = (int)Character.Width*2;
            do
            {
                isColliding = false;
                // Генерируем случайные координаты для спавна пули
                spawnX = rand.Next(10, (int)maincanvas.Width);
                spawnY = rand.Next(10, (int)maincanvas.Height);

                Rect codeSpawnArea = new Rect(spawnX, spawnY, trap.Width, trap.Height);

                // Проверяем столкновение со всеми препятствиями
                foreach (UIElement element in maincanvas.Children)
                {
                    if (element is Rectangle rectangle && (string)rectangle.Tag == "Collide")
                    {
                        Rect rectangleArea = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width, rectangle.Height);
                        if (codeSpawnArea.IntersectsWith(rectangleArea))
                        {
                            isColliding = true;
                            break;
                        }
                    }
                }
            } while (isColliding);
            Canvas.SetTop(trap, spawnY);
            Canvas.SetLeft(trap, spawnX);
            maincanvas.Children.Add(trap);
            Canvas.SetZIndex(trap, 1);
            Canvas.SetZIndex(Character, 1);
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
            //Character.Source = new BitmapImage(new Uri("hero1.png", UriKind.RelativeOrAbsolute));
            //foreach (mob mobe in mobs)
            //{
            //    maincanvas.Children.Remove(mobe.griver);
            //}
            //mobs.Clear(); // Очистите список mobs
            //player2.UpKeyPressed = false;
            //player2.DownKeyPressed = false;
            //player2.LeftKeyPressed = false;
            //player2.RightKeyPressed = false;
            //GameTimer.Stop();
            //griverTimer.Stop();
            //gameOver = false;
        }
    }
}
