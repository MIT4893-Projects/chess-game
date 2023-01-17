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
            Content = PieceImageIcon.BlackKnight;
        }
    }

    sealed class WhiteKnight : Knight
    {
        public WhiteKnight()
        {
            Content = PieceImageIcon.WhiteKnight;
        }
    }
}
