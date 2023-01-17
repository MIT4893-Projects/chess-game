using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Rook : Piece
    {
    }

    sealed class BlackRook : Rook
    {
        public BlackRook()
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackRook) };
        }
    }

    sealed class WhiteRook : Rook
    {
        public WhiteRook()
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteRook) };
        }
    }
}
