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

        public Piece()
        {
            //Style = Application.Current.Resources["PieceButton"] as Style;
        }

        public void SetRowPosition(int value)
        {
            if (0 <= value && value < 8)
                RowPosition = value;
        }

        public void SetColumnPosition(int value)
        {
            if (0 <= value && value < 8)
                ColumnPosition = value;
        }

        public void SetRowColumnPosition(int row, int col)
        {
            SetRowPosition(row);
            SetColumnPosition(col);
        }
    }

    static class PieceImageIcon
    {
        private static string CurrentDirectory = Directory.GetCurrentDirectory();

        public static Image BlackBishop = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/BlackBishop.png", UriKind.Relative)))
        };

        public static Image BlackKing = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/BlackKing.png", UriKind.Relative)))
        };

        public static Image BlackKnight = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/BlackKnight.png", UriKind.Relative)))
        };

        public static Image BlackPawn = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/BlackPawn.png", UriKind.Relative)))
        };

        public static Image BlackQueen = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/BlackKing.png", UriKind.Relative)))
        };

        public static Image BlackRook = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/BlackRook.png", UriKind.Relative)))
        };

        public static Image WhiteBishop = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/WhiteBishop.png", UriKind.Relative)))
        };

        public static Image WhiteKing = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/WhiteKing.png", UriKind.Relative)))
        };

        public static Image WhiteKnight = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/WhiteKnight.png", UriKind.Relative)))
        };

        public static Image WhitePawn = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/WhitePawn.png", UriKind.Relative)))
        };

        public static Image WhiteQueen = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/WhiteKing.png", UriKind.Relative)))
        };

        public static Image WhiteRook = new()
        {
            Source = new BitmapImage(new Uri(new Uri(CurrentDirectory, UriKind.Absolute), new Uri(@"/Images/WhiteRook.png", UriKind.Relative)))
        };
    }
}
