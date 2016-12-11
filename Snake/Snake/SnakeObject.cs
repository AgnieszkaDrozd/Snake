using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake
{
    class SnakeObject
    {
        public SnakeFragment Head { get; private set; }
        public List<SnakeFragment> Fragments { get; private set; }

        public SnakeObject()
        {
            Head = new SnakeFragment(20, 0);
            Head.Rect.Width = Head.Rect.Height = 10;
            Head.Rect.Fill = System.Windows.Media.Brushes.DarkGreen;

            Fragments = new List<SnakeFragment>();
            Fragments.Add(new SnakeFragment(19, 0));
            Fragments.Add(new SnakeFragment(18, 0));
            Fragments.Add(new SnakeFragment(17, 0));
            Fragments.Add(new SnakeFragment(16, 0));
            Fragments.Add(new SnakeFragment(15, 0));
            Fragments.Add(new SnakeFragment(14, 0));
            Fragments.Add(new SnakeFragment(13, 0));
            Fragments.Add(new SnakeFragment(12, 0));
            Fragments.Add(new SnakeFragment(11, 0));
            Fragments.Add(new SnakeFragment(10, 0));
        }

        public void DrawSnake()
        {
            Grid.SetColumn(Head.Rect, Head.X);
            Grid.SetRow(Head.Rect, Head.Y);

            foreach (SnakeFragment snakeFragment in Fragments)
            {
                Grid.SetColumn(snakeFragment.Rect, snakeFragment.X);
                Grid.SetRow(snakeFragment.Rect, snakeFragment.Y);
            }

        }


    }
}
