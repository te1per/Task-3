using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tanki_3._0
{
     partial class MainWindow
    {
        class Player : Character
        {
            public Player(int x, int y) : base(x, y)
            {
                Lifes = 5;
                image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/player.png"));
            }

        }
    }
}