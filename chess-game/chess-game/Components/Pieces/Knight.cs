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
    }

    sealed class BlackKnight : Knight
    {
        public BlackKnight()
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackKnight) };
        }
    }

    sealed class WhiteKnight : Knight
    {
        public WhiteKnight()
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteKnight) };
        }
    }
}
