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
        Page page;
        Random random = new Random();
        static int WidthMaze = 53;
        static int HeightMaze = 33;
        int[,] Maze = new int[HeightMaze, WidthMaze];  //матрица лабиринта
        Canvas mainCanvas;
        public RandomMaze(Page page, Canvas mainCanvas)
        {
            this.page = page;
            this.mainCanvas = mainCanvas;
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
            int mid = WidthMaze / 2;
            Maze[HeightMaze - 1, mid] = 1;
            Maze[HeightMaze - 1, mid - 1] = 1;
            Maze[HeightMaze - 1, mid + 1] = 1;
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

        private void PaintMaze()       // отрисовка матрицы
        {
            ImageBrush wall = new ImageBrush();
            ImageBrush way = new ImageBrush();
            wall.ImageSource = new BitmapImage(new Uri("Wall1.png", UriKind.Relative));
            way.ImageSource = new BitmapImage(new Uri("Way1.png", UriKind.Relative));
            for (int i = 0; i < HeightMaze; i++)  //брутфорс матрицы лабиринта
            {
                for (int j = 0; j < WidthMaze; j++)
                {
                    double cellWidth = page.Width / WidthMaze;
                    double cellHeight = page.Height / HeightMaze;
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
                            Canvas.SetTop(Wall, i * cellHeight);        // перенос матрицы лабиринта на грид
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
                    }
                }
            }
        }
        public void StartMaze()
        {
            GamePlace();
            CreateControlPoint();
            ChoiceDirections();
            PaintMaze();
        }
    }
}
