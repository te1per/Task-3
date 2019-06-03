using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Tanki_3._0
{
     partial class MainWindow
    {
        class DestroyedObject : Cell
        {
            private Image destroImage;
            public DestroyedObject(int x, int y) : base(x, y) {
                destroImage = new Image
                {
                    Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/exposion.png"))
                };
                destroImage.Width = destroImage.Height = CellSize;

            }
            public override void Draw()
            {
                destroImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
                Level.canvas.Children.Add(destroImage);
            }
        }
    }
}