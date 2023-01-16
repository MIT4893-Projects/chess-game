using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components
{
    internal class ChessBoard : Grid
    {
        public ChessBoard()
        {
            InitRowsAndColumns();
            FillCellsColor();
        }

        private void InitRowsAndColumns()
        {
            for (int i = 0; i < 8; ++i)
            {
                RowDefinitions.Add(new RowDefinition());
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void FillCellsColor()
        {
            SolidColorBrush black = new(Colors.Black);
            SolidColorBrush white = new(Colors.White);

            for (int row = 0; row < RowDefinitions.Count; ++row)
            {
                for (int col = 0; col < ColumnDefinitions.Count; ++col)
                {
                    Rectangle CellBackground =  new();

                    // If this is the white cell
                    if ((row + col) % 2 == 0)
                        CellBackground.Fill = white;
                    else
                        CellBackground.Fill = black;

                    SetRow(CellBackground, row);
                    SetColumn(CellBackground, col);
                }
            }
        }
    }
}
