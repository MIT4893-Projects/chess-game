using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components.Pieces
{
    internal class Piece : Button
    {
        public int RowPosition { get; private set; } = 0;
        public int ColumnPosition { get; private set; } = 0;

        public Piece() : base()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
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
    }

    static class PieceImageIcon
    {
        public static readonly Image BlackBishop = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/BlackBishop.png"))
        };

        public static readonly Image BlackKing = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/BlackKing.png"))
        };

        public static readonly Image BlackKnight = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/BlackKnight.png"))
        };

        public static readonly Image BlackPawn = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/BlackPawn.png"))
        };

        public static readonly Image BlackQueen = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/BlackQueen.png"))
        };

        public static readonly Image BlackRook = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/BlackRook.png"))
        };

        public static readonly Image WhiteBishop = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/WhiteBishop.png"))
        };

        public static readonly Image WhiteKing = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/WhiteKing.png"))
        };

        public static readonly Image WhiteKnight = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/WhiteKnight.png"))
        };

        public static readonly Image WhitePawn = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/WhitePawn.png"))
        };

        public static readonly Image WhiteQueen = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/WhiteQueen.png"))
        };

        public static readonly Image WhiteRook = new()
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/WhiteRook.png"))
        };
    }
}
