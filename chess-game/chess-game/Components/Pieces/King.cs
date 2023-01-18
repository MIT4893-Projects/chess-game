using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class King : Piece
    {
        public King(ChessBoardController controller) : base(controller) { }
    }

    sealed class BlackKing : King
    {
        public BlackKing(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackKing) };
        }
    }

    sealed class WhiteKing : King
    {
        public WhiteKing(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteKing) };
        }
    }
}
