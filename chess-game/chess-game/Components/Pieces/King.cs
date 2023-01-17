using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class King : Piece
    {
    }

    sealed class BlackKing : King
    {
        public BlackKing()
        {
            Content = PieceImageIcon.BlackKing;
        }
    }

    sealed class WhiteKing : King
    {
        public WhiteKing()
        {
            Content = PieceImageIcon.WhiteKing;
        }
    }
}
