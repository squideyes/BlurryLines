using System;
using System.Windows;

namespace BlurryLines
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var states = Enum.GetValues(typeof(CellState));

            var x = random.Next(board.Columns);
            var y = random.Next(board.Rows);
            var state = (CellState)random.Next(states.Length);

            board.SetState(x, y, state);
        }
    }
}
