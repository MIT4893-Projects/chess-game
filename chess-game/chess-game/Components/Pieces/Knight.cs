using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Knight : Piece
    {
        public Knight(ChessBoardController controller) : base(controller) { }
    }

    sealed class BlackKnight : Knight
    {
        public BlackKnight(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackKnight) };
        }
    }

    sealed class WhiteKnight : Knight
    {
        public WhiteKnight(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteKnight) };
        }
    }
}
