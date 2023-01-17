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
            Content = PieceImageIcon.BlackRook;
        }
    }

    sealed class WhiteRook : Rook
    {
        public WhiteRook()
        {
            Content = PieceImageIcon.WhiteRook;
        }
    }
}
