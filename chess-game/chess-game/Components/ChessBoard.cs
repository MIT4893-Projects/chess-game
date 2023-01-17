using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using chess_game.Components.Pieces;

namespace chess_game.Components
{
    internal class ChessBoardGrid : Grid
    {
        private readonly ChessBoard Board;

        public ChessBoardGrid()
        {
            Board = new(this) { Background = new SolidColorBrush(Colors.Gray) };
            Children.Add(Board);
            SetRow(Board, 0);
            SetColumn(Board, 0);

            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Board.UpdateSize();
        }
    }

    /// <summary>
    /// Create a ChessBoard wrap in ChessBoardGrid
    /// </summary>
    internal class ChessBoard : Grid
    {
        #region Initialize

        public readonly ChessBoardGrid ParentGrid;
        public ChessBoard(ChessBoardGrid parentGrid)
        {
            DataContext = new ChessBoardDataContext(this);
            ParentGrid = parentGrid;

            InitAlignments();
            InitRowsAndColumns();
            InitBindings();
            FillCellsColor();
        }

        private void InitAlignments()
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        private void InitRowsAndColumns()
        {
            for (int i = 0; i < 8; ++i)
            {
                RowDefinitions.Add(new RowDefinition());
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void InitBindings()
        {
            BindingOperations.SetBinding(this, HeightProperty, new Binding() { Path = new PropertyPath("Height") });
            BindingOperations.SetBinding(this, WidthProperty, new Binding() { Path = new PropertyPath("Width") });
        }

        private void FillCellsColor()
        {
            SolidColorBrush black = new(Colors.Black);
            SolidColorBrush white = new(Colors.White);

            for (int row = 0; row < RowDefinitions.Count; ++row)
            {
                for (int col = 0; col < ColumnDefinitions.Count; ++col)
                {
                    Rectangle CellBackground = new()
                    {
                        Fill = ((row + col) % 2 == 0) ? white : black
                    };

                    AddElement(CellBackground, row, col);
                }
            }
        }

        #endregion

        private void AddElement(FrameworkElement element, int row, int col)
        {
            Children.Add(element);
            PlaceElement(element, row, col);
        }

        private void PlaceElement(FrameworkElement element, int row, int col)
        {
            SetRow(element, row);
            SetColumn(element, col);
        }

        public ChessBoardDataContext GetDataContext()
        {
            return DataContext as ChessBoardDataContext;
        }

        public void UpdateSize()
        {
            GetDataContext().OnPropertyChange("Width");
            GetDataContext().OnPropertyChange("Height");
        }
    }

    internal class ChessBoardDataContext : DataContext
    {
        private readonly ChessBoard ParentElement;
        public ChessBoardDataContext(ChessBoard parentElement)
        {
            ParentElement = parentElement;
        }

        public double Width
        {
            get => Math.Min(ParentElement.ParentGrid.ActualWidth, ParentElement.ParentGrid.ActualHeight);
        }

        public double Height
        {
            get => Width;
        }
    }
}
