using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly int SIZE = 10;
        private SnakeObject _snake;
        private int _directionX = 1;
        private int _directionY = 0;
        private DispatcherTimer _timer;
        private SnakeFragment _food;
        private int _FragmentsAdd;


        public MainWindow()
        {
            InitializeComponent();

            initBoard();
            initSnake();
            initTimer();
            initFood();
        }

        void initBoard()
        {
            for (int i = 0; i < grid.Width / SIZE; i++)
            {
                ColumnDefinition columnDefinitions = new ColumnDefinition();
                columnDefinitions.Width = new GridLength(SIZE);
                grid.ColumnDefinitions.Add(columnDefinitions);
            }

            for (int j = 0; j < grid.Height / SIZE; j++)
            {
                RowDefinition rowDefinitions = new RowDefinition();
                rowDefinitions.Height = new GridLength(SIZE);
                grid.RowDefinitions.Add(rowDefinitions);

            }

            _snake = new SnakeObject();

        }

        void initSnake()
        {
            grid.Children.Add(_snake.Head.Rect);
            foreach (SnakeFragment snakeFragment in _snake.Fragments)
                grid.Children.Add(snakeFragment.Rect);
            _snake.DrawSnake();
        }

        private void SnakeMove()
        {
            for (int i = _snake.Fragments.Count - 1; i >= 1; i--)
            {
                _snake.Fragments[i].X = _snake.Fragments[i - 1].X;
                _snake.Fragments[i].Y = _snake.Fragments[i - 1].Y;
            }

            _snake.Fragments[0].X = _snake.Head.X;
            _snake.Fragments[0].Y = _snake.Head.Y;
            _snake.Head.X += _directionX;
            _snake.Head.Y += _directionY;
            _snake.DrawSnake();

            if (FoodCheck())
                DrawFood();
        }

        void initTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Start();

        }

        void _timer_Tick(object sender, EventArgs e)
        {
            SnakeMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _directionX = -1;
                _directionY = 0;
            }

            if (e.Key == Key.Right)
            {
                _directionX = 1;
                _directionY = 0;
            }

            if (e.Key == Key.Up)
            {
                _directionX = 0;
                _directionY = -1;
            }

            if (e.Key == Key.Down)
            {
                _directionX = 0;
                _directionY = 1;
            }
        }

        void initFood()
        {
            _food = new SnakeFragment(10, 10);
            _food.Rect.Width = _food.Rect.Height = 10;
            _food.Rect.Fill = Brushes.Aqua;
            grid.Children.Add(_food.Rect);
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);

        }

        private bool FoodCheck()
        {
            Random rand = new Random();
            if (_snake.Head.X == _food.X && _snake.Head.Y == _food.Y)
            {
                _FragmentsAdd += 20;
                for (int i = 0; i < 20; i++)
                {
                    int x = rand.Next(0, (int)(grid.Width / SIZE));
                    int y = rand.Next(0, (int)(grid.Height / SIZE));
                    if (FreeField(x, y))
                    {
                        _food.X = x;
                        _food.Y = y;
                        return true;

                    }
                }

                for (int i = 0; i < grid.Width / SIZE; i++)
                    for (int j = 0; j < grid.Height / SIZE; j++)
                    {
                        if (FreeField(i, j))
                        {
                            _food.X = i;
                            _food.Y = j;
                            return true;
                        }
                    }
                End();

          }

            return false;
        }

        private bool FreeField(int x, int y)
        {
            if (_snake.Head.X == x && _snake.Head.Y == y)
                return false;

            foreach (SnakeFragment snakeFragment in _snake.Fragments)
            {
                if (snakeFragment.X == x && snakeFragment.Y == y)
                    return false;        
            }
            return true;
        }

        void End()
        {
            _timer.Stop();
            MessageBox.Show("KONIEC GRY");
        }

        private void DrawFood()

        {
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);
        }

    }

}
