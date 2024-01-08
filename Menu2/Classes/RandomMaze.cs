using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Menu2.Classes
{
    internal class RandomMaze
    {
        Random random = new Random();
        static int WidthMaze = 53;
        static int HeightMaze = 33;
        internal int[,] Maze = new int[HeightMaze, WidthMaze];  //матрица лабиринта
        public Canvas mainCanvas;
        string direction;

        public RandomMaze(Canvas mainCanvas, string direction)
        {
            this.mainCanvas = mainCanvas;
            this.direction = direction;
        }

        private void CreateControlPoint()   // создание контрольных точек для формирования стен
        {
            for (int i = 2; i < HeightMaze - 2; i += 2)
            {
                for (int j = 2; j < WidthMaze - 2; j += 2)
                {
                    Maze[i, j] = 2;
                }
            }
        }

        private void GamePlace()    //игровое поле 29 на 39 //отброс крайних стен
        {
            for (int i = 1; i < HeightMaze - 1; i++)
            {
                for (int j = 1; j < WidthMaze - 1; j++)
                {
                    Maze[i, j] = 1;
                }
            }
        }

        private void CellExit(String direction)
        {
            if (direction == "Вверх")   //относительно спавна
            {
                int mid = WidthMaze / 2;
                Maze[HeightMaze - 1, mid] = 1;
                Maze[HeightMaze - 1, mid - 1] = 1;
                Maze[HeightMaze - 1, mid + 1] = 1;
            }
            if (direction == "Вниз")   //относительно спавна
            {
                int mid = WidthMaze / 2;
                Maze[0, mid] = 1;
                Maze[0, mid - 1] = 1;
                Maze[0, mid + 1] = 1;
            }
            if (direction == "Вправо")   //относительно спавна
            {
                int mid = HeightMaze / 2;
                Maze[mid, 0] = 1;
                Maze[mid + 1, 0] = 1;
                Maze[mid - 1, 0] = 1;
            }
            if (direction == "Влево")   //относительно спавна
            {
                int mid = HeightMaze / 2;
                Maze[mid, WidthMaze - 1] = 1;
                Maze[mid + 1, WidthMaze - 1] = 1;
                Maze[mid - 1, WidthMaze - 1] = 1;
            }
        }

        private List<int> Directions = new List<int>() { 0, 1, 2, 3 };  // список направлений
        private void ChoiceDirections()
        {
            for (int i = 0; i < HeightMaze; i++)
            {
                for (int j = 0; j < WidthMaze; j++)
                {
                    if (Maze[i, j] == 2)
                    {
                        switch (random.Next(Directions.Count))             // 1 - стена
                        {                                                  // 2 - коридор
                            case 0:     //Вверх

                                int x1 = i;
                                int y1 = j;

                                do
                                {
                                    Maze[x1, y1] = 0;
                                    x1--;
                                    if (x1 % 2 == 1)
                                    {
                                        if (random.Next(0, 4) == 0)
                                        {
                                            break;
                                        }
                                    }
                                } while (!(Maze[x1, y1] == 0) && x1 > 0);
                                break;

                            case 1:     //Вправо

                                int x2 = i;
                                int y2 = j;

                                do
                                {
                                    Maze[x2, y2] = 0;
                                    y2++;
                                    if (y2 % 2 == 1)
                                    {
                                        if (random.Next(0, 4) == 0)
                                        {
                                            break;
                                        }
                                    }
                                } while (!(Maze[x2, y2] == 0) && y2 < WidthMaze - 1);
                                break;

                            case 2:     //Вниз

                                int x3 = i;
                                int y3 = j;

                                do
                                {
                                    Maze[x3, y3] = 0;
                                    x3++;
                                    if (x3 % 2 == 1)
                                    {
                                        if (random.Next(0, 4) == 0)
                                        {
                                            break;
                                        }
                                    }
                                } while (!(Maze[x3, y3] == 0) && x3 < HeightMaze - 1);
                                break;

                            case 3:     //Влево

                                int x4 = i;
                                int y4 = j;

                                do
                                {
                                    Maze[x4, y4] = 0;
                                    y4--;
                                    if (y4 % 2 == 1)
                                    {
                                        if (random.Next(0, 4) == 0)
                                        {
                                            break;
                                        }
                                    }
                                } while (!(Maze[x4, y4] == 0) && y4 > 0);    //пока не нашел стену
                                break;
                        }
                    }
                }
            }
        }
        private void RandomPoint()
        {
            List<Point> emptyCells = new List<Point>();

            // Сначала соберем список всех пустых клеток.
            for (int i = 1; i < HeightMaze - 1; i++)
            {
                for (int j = 1; j < WidthMaze - 1; j++)
                {
                    if (Maze[i, j] == 0)
                    {
                        emptyCells.Add(new Point(i, j));
                    }
                }
            }

            // Теперь, если есть пустые клетки, выберем одну из них случайно.
            if (emptyCells.Count > 0)
            {
                int ind = random.Next(emptyCells.Count);
                Point randomPoint = emptyCells[ind];
                Maze[Convert.ToInt32(randomPoint.X), Convert.ToInt32(randomPoint.Y)] = 2;
            }
        }


        public double cellWidth { get; private set; }
        public double cellHeight { get; private set; }
        private void PaintMaze()       // отрисовка матрицы
        {
            ImageBrush wall = new ImageBrush();
            ImageBrush way = new ImageBrush();
            wall.ImageSource = new BitmapImage(new Uri("Wall.png", UriKind.Relative));
            way.ImageSource = new BitmapImage(new Uri("Way.png", UriKind.Relative));
            cellWidth = mainCanvas.Width / WidthMaze;
            cellHeight = mainCanvas.Height / HeightMaze;
            for (int i = 0; i < HeightMaze; i++)  //брутфорс матрицы лабиринта
            {
                for (int j = 0; j < WidthMaze; j++)
                {
                    switch (Maze[i, j])
                    {
                        case 0:
                            Rectangle Wall = new Rectangle()    // стена
                            {
                                Tag = "Collide",
                                Fill = wall,
                                Width = cellWidth + 1,   //размер ячейки
                                Height = cellHeight + 1,
                            };
                            Canvas.SetLeft(Wall, j * cellWidth);       //
                            Canvas.SetTop(Wall, i * cellHeight);        // перенос матрицы лабиринта на холст
                            mainCanvas.Children.Add(Wall);      //
                            break;

                        case 1:
                            Rectangle Way = new Rectangle()    // стена
                            {

                                Fill = way,
                                Width = cellWidth + 1,   //размер ячейки
                                Height = cellHeight + 1,
                            };
                            Canvas.SetLeft(Way, j * cellWidth);       //
                            Canvas.SetTop(Way, i * cellHeight);        // перенос матрицы лабиринта на грид
                            mainCanvas.Children.Add(Way);      //
                            break;

                        case 2:
                            Rectangle Wall1 = new Rectangle()    // стена
                            {
                                Tag = "Exit",
                                Fill = way,
                                Width = cellWidth + 1,   //размер ячейки
                                Height = cellHeight + 1,
                            };
                            Canvas.SetLeft(Wall1, j * cellWidth);       //
                            Canvas.SetTop(Wall1, i * cellHeight);        // перенос матрицы лабиринта на холст
                            mainCanvas.Children.Add(Wall1);      //
                            break;
                    }
                }
            }
        }

        public void StartMaze()
        {
            CellExit(direction);
            GamePlace();
            CreateControlPoint();
            ChoiceDirections();
            RandomPoint();
            PaintMaze();
        }
    }
}