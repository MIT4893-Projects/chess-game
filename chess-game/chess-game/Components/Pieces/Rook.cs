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
    internal class Rook : Piece
    {
        public Rook(ChessBoardController controller) : base(controller) { }
    }

    sealed class BlackRook : Rook
    {
        public BlackRook(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackRook) };
        }
    }

    sealed class WhiteRook : Rook
    {
        public WhiteRook(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhiteRook) };
        }
    }
}