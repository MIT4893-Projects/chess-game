using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Queen : Piece
    {
        public Queen(ChessBoardController controller) : base(controller) { }
    }

    sealed class BlackQueen : Queen
    {
        public BlackQueen(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackQueen) };
        }
    }

    sealed class WhiteQueen : Queen
    {
        public WhiteQueen(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteQueen) };
        }
    }
}
