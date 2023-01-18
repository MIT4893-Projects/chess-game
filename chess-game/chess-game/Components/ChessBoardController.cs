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
                AddPiece(new Pawn(this, true), 1, pawnCount);
                AddPiece(new Pawn(this, false), 6, pawnCount);
            }
        }

        public void InitRooks()
        {
            AddPiece(new Rook(this, true), 0, 0);
            AddPiece(new Rook(this, true), 0, 7);
            AddPiece(new Rook(this, false), 7, 0);
            AddPiece(new Rook(this, false), 7, 7);
        }

        public void InitKnights()
        {
            AddPiece(new Knight(this, true), 0, 1);
            AddPiece(new Knight(this, true), 0, 6);
            AddPiece(new Knight(this, false), 7, 1);
            AddPiece(new Knight(this, false), 7, 6);
        }

        public void InitBishops()
        {
            AddPiece(new Bishop(this, true), 0, 2);
            AddPiece(new Bishop(this, true), 0, 5);
            AddPiece(new Bishop(this, false), 7, 2);
            AddPiece(new Bishop(this, false), 7, 5);
        }

        public void InitQueens()
        {
            AddPiece(new Queen(this, true), 0, 3);
            AddPiece(new Queen(this, false), 7, 3);
        }

        public void InitKings()
        {
            AddPiece(new King(this, true), 0, 4);
            AddPiece(new King(this, false), 7, 4);
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
            PlacePiece(piece, row, col);
        }

        /// <summary>
        /// Place piece to chessboard.
        /// </summary>
        /// <param name="piece">piece to place</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        public void PlacePiece(Piece piece, int row, int col)
        {
            PiecesMatrix[row][col] = piece;
            piece.SetRowColumnPosition(row, col);
            ParentChessBoard.PlaceElement(piece, row, col);
        }

        #endregion

        #region Request and perform a move

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
        /// <param name="pieceRowIndex"></param>
        /// <param name="pieceColIndex"></param>
        private void MarkThisPieceNotCheckedAfterMove(int pieceRowIndex, int pieceColIndex)
        {
            PiecesMatrix[pieceRowIndex][pieceColIndex].IsChecked = false;
        }

        /// <summary>
        /// Move waiting piece to triggerred cell.
        /// </summary>
        /// <param name="targetRowPosition">Triggerred's cell row position</param>
        /// <param name="targetColPosition">Triggerred's cell column position</param>
        public void RequestMovePieceToCell(int targetRowPosition, int targetColPosition)
        {
            if (PieceIsWaitingForMoveRowIndex != -1 && PieceIsWaitingForMoveColIndex != -1)
            {
                MovePiece(targetRowPosition, targetColPosition);
                MarkThisPieceNotCheckedAfterMove(targetRowPosition, targetColPosition);
            }
        }

        /// <summary>
        /// Move piece to different cell.
        /// </summary>
        /// <param name="lastRowPosition">Row index of piece to move</param>
        /// <param name="lastColumnPosition">Column index of piece to move</param>
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

        private void MakeNoPieceIsWaiting()
        {
            PieceIsWaitingForMoveRowIndex = 0;
            PieceIsWaitingForMoveColIndex = 0;
        }

        #endregion
    }
}
