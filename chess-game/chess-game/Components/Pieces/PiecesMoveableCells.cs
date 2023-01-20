using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        private bool IsIndexValid(int index)
        {
            return 0 <= index && index < 8;
        }

        private bool IsRowAndColValid(int rowIndex, int columnIndex)
        {
            return IsIndexValid(rowIndex) && IsIndexValid(columnIndex);
        }

        private string GetCellString(int row, int column)
        {
            return new MoveableCell() { Row = row, Column = column }.ToString();
        }

        private bool? IsTargetCellIsOpponent(Piece piece, int targetRowIndex, int targetColIndex)
        {
            if (PiecesMatrix[targetRowIndex][targetColIndex] == null)
                return null;
            return piece.IsBlackTeam != PiecesMatrix[targetRowIndex][targetColIndex].IsBlackTeam;
        }

        private HashSet<string> PawnMoveableCells(Piece pawnPiece)
        {
            HashSet<string> moveableCells = new();

            bool PieceIsBlack = pawnPiece.IsBlackTeam;

            // Black goes down, white goes up by setting this factor.
            int verticalFactor = (PieceIsBlack) ? 1 : -1;

            int PawnRow = pawnPiece.RowPosition;
            int PawnCol = pawnPiece.ColumnPosition;

            // If on bottom edge, can't move to anywhere. So return empty list.
            if (PawnRow == ((PieceIsBlack) ? 7 : 0))
                return moveableCells;

            int FrontSquareRow = PawnRow + 1 * verticalFactor;
            // Pawns can only move forward one square at a time, and the cell in front don't have any pieces.
            if (IsIndexValid(FrontSquareRow))
                if (IsTargetCellIsOpponent(pawnPiece, FrontSquareRow, PawnCol) == null)
                {
                    moveableCells.Add(GetCellString(FrontSquareRow, PawnCol));

                    // If this is the first move, the pawns can move forward up to two squares.
                    if (!pawnPiece.FirstMovePerformed)
                    {
                        if (IsTargetCellIsOpponent(pawnPiece, PawnRow + 2 * verticalFactor, PawnCol) == null)
                            moveableCells.Add(GetCellString(PawnRow + 2 * verticalFactor, PawnCol));
                    }
                }

            // Pawns can capture one of two squares diagonally in front of them.
            int DiagonalLeftSquareCol = PawnCol - 1;
            int DiagonalRightSquareCol = PawnCol + 1;

            if (DiagonalLeftSquareCol >= 0)
                if (IsTargetCellIsOpponent(pawnPiece, FrontSquareRow, DiagonalLeftSquareCol) == true)
                    moveableCells.Add(GetCellString(FrontSquareRow, DiagonalLeftSquareCol));

            if (DiagonalRightSquareCol < 8)
                if (IsTargetCellIsOpponent(pawnPiece, FrontSquareRow, DiagonalRightSquareCol) == true)
                    moveableCells.Add(GetCellString(FrontSquareRow, DiagonalRightSquareCol));

            return moveableCells;
        }

        private HashSet<string> RookMoveableCells(Piece rookPiece)
        {
            HashSet<string> moveableCells = new();

            //! Rook's row and col positions after moves.
            int[] verticalFactors = { 1, -1, 0, 0 };
            int[] horizontalFactors = { 0, 0, 1, -1 };

            for (int factorIdx = 0; factorIdx < verticalFactors.Length; ++factorIdx)
            {
                int targetCellRowPosition = rookPiece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = rookPiece.ColumnPosition + horizontalFactors[factorIdx];
                bool canContinueMove = true;
                while (IsRowAndColValid(targetCellRowPosition, targetCellColPosition) && canContinueMove)
                {
                    switch (IsTargetCellIsOpponent(rookPiece, targetCellRowPosition, targetCellColPosition))
                    {
                        case true:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            canContinueMove = false;
                            break;
                        case false:
                            canContinueMove = false;
                            break;
                        default:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            break;
                    }
                    targetCellRowPosition += verticalFactors[factorIdx];
                    targetCellColPosition += horizontalFactors[factorIdx];
                }
            }

            return moveableCells;
        }

        private HashSet<string> KnightMoveableCells(Piece knightPiece)
        {
            HashSet<string> moveableCells = new();

            //! Knight's row and col positions after move.
            int[] horizontalFactors = { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] verticalFactors = { 1, -1, 2, -2, 2, -2, 1, -1 };

            for (int factorIdx = 0; factorIdx < horizontalFactors.Length; ++factorIdx)
            {
                int targetCellRowPosition = knightPiece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = knightPiece.ColumnPosition + horizontalFactors[factorIdx];

                if (IsRowAndColValid(targetCellRowPosition, targetCellColPosition))
                {
                    switch (IsTargetCellIsOpponent(knightPiece, targetCellRowPosition, targetCellColPosition))
                    {
                        case true:
                        case null:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            break;
                        case false:
                            break;
                    }
                }
            }

            return moveableCells;
        }

        private HashSet<string> BishopMoveableCells(Piece bishopPiece)
        {
            HashSet<string> moveableCells = new();

            //! Bishop's row and col positions after moves.
            int[] verticalFactors = { 1, -1, -1, 1 };
            int[] horizontalFactors = { 1, -1, 1, -1 };

            for (int factorIdx = 0; factorIdx < verticalFactors.Length; ++factorIdx)
            {
                bool canContinueMove = true;
                int targetCellRowPosition = bishopPiece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = bishopPiece.ColumnPosition + horizontalFactors[factorIdx];
                while (IsRowAndColValid(targetCellRowPosition, targetCellColPosition) && canContinueMove)
                {
                    switch (IsTargetCellIsOpponent(bishopPiece, targetCellRowPosition, targetCellColPosition))
                    {
                        case true:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            canContinueMove = false;
                            break;
                        case false:
                            canContinueMove = false;
                            break;
                        default:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            break;
                    }
                    targetCellRowPosition += verticalFactors[factorIdx];
                    targetCellColPosition += horizontalFactors[factorIdx];
                }
            }

            return moveableCells;
        }

        private HashSet<string> KingMoveableCells(Piece kingPiece)
        {
            HashSet<string> moveableCells = new();

            //! King's row and col positions after moves.
            int[] verticalFactors = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] horizontalFactors = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int factorIdx = 0; factorIdx < verticalFactors.Length; ++factorIdx)
            {
                int targetCellRowPosition = kingPiece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = kingPiece.ColumnPosition + horizontalFactors[factorIdx];

                if (IsRowAndColValid(targetCellRowPosition, targetCellColPosition))
                {
                    switch (IsTargetCellIsOpponent(kingPiece, targetCellRowPosition, targetCellColPosition))
                    {
                        case true:
                        case null:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            break;
                        case false:
                            break;
                    }
                }
            }

            return moveableCells;
        }

        private HashSet<string> QueenMoveableCells(Piece queenPiece)
        {
            HashSet<string> moveableCells = new();

            //! Queen's row and col positions after moves.
            int[] verticalFactors = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] horizontalFactors = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int factorIdx = 0; factorIdx < verticalFactors.Length; ++factorIdx)
            {
                bool canContinueMove = true;
                int targetCellRowPosition = queenPiece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = queenPiece.ColumnPosition + horizontalFactors[factorIdx];

                while (IsRowAndColValid(targetCellRowPosition, targetCellColPosition) && canContinueMove)
                {
                    switch (IsTargetCellIsOpponent(queenPiece, targetCellRowPosition, targetCellColPosition))
                    {
                        case true:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            canContinueMove = false;
                            break;
                        case false:
                            canContinueMove = false;
                            break;
                        default:
                            moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                            break;
                    }
                    targetCellRowPosition += verticalFactors[factorIdx];
                    targetCellColPosition += horizontalFactors[factorIdx];
                }
            }

            return moveableCells;
        }

        public HashSet<string> PieceMoveableCells(Piece pieceIsRequestingToMove)
        {
            // Type tester
            return pieceIsRequestingToMove switch
            {
                Pawn _ => PawnMoveableCells(pieceIsRequestingToMove),
                Rook _ => RookMoveableCells(pieceIsRequestingToMove),
                Knight _ => KnightMoveableCells(pieceIsRequestingToMove),
                Bishop _ => BishopMoveableCells(pieceIsRequestingToMove),
                King _ => KingMoveableCells(pieceIsRequestingToMove),
                //! Queen (default case).
                _ => QueenMoveableCells(pieceIsRequestingToMove),
            };
        }

        public HashSet<string> PieceMoveableCells(int pieceIsWaitingRow, int pieceIsWaitingCol)
        {
            return PieceMoveableCells(PiecesMatrix[pieceIsWaitingRow][pieceIsWaitingCol]);
        }
    }
}
