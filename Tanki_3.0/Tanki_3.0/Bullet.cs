using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tanki_3._0
{
    partial class MainWindow
    {
        class Bullet : Character
        {
            public bool EnemySource;
            
            public Bullet(int x, int y, Direction direction, bool enemySource = false) 
                : base(x, y)
            {
                EnemySource = enemySource;
                this.direction = direction;

                image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/bullet.png"));
            }

            public void ChooseDirection(out int x, out int y) {
                x = y = 0;
                switch (direction)
                {
                    case Direction.Up:
                        y--;
                        break;
                    case Direction.Down:
                        y++;
                        break;
                    case Direction.Left:
                        x--;
                        break;
                    case Direction.Right:
                        x++;
                        break;
                }
            }
            public void MakeStep()
            {
                int dx, dy;
                ChooseDirection(out dx, out dy);
                X += dx;
                Y += dy;
            }
            
        }
    }
}