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
    sealed class ChessBoardGrid : Grid
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

    sealed class ChessBoardCell : Button
    {
        private readonly ChessBoardController Controller;
        private readonly int RowIndex, ColumnIndex;

        public ChessBoardCell(ChessBoardController controller, int rowIndex, int columnIndex)
        {
            Controller = controller;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            StyleModifier.MakeBackgroundTransparent(this);
            StyleModifier.NoMarginAndPadding(this);
            StyleModifier.SetAlignment(this, HorizontalAlignment.Stretch, VerticalAlignment.Stretch);

            Click += OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            RequestMoveToThisCell();
        }

        private void RequestMoveToThisCell()
        {
            Controller.RequestMovePieceToCell(RowIndex, ColumnIndex);
        }
    }

    /// <summary>
    /// Create a ChessBoard wrap in a ChessBoardGrid
    /// </summary>
    sealed class ChessBoard : Grid
    {
        #region Attributes and properties

        public readonly ChessBoardGrid ParentGrid;
        public readonly ChessBoardController Controller;

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize method, always require parentGrid paramater.
        /// </summary>
        /// <param name="parentGrid"></param>
        public ChessBoard(ChessBoardGrid parentGrid)
        {
            Controller = new(this);

            DataContext = new ChessBoardDataContext(this);
            ParentGrid = parentGrid;

            InitRowsAndColumns();
            InitCells();
            InitBindings();

            StyleModifier.SetAlignment(this, HorizontalAlignment.Center, VerticalAlignment.Center);
            StyleModifier.SetImageBackground(this, "Images/background.png");

            Controller.InitStartPosition();
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
        /// Initialize all cells in ChessBoard.
        /// </summary>
        private void InitCells()
        {
            for (int row = 0; row < 8; ++row)
            {
                for (int col = 0; col < 8; ++col)
                {
                    AddElement(new ChessBoardCell(Controller, row, col), row, col);
                }
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

        #endregion

        #region Add and place elements

        /// <summary>
        /// Add a element to specified row and column index.
        /// </summary>
        /// <param name="element">Element to add to ChessBoard</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        public void AddElement(FrameworkElement element, int row, int col)
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
        public void PlaceElement(FrameworkElement element, int row, int col)
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
