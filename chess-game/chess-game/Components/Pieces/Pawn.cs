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
    sealed class Pawn : Piece
    {
        public Pawn(ChessBoardController controller, PiecesMoveableCells MoveableCellsMarker, bool isBlack)
            : base(controller, MoveableCellsMarker, isBlack)
        {
            if (isBlack)
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.BlackPawn) };
            else
                Content = new Image() { Source = new BitmapImage(PieceImageIcon.WhitePawn) };
        }
    }
}
