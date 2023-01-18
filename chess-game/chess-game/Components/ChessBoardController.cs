using chess_game.Components.Pieces;
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
            InitStartPosition();
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
                AddPiece(new BlackPawn(this), 1, pawnCount);
                AddPiece(new WhitePawn(this), 6, pawnCount);
            }
        }

        public void InitRooks()
        {
            AddPiece(new BlackRook(this), 0, 0);
            AddPiece(new BlackRook(this), 0, 7);
            AddPiece(new WhiteRook(this), 7, 0);
            AddPiece(new WhiteRook(this), 7, 7);
        }

        public void InitKnights()
        {
            AddPiece(new BlackKnight(this), 0, 1);
            AddPiece(new BlackKnight(this), 0, 6);
            AddPiece(new WhiteKnight(this), 7, 1);
            AddPiece(new WhiteKnight(this), 7, 6);
        }

        public void InitBishops()
        {
            AddPiece(new BlackBishop(this), 0, 2);
            AddPiece(new BlackBishop(this), 0, 5);
            AddPiece(new WhiteBishop(this), 7, 2);
            AddPiece(new WhiteBishop(this), 7, 5);
        }

        public void InitQueens()
        {
            AddPiece(new BlackQueen(this), 0, 3);
            AddPiece(new WhiteQueen(this), 7, 3);
        }

        public void InitKings()
        {
            AddPiece(new BlackKing(this), 0, 4);
            AddPiece(new WhiteKing(this), 7, 4);
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

        /// <summary>
        /// Move piece to different cell.
        /// </summary>
        /// <param name="lastRowPosition">Row index of piece to move</param>
        /// <param name="lastColumnPosition">Column index of piece to move</param>
        /// <param name="newRowPosition">Row index of targer cell to move</param>
        /// <param name="newColumnPosition">Column index of targer cell to move</param>
        public void MovePiece(int lastRowPosition, int lastColumnPosition, int newRowPosition, int newColumnPosition)
        {
            Piece PieceToMove = PiecesMatrix[lastRowPosition][lastColumnPosition];

            if (PieceToMove == null)
                return;

            PiecesMatrix[newRowPosition][newColumnPosition] = PieceToMove;
            PiecesMatrix[lastRowPosition][lastColumnPosition] = null;
            PlacePiece(PieceToMove, newRowPosition, newColumnPosition);
        }

        #endregion
    }
}
