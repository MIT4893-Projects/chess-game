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
    }

    sealed class BlackQueen : Queen
    {
        public BlackQueen()
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackQueen) };
        }
    }

    sealed class WhiteQueen : Queen
    {
        public WhiteQueen()
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteQueen) };
        }
    }
}
