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
            int[] verticalFactors = { 1, -1, 0, 0 };
            int[] horizontalFactors = { 0, 0, 1, -1 };

            for (int factorIdx = 0; factorIdx < verticalFactors.Length; ++factorIdx)
            {
                int rowIndex = rookPiece.RowPosition + verticalFactors[factorIdx];
                int colIndex = rookPiece.ColumnPosition + horizontalFactors[factorIdx];
                bool canContinueMove = true;
                while (IsRowAndColValid(rowIndex, colIndex) && canContinueMove)
                {
                    switch (IsTargetCellIsOpponent(rookPiece, rowIndex, colIndex))
                    {
                        case true:
                            moveableCells.Add(GetCellString(rowIndex, colIndex));
                            canContinueMove = false;
                            break;
                        case false:
                            canContinueMove = false;
                            break;
                        default:
                            moveableCells.Add(GetCellString(rowIndex, colIndex));
                            break;
                    }
                    rowIndex += verticalFactors[factorIdx];
                    colIndex += horizontalFactors[factorIdx];
                }
            }

            return moveableCells;
        }

        private HashSet<string> KnightMoveableCells(Piece rookPiece)
        {
            HashSet<string> moveableCells = new();

            return moveableCells;
        }

        private HashSet<string> BishopMoveableCells(Piece rookPiece)
        {
            HashSet<string> moveableCells = new();

            return moveableCells;
        }

        private HashSet<string> KingMoveableCells(Piece rookPiece)
        {
            HashSet<string> moveableCells = new();

            return moveableCells;
        }

        private HashSet<string> QueenMoveableCells(Piece rookPiece)
        {
            HashSet<string> moveableCells = new();

            return moveableCells;
        }

        public HashSet<string> PieceMoveableCells(Piece pieceIsRequestingToMove)
        {
            // Type tester
            switch (pieceIsRequestingToMove)
            {
                case Pawn _:
                    return PawnMoveableCells(pieceIsRequestingToMove);
                case Rook _:
                    return RookMoveableCells(pieceIsRequestingToMove);
                case Knight _:
                    return KnightMoveableCells(pieceIsRequestingToMove);
                case Bishop _:
                    return BishopMoveableCells(pieceIsRequestingToMove);
                case King _:
                    return KingMoveableCells(pieceIsRequestingToMove);
                case Queen _:
                    return QueenMoveableCells(pieceIsRequestingToMove);
            }
            return new();
        }

        public HashSet<string> PieceMoveableCells(int pieceIsWaitingRow, int pieceIsWaitingCol)
        {
            return PieceMoveableCells(PiecesMatrix[pieceIsWaitingRow][pieceIsWaitingCol]);
        }
    }
}
