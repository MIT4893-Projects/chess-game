using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Piece : Button
    {
        public int RowPosition { get; private set; } = 0;
        public int ColumnPosition { get; private set; } = 0;

        public Piece()
        {
            //Style = Application.Current.Resources["PieceButton"] as Style;
        }

        public void SetRowPosition(int value)
        {
            if (0 <= value && value < 8)
                RowPosition = value;
        }

        public void SetColumnPosition(int value)
        {
            if (0 <= value && value < 8)
                ColumnPosition = value;
        }

        public void SetRowColumnPosition(int row, int col)
        {
            SetRowPosition(row);
            SetColumnPosition(col);
        }
    }
}
