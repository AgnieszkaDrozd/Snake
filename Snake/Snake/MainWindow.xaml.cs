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

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly int SIZE = 10;
        private SnakeObject _snake;


        public MainWindow()
        {
            InitializeComponent();

        }

        void InitBoard()
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
    }
}
