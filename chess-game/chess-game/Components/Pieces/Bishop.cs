using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Bishop : Piece
    {
    }

    sealed class BlackBishop : Bishop
    {
        public BlackBishop()
        {
            Content = PieceImageIcon.BlackBishop;
        }
    }

    sealed class WhiteBishop : Bishop
    {
        public WhiteBishop()
        {
            Content = PieceImageIcon.WhiteBishop;
        }
    }
}
