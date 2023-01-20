using chess_game.Components.Pieces;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components
{
    internal class ChessBoardController
    {
        #region Attributes and Properties

        private int PieceIsWaitingForMoveRowIndex = -1;
        private int PieceIsWaitingForMoveColIndex = -1;

        private readonly ChessBoard ParentChessBoard;

        private readonly List<Piece> PiecesOnBoard = new();

        /// <summary>
        /// 2D Matrix represents pieces on ChessBoard.
        /// </summary>
        public readonly List<List<Piece>> PiecesMatrix = new();

        private readonly PiecesMoveableCells MoveableCellsMarker;

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="parentChessBoard"></param>
        public ChessBoardController(ChessBoard parentChessBoard)
        {
            ParentChessBoard = parentChessBoard;
            InitRowsAndColumnsPiecesMatrix();

            MoveableCellsMarker = new(this);
        }

        /// <summary>
        /// Make PiecesMatrix have 8 columns and 8 rows (ChessBoard size).
        /// </summary>
        public void InitRowsAndColumnsPiecesMatrix()
        {
            for (int row = 0; row < 8; ++row)
            {
                PiecesMatrix.Add(new List<Piece>());
                for (int col = 0; col < 8; ++col)
                {
                    PiecesMatrix[row].Add(null);
                }
            }
        }

        public void InitStartPosition()
        {
            InitPawns();
            InitRooks();
            InitKnights();
            InitBishops();
            InitQueens();
            InitKings();
        }

        public void InitPawns()
        {
            for (int pawnCount = 0; pawnCount < 8; ++pawnCount)
            {
                AddPiece(new Pawn(this, MoveableCellsMarker, true), 1, pawnCount);
                AddPiece(new Pawn(this, MoveableCellsMarker, false), 6, pawnCount);
            }
        }

        public void InitRooks()
        {
            AddPiece(new Rook(this, MoveableCellsMarker, true), 0, 0);
            AddPiece(new Rook(this, MoveableCellsMarker, true), 0, 7);
            AddPiece(new Rook(this, MoveableCellsMarker, false), 7, 0);
            AddPiece(new Rook(this, MoveableCellsMarker, false), 7, 7);
        }

        public void InitKnights()
        {
            AddPiece(new Knight(this, MoveableCellsMarker, true), 0, 1);
            AddPiece(new Knight(this, MoveableCellsMarker, true), 0, 6);
            AddPiece(new Knight(this, MoveableCellsMarker, false), 7, 1);
            AddPiece(new Knight(this, MoveableCellsMarker, false), 7, 6);
        }

        public void InitBishops()
        {
            AddPiece(new Bishop(this, MoveableCellsMarker, true), 0, 2);
            AddPiece(new Bishop(this, MoveableCellsMarker, true), 0, 5);
            AddPiece(new Bishop(this, MoveableCellsMarker, false), 7, 2);
            AddPiece(new Bishop(this, MoveableCellsMarker, false), 7, 5);
        }

        public void InitQueens()
        {
            AddPiece(new Queen(this, MoveableCellsMarker, true), 0, 3);
            AddPiece(new Queen(this, MoveableCellsMarker, false), 7, 3);
        }

        public void InitKings()
        {
            AddPiece(new King(this, MoveableCellsMarker, true), 0, 4);
            AddPiece(new King(this, MoveableCellsMarker, false), 7, 4);
        }

        #endregion

        #region Add and place pieces

        /// <summary>
        /// Add and place piece to chessboard.
        /// </summary>
        /// <param name="piece">Piece to add and place</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        public void AddPiece(Piece piece, int row, int col)
        {
            PiecesOnBoard.Add(piece);
            ParentChessBoard.AddElement(piece, row, col);
            PlacePiece(piece, row, col, true);
        }

        /// <summary>
        /// Place piece to chessboard.
        /// </summary>
        /// <param name="piece">piece to place</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        public void PlacePiece(Piece piece, int row, int col, bool isInitMove = false)
        {
            PiecesMatrix[row][col] = piece;
            piece.SetRowColumnPosition(row, col, isInitMove);
            ParentChessBoard.PlaceElement(piece, row, col);
        }

        /// <summary>
        /// Remove piece with specified row and column index out of board.
        /// </summary>
        /// <param name="row">Piece to remove's row index</param>
        /// <param name="col">Piece to remove's column index</param>
        public void RemovePiece(int row, int col)
        {
            ParentChessBoard.Children.Remove(PiecesMatrix[row][col]);
            PiecesOnBoard.Remove(PiecesMatrix[row][col]);
            PiecesMatrix[row][col] = null;
        }

        #endregion

        #region Request and perform a move

        /// <summary>
        /// Check if there is a waiting piece.
        /// </summary>
        /// <returns>A boolean represent this is a waiting piece or not</returns>
        public bool HaveWaitingPiece()
        {
            return PieceIsWaitingForMoveRowIndex != -1 && PieceIsWaitingForMoveColIndex != -1;
        }

        /// <summary>
        /// Check current waiting piece is in the black team or not.
        /// </summary>
        /// <returns>A boolean represent this is a black team's piece or not</returns>
        public bool IsWaitingPieceIsBlackTeam()
        {
            return PiecesMatrix[PieceIsWaitingForMoveRowIndex][PieceIsWaitingForMoveColIndex].IsBlackTeam;
        }

        /// <summary>
        /// Request a piece is waiting for a move and mark all other pieces are not waiting.
        /// </summary>
        /// <param name="pieceRowIndex">Piece's row index</param>
        /// <param name="pieceColIndex">Piece's col index</param>
        public void WaitForMove(int pieceRowIndex, int pieceColIndex)
        {
            PieceIsWaitingForMoveRowIndex = pieceRowIndex;
            PieceIsWaitingForMoveColIndex = pieceColIndex;
            MarkAllOtherPiecesNotChecked(pieceRowIndex, pieceColIndex);
        }

        /// <summary>
        /// Mark all pieces are not checked (ToggleButton) except a piece.
        /// </summary>
        /// <param name="exceptPieceRowIndex">Row of the excluded object</param>
        /// <param name="exceptPieceColIndex">Column of the excluded object</param>
        private void MarkAllOtherPiecesNotChecked(int exceptPieceRowIndex, int exceptPieceColIndex)
        {
            foreach (Piece piece in PiecesOnBoard)
            {
                if (piece.RowPosition == exceptPieceRowIndex
                        && piece.ColumnPosition == exceptPieceColIndex)
                    continue;
                piece.IsChecked = false;
            }
        }

        /// <summary>
        /// Uncheck piece when moved.
        /// </summary>
        /// <param name="pieceRowIndex">Piece to uncheck's row index</param>
        /// <param name="pieceColIndex">Piece to uncheck's column index</param>
        private void MarkThisPieceNotCheckedAfterMove(int pieceRowIndex, int pieceColIndex)
        {
            PiecesMatrix[pieceRowIndex][pieceColIndex].IsChecked = false;
        }

        /// <summary>
        /// Request a capture from waiting piece to target opponent's piece.
        /// </summary>
        /// <param name="targetCellRowPosition">Row index of target opponent's piece</param>
        /// <param name="targetCellColPosition">Column index of target opponent's piece</param>
        public void RequestCapturePieceAtCell(int targetCellRowPosition, int targetCellColPosition)
        {
            RemovePiece(targetCellRowPosition, targetCellColPosition);
            RequestMovePieceToCell(targetCellRowPosition, targetCellColPosition);
        }

        /// <summary>
        /// Move waiting piece to triggerred cell.
        /// </summary>
        /// <param name="targetCellRowPosition">Triggerred's cell row position</param>
        /// <param name="targetCellColPosition">Triggerred's cell column position</param>
        public void RequestMovePieceToCell(int targetCellRowPosition, int targetCellColPosition)
        {
            if (HaveWaitingPiece())
            {
                MovePiece(targetCellRowPosition, targetCellColPosition);
                MarkThisPieceNotCheckedAfterMove(targetCellRowPosition, targetCellColPosition);
            }
        }

        /// <summary>
        /// Move piece to different cell.
        /// </summary>
        /// <param name="newRowPosition">Row index of targer cell to move</param>
        /// <param name="newColumnPosition">Column index of targer cell to move</param>
        public void MovePiece(int newRowPosition, int newColumnPosition)
        {
            Piece PieceToMove = PiecesMatrix[PieceIsWaitingForMoveRowIndex][PieceIsWaitingForMoveColIndex];

            PiecesMatrix[newRowPosition][newColumnPosition] = PieceToMove;
            PiecesMatrix[PieceIsWaitingForMoveRowIndex][PieceIsWaitingForMoveColIndex] = null;
            PlacePiece(PieceToMove, newRowPosition, newColumnPosition);

            MakeNoPieceIsWaiting();
        }

        /// <summary>
        /// Make current waiting piece is not waiting.
        /// </summary>
        public void MakeNoPieceIsWaiting()
        {
            PieceIsWaitingForMoveRowIndex = -1;
            PieceIsWaitingForMoveColIndex = -1;
        }

        #endregion
    }
}
