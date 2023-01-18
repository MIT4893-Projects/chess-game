using chess_game.Components;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input.Custom;

namespace chess_game.Components.Pieces
{
    sealed class Rook : Piece
    {
        public Rook(ChessBoardController controller, bool isBlack) : base(controller, isBlack)
        {
            if (isBlack)
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackRook) };
            else
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteRook) };
        }
    }
}