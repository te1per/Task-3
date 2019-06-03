using System;

namespace Tanki_3._0
{
     partial class MainWindow
    {
        abstract class Cell
        {
            public int X;
            public int Y;
            protected int CellSize = 40;
            public Cell(int x, int y)
            {
                X = x;
                Y = y; 
            }

            public abstract void Draw();
        }
         
    }
}