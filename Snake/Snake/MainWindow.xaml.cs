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

        void initBoard() //tworzymy planszę
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

        void initSnake() //tworzymy węża
        {
            grid.Children.Add(_snake.Head.Rect);
            foreach (SnakeFragment snakeFragment in _snake.Fragments)
                grid.Children.Add(snakeFragment.Rect);
            _snake.DrawSnake();
        }

        private void SnakeMove() //poruszanie się
        {
            int snakeFragmentCount = _snake.Fragments.Count;
            if (_FragmentsAdd > 0)
            {
                SnakeFragment newFragment = new SnakeFragment(_snake.Fragments[_snake.Fragments.Count - 1].X,
                    _snake.Fragments[_snake.Fragments.Count - 1].Y);
                grid.Children.Add(newFragment.Rect);
                _snake.Fragments.Add(newFragment);
                _FragmentsAdd--;
            }
            for (int i = snakeFragmentCount - 1; i >= 1; i--)
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

        void initTimer() //czas
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Start();

        }

        void _timer_Tick(object sender, EventArgs e) //ruch węża w czasie
        {
            SnakeMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        //poruszanie się za pomocą strzałek
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

        void initFood() //tworzymy jedzenie
        {
            _food = new SnakeFragment(10, 10);
            _food.Rect.Width = _food.Rect.Height = 10;
            _food.Rect.Fill = Brushes.Aqua;
            grid.Children.Add(_food.Rect);
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);

        }

        private bool FoodCheck() //sprawdzamy pojawienie się jedzenia
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

        void End() //koniec gry
        {
            _timer.Stop();
            MessageBox.Show("KONIEC GRY");
        }

        private void DrawFood()

        {
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);
        }


        bool CollisionCheck()
        {
            if (CollisionCheckBorder())
                return true;
            if (CollisionCheckSnake())
                return true;
            return false;

        }

        bool CollisionCheckBorder()
        {
            if (_snake.Head.X < 0 || _snake.Head.X > grid.Width / SIZE)
                return true;
            if (_snake.Head.Y < 0 || _snake.Head.Y > grid.Height / SIZE)
                return true;
            return false;

        }

        bool CollisionCheckSnake()
        {
            foreach (SnakeFragment snakeFragment in _snake.Fragments)
            {
                if (_snake.Head.X == snakeFragment.X && _snake.Head.Y == snakeFragment.Y)
                    return true;
            }
            return false;
        }


    }

}
