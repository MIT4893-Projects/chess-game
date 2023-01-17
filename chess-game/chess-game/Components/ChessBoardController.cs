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
                AddPiece(new BlackPawn(), 1, pawnCount);
                AddPiece(new WhitePawn(), 6, pawnCount);
            }
        }

        public void InitRooks()
        {
            AddPiece(new BlackRook(), 0, 0);
            AddPiece(new BlackRook(), 0, 7);
            AddPiece(new WhiteRook(), 7, 0);
            AddPiece(new WhiteRook(), 7, 7);
        }

        public void InitKnights()
        {
            AddPiece(new BlackKnight(), 0, 1);
            AddPiece(new BlackKnight(), 0, 6);
            AddPiece(new WhiteKnight(), 7, 1);
            AddPiece(new WhiteKnight(), 7, 6);
        }

        public void InitBishops()
        {
            AddPiece(new BlackBishop(), 0, 2);
            AddPiece(new BlackBishop(), 0, 5);
            AddPiece(new WhiteBishop(), 7, 2);
            AddPiece(new WhiteBishop(), 7, 5);
        }

        public void InitQueens()
        {
            AddPiece(new BlackQueen(), 0, 3);
            AddPiece(new WhiteQueen(), 7, 3);
        }

        public void InitKings()
        {
            AddPiece(new BlackKing(), 0, 4);
            AddPiece(new WhiteKing(), 7, 4);
        }

        #endregion

        #region Add and place pieces

        public void AddPiece(Piece piece, int row, int col)
        {
            PiecesOnBoard.Add(piece);
            ParentChessBoard.AddElement(piece, row, col);
            PlacePiece(piece, row, col);
        }

        public void PlacePiece(Piece piece, int row, int col)
        {
            PiecesMatrix[row][col] = piece;
            piece.SetRowColumnPosition(row, col);
            ParentChessBoard.PlaceElement(piece, row, col);
        }

        #endregion
    }
}
