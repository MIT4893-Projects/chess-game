using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    sealed class King : Piece
    {
        public King(ChessBoardController controller, PiecesMoveableCells MoveableCellsMarker, bool isBlack)
            : base(controller, MoveableCellsMarker, isBlack)
        {
            if (isBlack)
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackKing) };
            else
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteKing) };
        }
    }
}
