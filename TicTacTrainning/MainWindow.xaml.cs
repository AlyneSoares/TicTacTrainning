using System;
using System.Windows.Controls;
using System.Windows;
using System.Linq;
using System.Windows.Media;

namespace TicTacTrainning
{
    public partial class MainWindow : Window
    {
        #region PrivateMembers
        private MarkType[] mResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

        private void NewGame()
        {
            mResults = new MarkType[9];
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;

        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"> The button itself </param>
        /// <param name="e">the events of tje click </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            
            var button = (Button)sender;
            var column = Grid.GetRow(button);
            var row = Grid.GetColumn(button);

            var index = column + (row * 3);

            // Not allowd to receive new value if it's not empty
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            //Set wanted value
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Show value in the cell
            button.Content = mPlayer1Turn ? "X" : "O";

            //Alternats the players
            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])

            {
                mGameEnded = true ;
            }
            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;
            }               
        }
    }
}