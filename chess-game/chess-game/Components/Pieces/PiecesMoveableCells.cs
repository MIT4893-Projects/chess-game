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

        #region Initialize

        public PiecesMoveableCells(ChessBoardController controller)
        {
            Controller = controller;
        }

        #endregion

        /// <summary>
        /// Check a index is valid.
        /// </summary>
        /// <param name="index">Index to check</param>
        /// <returns>A boolean represents is index valids or not</returns>
        private bool IsIndexValid(int index)
        {
            return 0 <= index && index < 8;
        }

        /// <summary>
        /// Check a row and column index are valid, based on IsIndexValid.
        /// </summary>
        /// <param name="rowIndex">Row's index to check</param>
        /// <param name="columnIndex">Column's index to check</param>
        /// <returns>A boolean represents are row and column indexes are both valid</returns>
        private bool IsRowAndColValid(int rowIndex, int columnIndex)
        {
            return IsIndexValid(rowIndex) && IsIndexValid(columnIndex);
        }

        /// <summary>
        /// Convert a cell's row index and column index to string for store in HashSet.
        /// </summary>
        /// <param name="row">Cell's row index</param>
        /// <param name="column">Cell's column index</param>
        /// <returns></returns>
        private string GetCellString(int row, int column)
        {
            return $"{row} {column}";
        }

        /// <summary>
        /// Check a target cell is opponent or teammate or empty.
        /// </summary>
        /// <param name="piece">Piece to get the team information to check.</param>
        /// <param name="targetRowIndex">Target row's index</param>
        /// <param name="targetColIndex">Target column's index</param>
        /// <returns>True when target cell is opponent, false when target cell is teammate, null when target cell is empty</returns>
        private bool? IsTargetCellIsOpponent(Piece piece, int targetRowIndex, int targetColIndex)
        {
            if (PiecesMatrix[targetRowIndex][targetColIndex] == null)
                return null;
            return piece.IsBlackTeam != PiecesMatrix[targetRowIndex][targetColIndex].IsBlackTeam;
        }

        /// <summary>
        /// Add a move for a far move piece.
        /// </summary>
        /// <param name="moveableCells">HashSet stores cells the piece can move to</param>
        /// <param name="piece">Piece is waiting for move</param>
        /// <param name="rowIndex">Target cell's row index</param>
        /// <param name="colIndex">Target cell's column index</param>
        /// <returns></returns>
        private bool FarMovePieceCellCheck(HashSet<string> moveableCells, Piece piece, int rowIndex, int colIndex)
        {
            if (IsRowAndColValid(rowIndex, colIndex))
            {
                switch (IsTargetCellIsOpponent(piece, rowIndex, colIndex))
                {
                    case true:
                        moveableCells.Add(GetCellString(rowIndex, colIndex));
                        return false;
                    case false:
                        return false;
                    default:
                        moveableCells.Add(GetCellString(rowIndex, colIndex));
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Add a move for a short move piece.
        /// </summary>
        /// <param name="moveableCells">HashSet stores cells the piece can move to</param>
        /// <param name="piece">Piece is waiting for move</param>
        /// <param name="rowIndex">Target cell's row index</param>
        /// <param name="colIndex">Target cell's column index</param>
        private void ShortMovePieceCellCheck(HashSet<string> moveableCells, Piece piece, int rowIndex, int colIndex)
        {
            if (IsRowAndColValid(rowIndex, colIndex))
            {
                switch (IsTargetCellIsOpponent(piece, rowIndex, colIndex))
                {
                    case true:
                    case null:
                        moveableCells.Add(GetCellString(rowIndex, colIndex));
                        return;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Return a HashSet contains all cell the piece can move to.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="verticalFactors"></param>
        /// <param name="horizontalFactors"></param>
        /// <returns></returns>
        private HashSet<string> MakeListOfCellFarMovePiece(Piece piece, int[] verticalFactors, int[] horizontalFactors)
        {
            HashSet<string> moveableCells = new();

            for (int factorIdx = 0; factorIdx < verticalFactors.Length; ++factorIdx)
            {
                int targetCellRowPosition = piece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = piece.ColumnPosition + horizontalFactors[factorIdx];
                bool canContinueMove = true;
                while (IsRowAndColValid(targetCellRowPosition, targetCellColPosition) && canContinueMove)
                {
                    canContinueMove = FarMovePieceCellCheck(moveableCells, piece, targetCellRowPosition,
                                                            targetCellColPosition);
                    targetCellRowPosition += verticalFactors[factorIdx];
                    targetCellColPosition += horizontalFactors[factorIdx];
                }
            }

            return moveableCells;
        }

        /// <summary>
        /// Return a HashSet contains all cell the piece can move to.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="verticalFactors"></param>
        /// <param name="horizontalFactors"></param>
        /// <returns></returns>
        private HashSet<string> MakeListOfCellShortMovePiece(Piece piece, int[] verticalFactors, int[] horizontalFactors)
        {
            HashSet<string> moveableCells = new();

            for (int factorIdx = 0; factorIdx < horizontalFactors.Length; ++factorIdx)
            {
                int targetCellRowPosition = piece.RowPosition + verticalFactors[factorIdx];
                int targetCellColPosition = piece.ColumnPosition + horizontalFactors[factorIdx];

                if (IsRowAndColValid(targetCellRowPosition, targetCellColPosition))
                {
                    ShortMovePieceCellCheck(moveableCells, piece, targetCellRowPosition, targetCellColPosition);
                }
            }

            return moveableCells;
        }

        /// <summary>
        /// Return a HashSet contains all cells the pawn can move to.
        /// </summary>
        /// <param name="pawnPiece"></param>
        /// <returns></returns>
        private HashSet<string> PawnMoveableCells(Piece pawnPiece)
        {
            bool PieceIsBlack = pawnPiece.IsBlackTeam;

            //! Pawn's row and col positions after moves.
            int[] verticalFactors = { 1, 1 };
            int[] horizontalFactors = { 1, -1 };

            // Black goes down, white goes up by setting this factor.
            int verticalFactor = (PieceIsBlack) ? 1 : -1;

            HashSet<string> moveableCells = new();

            for (int factorIdx = 0; factorIdx < horizontalFactors.Length; ++factorIdx)
            {
                int targetCellRowPosition = pawnPiece.RowPosition + verticalFactors[factorIdx] * verticalFactor;
                int targetCellColPosition = pawnPiece.ColumnPosition + horizontalFactors[factorIdx] * verticalFactor;

                if (IsRowAndColValid(targetCellRowPosition, targetCellColPosition))
                    if (IsTargetCellIsOpponent(pawnPiece, targetCellRowPosition, targetCellColPosition) == true)
                        moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
            }

            if (IsTargetCellIsOpponent(pawnPiece, pawnPiece.RowPosition + 1 * verticalFactor, pawnPiece.ColumnPosition) == null)
            {
                moveableCells.Add(GetCellString(pawnPiece.RowPosition + 1 * verticalFactor, pawnPiece.ColumnPosition));

                if (!pawnPiece.FirstMovePerformed)
                {
                    int targetCellRowPosition = pawnPiece.RowPosition + 2 * verticalFactor;
                    int targetCellColPosition = pawnPiece.ColumnPosition;

                    if (IsTargetCellIsOpponent(pawnPiece, targetCellRowPosition, targetCellColPosition) == null)
                    {
                        moveableCells.Add(GetCellString(targetCellRowPosition, targetCellColPosition));
                    }
                }
            }

            return moveableCells;
        }

        /// <summary>
        /// Return a HashSet contains all cells the rook can move to.
        /// </summary>
        /// <param name="rookPiece"></param>
        /// <returns></returns>
        private HashSet<string> RookMoveableCells(Piece rookPiece)
        {
            //! Rook's row and col positions after moves.
            int[] verticalFactors = { 1, -1, 0, 0 };
            int[] horizontalFactors = { 0, 0, 1, -1 };

            return MakeListOfCellFarMovePiece(rookPiece, verticalFactors, horizontalFactors);
        }

        /// <summary>
        /// Return a HashSet contains all cells the knight can move to.
        /// </summary>
        /// <param name="knightPiece"></param>
        /// <returns></returns>
        private HashSet<string> KnightMoveableCells(Piece knightPiece)
        {
            //! Knight's row and col positions after move.
            int[] verticalFactors = { 1, -1, 2, -2, 2, -2, 1, -1 };
            int[] horizontalFactors = { -2, -2, -1, -1, 1, 1, 2, 2 };

            return MakeListOfCellShortMovePiece(knightPiece, verticalFactors, horizontalFactors);
        }

        /// <summary>
        /// Return a HashSet contains all cells the bishop can move to.
        /// </summary>
        /// <param name="bishopPiece"></param>
        /// <returns></returns>
        private HashSet<string> BishopMoveableCells(Piece bishopPiece)
        {
            //! Bishop's row and col positions after moves.
            int[] verticalFactors = { 1, -1, -1, 1 };
            int[] horizontalFactors = { 1, -1, 1, -1 };

            return MakeListOfCellFarMovePiece(bishopPiece, verticalFactors, horizontalFactors);
        }

        /// <summary>
        /// Return a HashSet contains all cells the king can move to.
        /// </summary>
        /// <param name="kingPiece"></param>
        /// <returns></returns>
        private HashSet<string> KingMoveableCells(Piece kingPiece)
        {
            //! King's row and col positions after moves.
            int[] verticalFactors = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] horizontalFactors = { 0, 1, 1, 1, 0, -1, -1, -1 };

            return MakeListOfCellShortMovePiece(kingPiece, verticalFactors, horizontalFactors);
        }

        /// <summary>
        /// Return a HashSet contains all cells the queen can move to.
        /// </summary>
        /// <param name="queenPiece"></param>
        /// <returns></returns>
        private HashSet<string> QueenMoveableCells(Piece queenPiece)
        {
            //! Queen's row and col positions after moves.
            int[] verticalFactors = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] horizontalFactors = { 0, 1, 1, 1, 0, -1, -1, -1 };

            return MakeListOfCellFarMovePiece(queenPiece, verticalFactors, horizontalFactors);
        }

        /// <summary>
        /// Auto choose between pieces's moves and return all cells the piece can move to.
        /// </summary>
        /// <param name="pieceIsRequestingToMove"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Overload for index arguments.
        /// </summary>
        /// <param name="pieceIsWaitingRow"></param>
        /// <param name="pieceIsWaitingCol"></param>
        /// <returns></returns>
        public HashSet<string> PieceMoveableCells(int pieceIsWaitingRow, int pieceIsWaitingCol)
        {
            return PieceMoveableCells(PiecesMatrix[pieceIsWaitingRow][pieceIsWaitingCol]);
        }
    }
}
