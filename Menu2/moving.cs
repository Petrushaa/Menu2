using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Menu2
{
    internal class moving
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        private float SpeedX, SpeedY, Friction = 0.77f, Speed = 2f;
        private readonly ImageBrush Player = new ImageBrush(); // Поле для отображения персонажа

        public Move()
        {
            InitializePlayer();
            SetupTimer();
        }

        private void InitializePlayer()
        {
            // Настройка изображения персонажа
            Player.ImageSource = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));

        }

        private void SetupTimer()
        {
            GameTimer.Tick += GameLoop;
            GameTimer.Interval = TimeSpan.FromMilliseconds(16.666); // ~60 FPS
            GameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            Move();
        }

        private void Move()
        {
            if (UpKeyPressed)
            {
                SpeedY += Speed;
            }
            if (RightKeyPressed)
            {
                SpeedX += Speed;
            }
            if (LeftKeyPressed)
            {
                SpeedX -= Speed;
            }
            if (DownKeyPressed)
            {
                SpeedY -= Speed;
            }
            SpeedX = SpeedX * Friction;
            SpeedY = SpeedY * Friction;

            // Обновление позиции персонажа на основе скорости
            // Добавьте здесь логику изменения координат вашего персонажа (например, X += SpeedX; Y += SpeedY;)

        }
    }
}
