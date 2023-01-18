using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Pawn : Piece
    {
        public Pawn(ChessBoardController controller) : base(controller) { }
    }

    sealed class BlackPawn : Pawn
    {
        public BlackPawn(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackPawn) };
        }
    }

    sealed class WhitePawn : Pawn
    {
        public WhitePawn(ChessBoardController controller) : base(controller)
        {
            Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhitePawn) };
        }
    }
}
