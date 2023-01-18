using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    sealed class Queen : Piece
    {
        public Queen(ChessBoardController controller, bool isBlack) : base(controller, isBlack)
        {
            if (isBlack)
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackQueen) };
            else
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteQueen) };
        }
    }
}
