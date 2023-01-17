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
    /// <summary>
    /// Control which wrap around the ChessBoard to make ChessBoard square. ChessBoard size
    /// based on this control's size.
    /// </summary>
    internal class ChessBoardGrid : Grid
    {
        #region Attributes and Properties

        private readonly ChessBoard Board;

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize method.
        /// </summary>
        public ChessBoardGrid()
        {
            Board = new(this);
            AddChessBoard();
            AddEventHandlers();
        }

        /// <summary>
        /// Add ChessBoard to this control.
        /// </summary>
        public void AddChessBoard()
        {
            Children.Add(Board);
            SetRow(Board, 0);
            SetColumn(Board, 0);
        }

        /// <summary>
        /// Add event handlers for this control.
        /// </summary>
        public void AddEventHandlers()
        {
            SizeChanged += OnSizeChanged;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handlers to trigger when size changed, use to update inside ChessBoard's size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Board.UpdateSize();
        }

        #endregion
    }

    /// <summary>
    /// Create a ChessBoard wrap in ChessBoardGrid
    /// </summary>
    internal class ChessBoard : Grid
    {
        #region Attributes and properties

        public readonly ChessBoardGrid ParentGrid;

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize method, always require parentGrid paramater.
        /// </summary>
        /// <param name="parentGrid"></param>
        public ChessBoard(ChessBoardGrid parentGrid)
        {
            DataContext = new ChessBoardDataContext(this);
            ParentGrid = parentGrid;

            InitAlignments();
            InitRowsAndColumns();
            InitBindings();
            FillCellsColor();
        }

        /// <summary>
        /// Initialize Alignments for ChessBoard.
        /// </summary>
        private void InitAlignments()
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Initialize a ChessBoard with 8 rows and 8 columns.
        /// </summary>
        private void InitRowsAndColumns()
        {
            for (int i = 0; i < 8; ++i)
            {
                RowDefinitions.Add(new RowDefinition());
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        /// <summary>
        /// Initialize size's bindings to make ChessBoard square.
        /// </summary>
        private void InitBindings()
        {
            BindingOperations.SetBinding(this, HeightProperty, new Binding() { Path = new PropertyPath("Height") });
            BindingOperations.SetBinding(this, WidthProperty, new Binding() { Path = new PropertyPath("Width") });
        }

        /// <summary>
        /// Fill colors to every cells in ChessBoard
        /// </summary>
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

        #region Add and place elements

        /// <summary>
        /// Add a element to specified row and column index.
        /// </summary>
        /// <param name="element">Element to add to ChessBoard</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        private void AddElement(FrameworkElement element, int row, int col)
        {
            Children.Add(element);
            PlaceElement(element, row, col);
        }

        /// <summary>
        /// Place a element to specified row and column index. (NOT add)
        /// </summary>
        /// <param name="element"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void PlaceElement(FrameworkElement element, int row, int col)
        {
            SetRow(element, row);
            SetColumn(element, col);
        }

        #endregion

        #region Getters

        /// <summary>
        /// DataContext getter.
        /// </summary>
        /// <returns>This object DataContext as type ChessBoardDataContext.</returns>
        public ChessBoardDataContext GetDataContext()
        {
            return DataContext as ChessBoardDataContext;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Update ChessBoard size when ParentElement's size changed.
        /// </summary>
        public void UpdateSize()
        {
            GetDataContext().OnPropertyChange("Width");
            GetDataContext().OnPropertyChange("Height");
        }

        #endregion
    }

    /// <summary>
    /// DataContext for ChessBoard
    /// </summary>
    internal class ChessBoardDataContext : DataContext
    {
        #region Attrbutes and properties

        private readonly ChessBoard ParentElement;

        #endregion

        /// <summary>
        /// Initialize method, take one paramater specify which object own this DataContext
        /// </summary>
        /// <param name="parentElement">Parent object own this DataContext</param>
        public ChessBoardDataContext(ChessBoard parentElement)
        {
            ParentElement = parentElement;
        }

        #region Size bindings

        /// <summary>
        /// ChessBoard's Width
        /// </summary>
        public double Width
        {
            // Set width to minimum of ParentElement's Width and ParentElement's size. So it can't
            // overflow.
            get => Math.Min(ParentElement.ParentGrid.ActualWidth, ParentElement.ParentGrid.ActualHeight);
        }

        /// <summary>
        /// chessBoard's Height
        /// </summary>
        public double Height
        {
            // Set height equals width to make ChessBoard square.
            get => Width;
        }

        #endregion
    }
}
