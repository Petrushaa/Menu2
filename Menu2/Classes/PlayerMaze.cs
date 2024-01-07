﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Menu2.Classes
{
    internal class PlayerMaze
    {
        public bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        public float SpeedX, SpeedY, Friction;
        public Image Character;
        public static string facing = "up";
        private Canvas canvas;
        public static int ammo = 10;
        public CollisiaMaze collisia;
        public int Health = 1000;
        public float Speed = 2f;
        public PlayerMaze(Canvas canvas, Image Character, CollisiaMaze collisia, float SpeedY = 0, float SpeedX = 0, bool UpKeyPressed = false, bool DownKeyPressed = false, bool LeftKeyPressed = false, bool RightKeyPressed = false, float Friction = 0.77f)
        {
            this.SpeedY = SpeedY;
            this.SpeedX = SpeedX;
            this.Friction = Friction;
            this.UpKeyPressed = UpKeyPressed;
            this.DownKeyPressed = DownKeyPressed;
            this.LeftKeyPressed = LeftKeyPressed;
            this.RightKeyPressed = RightKeyPressed;
            this.Character = Character;
            this.canvas = canvas;
            this.collisia = collisia;
        }
        public void Move()
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
            Canvas.SetLeft(canvas, Canvas.GetLeft(canvas) - SpeedX);
            Canvas.SetLeft(Character, Canvas.GetLeft(Character) + SpeedX);
            collisia.Collide("x");
            Canvas.SetTop(canvas, Canvas.GetTop(canvas) + SpeedY);
            Canvas.SetTop(Character, Canvas.GetTop(Character) - SpeedY);
            collisia.Collide("y");
        }
        public void KeyboardUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.W )
            {
                UpKeyPressed = false;
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = false;
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = false;
            }
        }
        public void KeyBoardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = true;
                facing = "up";
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = true;
                facing = "down";
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = true;
                facing = "left";
                Character.Source = new BitmapImage(new Uri("characterLeft.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = true;
                Character.Source = new BitmapImage(new Uri("characterRight.png", UriKind.RelativeOrAbsolute));
                facing = "right";
            }
        }
    }
}
