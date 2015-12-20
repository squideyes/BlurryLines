using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlurryLines
{
    public partial class Board : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(Board),
            new PropertyMetadata(32, OnResetBoard));

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(Board),
            new PropertyMetadata(32, OnResetBoard));

        public static readonly DependencyProperty CellSizeProperty =
            DependencyProperty.Register("CellSize", typeof(int), typeof(Board),
            new PropertyMetadata(10, OnResetBoard));

        private class Cells
        {
            public Cells(int sizeX, int sizeY)
            {
                Rows = new List<List<Cell>>();

                for (int y = 0; y < sizeY; y++)
                {
                    var cells = new List<Cell>();

                    for (int x = 0; x < sizeX; x++)
                        cells.Add(new Cell());

                    Rows.Add(cells);
                }
            }

            public List<List<Cell>> Rows { get; }
        }

        private class Cell : INotifyPropertyChanged
        {
            private CellState state = CellState.Empty;
            private Brush fillColor = Brushes.White;

            public event PropertyChangedEventHandler PropertyChanged;

            public CellState State
            {
                get
                {
                    return state;
                }
                set
                {
                    state = value;

                    switch (state)
                    {
                        case CellState.Current:
                            FillColor = Brushes.Red;
                            break;
                        case CellState.Food:
                            FillColor = Brushes.Black;
                            break;
                        case CellState.Gap:
                            FillColor = Brushes.Gray;
                            break;
                        case CellState.Route:
                            FillColor = Brushes.HotPink;
                            break;
                        default:
                            FillColor = Brushes.White;
                            break;
                    }
                }
            }

            public Brush FillColor
            {
                get
                {
                    return fillColor;
                }
                private set
                {
                    fillColor = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs(nameof(FillColor)));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Board()
        {
            InitializeComponent();

            Reset();
        }

        public int Rows
        {
            get
            {
                return (int)GetValue(RowsProperty);
            }
            set
            {
                SetValue(RowsProperty, value);
            }
        }

        public int Columns
        {
            get
            {
                return (int)GetValue(ColumnsProperty);
            }
            set
            {
                SetValue(ColumnsProperty, value);
            }
        }

        public int CellSize
        {
            get
            {
                return (int)GetValue(CellSizeProperty);
            }
            set
            {
                SetValue(CellSizeProperty, value);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnResetBoard(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var board = dependencyObject as Board;

            board.OnPropertyChanged(nameof(Rows));
            board.OnPropertyChanged(nameof(Columns));

            board.Reset();
        }

        private void Reset()
        {
            Width = Columns * CellSize;
            Height = Rows * CellSize;

            board.DataContext = new Cells(Columns, Rows);
        }

        public void SetState(int x, int y, CellState state)
        {
            if (x < 0 || x >= Columns)
                throw new ArgumentOutOfRangeException(nameof(x));

            if (y < 0 || y >= Rows)
                throw new ArgumentOutOfRangeException(nameof(y));

            var cc = board.DataContext as Cells;

            cc.Rows[y][x].State = state;
        }
    }
}