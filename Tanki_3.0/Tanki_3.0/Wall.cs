using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Tanki_3._0
{
     partial class MainWindow
    {
        
        class Wall : Cell
        {
            private Image wallImage;
            public Wall(int x, int y) : base(x, y) {
                wallImage = new Image
                {
                    Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/wall.png"))
                };
                wallImage.Width = wallImage.Height = CellSize;

            }
            public override void Draw()
            {
                wallImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
                Level.canvas.Children.Add(wallImage); 
            }
        }
    }
}