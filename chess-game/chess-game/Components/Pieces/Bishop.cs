using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(ChessBoardController controller) : base(controller) { }
    }

    sealed class BlackBishop : Bishop
    {
        public BlackBishop(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackBishop) };
        }
    }

    sealed class WhiteBishop : Bishop
    {
        public WhiteBishop(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteBishop) };
        }
    }
}
