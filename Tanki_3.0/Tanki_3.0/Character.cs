using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tanki_3._0
{
     partial class MainWindow
    {
        enum Direction
        {
            Down,
            Left,
            Up,
            Right
        }

        class Character : Cell
        {
            protected Image image;

            protected Direction direction;
            public int Lifes;
            
            public Direction CurrentDirection
            {
                get { return direction; }
            }

            public Character(int x, int y) : base(x, y)
            {
                direction = Direction.Down;
                X = x;
                Y = y;

                image = new Image();
                image.Width = image.Height = CellSize;
            }

            public override void Draw()
            {
                RotateTransform rotate = new RotateTransform((int)direction * 90)
                {
                    CenterX = CellSize / 2,
                    CenterY = CellSize / 2
                };

                image.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
                image.RenderTransform = rotate;
                Level.canvas.Children.Add(image);
            }

            public void Rotate(int dx, int dy)
            {
                direction = NewRotate(dx, dy);
            }
            
            public Direction NewRotate (int dx, int dy)
            {
                if (dx == -1)
                    return Direction.Left;
                else if (dx == 1)
                    return Direction.Right;
                else if (dy == -1)
                    return Direction.Up;
                else if (dy == 1)
                    return Direction.Down;
                return direction;
            }
            public void MakeStep(int dx, int dy)
            {
                if (direction == NewRotate(dx, dy))
                {
                    X += dx;
                    Y += dy;
                }
                
            }
        }
    }
}