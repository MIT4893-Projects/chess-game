using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    sealed class PiecesMoveableCells
    {
        private readonly ChessBoardController Controller;
        private List<List<Piece>> PiecesMatrix { get => Controller.PiecesMatrix; }

        public PiecesMoveableCells(ChessBoardController controller)
        {
            Controller = controller;
        }

        private bool? IsTargetCellIsOpponent(Piece piece, int targetRowIndex, int targetColIndex)
        {
            if (PiecesMatrix[targetRowIndex][targetColIndex] == null)
                return null;
            return piece.IsBlackTeam != PiecesMatrix[targetRowIndex][targetColIndex].IsBlackTeam;
        }

        private HashSet<MoveableCell> BlackPawnMoveableCell(Piece pawnPiece)
        {
            HashSet<MoveableCell> moveableCells = new();

            int PawnRow = pawnPiece.RowPosition;
            int PawnCol = pawnPiece.ColumnPosition;

            // If on bottom edge, can't move to anywhere. So return empty list.
            if (PawnRow == 0)
                return moveableCells;

            // Pawns can only move forward one square at a time, and the cell in front don't have any pieces.
            if (IsTargetCellIsOpponent(pawnPiece, PawnRow + 1, PawnCol) == null)
            {
                moveableCells.Add(new MoveableCell() { Row = PawnRow + 1, Column = PawnCol });

                // If this is the first move, the pawns can move forward up to two squares.
                if (!pawnPiece.FirstMovePerformed)
                {
                    if (IsTargetCellIsOpponent(pawnPiece, PawnRow + 2, PawnCol) == null)
                        moveableCells.Add(new MoveableCell() { Row = PawnRow + 2, Column = PawnCol });
                }
            }

            // Pawns can capture one of two squares diagonally in front of them.
            if (IsTargetCellIsOpponent(pawnPiece, PawnRow + 1, PawnCol - 1) == true)
                moveableCells.Add(new MoveableCell() { Row = PawnRow + 1, Column = PawnCol - 1 });

            if (IsTargetCellIsOpponent(pawnPiece, PawnRow + 1, PawnCol + 1) == true)
                moveableCells.Add(new MoveableCell() { Row = PawnRow + 1, Column = PawnCol + 1 });

            return moveableCells;
        }

        private HashSet<MoveableCell> WhitePawnMoveableCell(Piece pawnPiece) { return new(); }

        public HashSet<MoveableCell> PawnMoveableCells(Piece pawnPiece)
        {
            //
            if (pawnPiece.IsBlackTeam)
                return BlackPawnMoveableCell(pawnPiece);
            return WhitePawnMoveableCell(pawnPiece);
        }
    }
}
