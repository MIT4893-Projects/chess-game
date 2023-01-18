using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;

namespace chess_game.Components.Pieces
{
    internal class Piece : ToggleButton
    {
        public readonly bool IsBlackTeam;
        protected readonly ChessBoardController Controller;
        public int RowPosition { get; private set; } = 0;
        public int ColumnPosition { get; private set; } = 0;

        public Piece(ChessBoardController parentChessBoardController, bool isBlack)
        {
            Controller = parentChessBoardController;
            IsBlackTeam = isBlack;

            StyleModifier.MakeCornersSquare(this);
            StyleModifier.MakeBackgroundTransparent(this);
            StyleModifier.NoMarginAndPadding(this);
            StyleModifier.SetAlignment(this, HorizontalAlignment.Stretch, VerticalAlignment.Stretch);

            Click += OnClick;
        }

        public void SetRowPosition(int value)
        {
            if (0 <= value && value < 8)
                RowPosition = value;
            Grid.SetRow(this, RowPosition);
        }

        public void SetColumnPosition(int value)
        {
            if (0 <= value && value < 8)
                ColumnPosition = value;
            Grid.SetColumn(this, ColumnPosition);
        }

        public void SetRowColumnPosition(int row, int col)
        {
            SetRowPosition(row);
            SetColumnPosition(col);
        }

        #region Event handlers

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if ((bool)IsChecked)
            {
                if (IsACaptureMove())
                    Controller.RequestCapturePieceAtCell(RowPosition, ColumnPosition);
                else
                    Controller.WaitForMove(RowPosition, ColumnPosition);
            }
            else
            {
                Controller.MakeNoPieceIsWaiting();
            }
        }

        private bool IsACaptureMove()
        {
            return Controller.HaveWaitingPiece() && Controller.IsWaitingPieceIsBlackTeam() != IsBlackTeam;
        }

        #endregion
    }

    static class PieceImageIcon
    {
        public static readonly Uri BlackBishop = new("ms-appx:///Images/BlackBishop.png");

        public static readonly Uri BlackKing = new("ms-appx:///Images/BlackKing.png");

        public static readonly Uri BlackKnight = new("ms-appx:///Images/BlackKnight.png");

        public static readonly Uri BlackPawn = new("ms-appx:///Images/BlackPawn.png");

        public static readonly Uri BlackQueen = new("ms-appx:///Images/BlackQueen.png");

        public static readonly Uri BlackRook = new("ms-appx:///Images/BlackRook.png");

        public static readonly Uri WhiteBishop = new("ms-appx:///Images/WhiteBishop.png");

        public static readonly Uri WhiteKing = new("ms-appx:///Images/WhiteKing.png");

        public static readonly Uri WhiteKnight = new("ms-appx:///Images/WhiteKnight.png");

        public static readonly Uri WhitePawn = new("ms-appx:///Images/WhitePawn.png");

        public static readonly Uri WhiteQueen = new("ms-appx:///Images/WhiteQueen.png");

        public static readonly Uri WhiteRook = new("ms-appx:///Images/WhiteRook.png");
    }
}
