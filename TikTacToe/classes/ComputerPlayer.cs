using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace TikTacToe.classes
{
    /// <summary>
    /// This class will handle the logic for a computer player including deciding next move
    /// </summary>
    internal class ComputerPlayer : Player
    {
        /// <summary>
        /// enumeration denoting possible difficulty levels
        /// </summary>
        #region EnumerationDifficulty
        private enum Difficulty
        {
            easy,
            normal, 
            hard
        }
        #endregion

        /// <summary>
        /// ComputerPlayer attribute(s)
        /// </summary>
        #region ComputerPlayerClassVariables
        private Difficulty difficulty;
        #endregion

        /// <summary>
        /// Constructor for ComputerPlayer
        /// </summary>
        #region ComputerPlayerConstructor
        public ComputerPlayer(int difficulty) : base("Tik-Tak-Toeinator", 5) 
        {
            this.difficulty = (Difficulty)difficulty;
        }
        #endregion

        /// <summary>
        /// Getter and setter for ComputerPlayer attributes
        /// </summary>
        #region GettersAndSetters
        /// <summary>
        /// sets the difficulty level of the ComputerPlayer
        /// </summary>
        /// <param name="difficulty"></param>
        public void setDifficulty(int difficulty) { this.difficulty = (Difficulty)difficulty; }

        /// <summary>
        /// Returns the difficulty level of the ComputerPlayer as an integer
        /// </summary>
        /// <returns></returns>
        public int getDifficulty() { return (int)difficulty; }
        #endregion

        /// <summary>
        /// Method returns the next move to be made by the computer player based on the difficulty level
        /// of the computer all
        /// </summary>
        /// <param name="isPlayerOne"></param>
        /// <param name="isFirstMove"></param>
        /// <param name="gameBoard"></param>
        /// <returns></returns>
        #region nextMove
        public int[] nextMove(bool isPlayerOne, bool isFirstMove, GameBoard gameBoard) 
        {
            // if difficulty is hard, and center space is available always take it in first move
            // (almost always guaranties a a win or a tie)
            if(isFirstMove && difficulty.Equals(Difficulty.hard) && gameBoard.spaceIsAvailable(1,1)) return [1, 1];
            bool[,] possableMoves = new bool[2,9]; // array to store possible easy and normal moves(row zero easy row one normal)
            for (int i = 0, row = 0, column = 0; i < 9; column = (column + 1) % 3, i++)
            {
                // check if space is available, if not check increment row and continue
                if (!gameBoard.spaceIsAvailable(row, column)) 
                {
                    if (i == 2 || i == 5) row++;
                    continue;
                }
                //check if move would lead to computer win
                if (gameBoard.isWinningMove(isPlayerOne, row, column))
                {
                    //if true, and computer on hard mode make the move
                    //(no hesitation, just victory)
                    if (difficulty.Equals(Difficulty.hard)) return [row, column];
                    //if not in hard mode add to possible normal moves
                    possableMoves[1, i] = gameBoard.isWinningMove(isPlayerOne, row, column)
                                        || gameBoard.isWinningMove(!isPlayerOne, row, column);
                }               
                //Mark as available for easy move
                possableMoves[0,i] = gameBoard.spaceIsAvailable(row, column);
                if (i == 2 || i == 5) row++; //increment row
            }
            //all hard mode moves will be the same as normal for remainder of method
            Random rand = new Random(); //initialize random object to decrease difficulty
            if (!difficulty.Equals(Difficulty.easy))//if not in easy mode make a normal/hard move
            {
                //randomize column checked for normal move
                int top = rand.Next(1, 3);
                int middle = rand.Next(1, 3);
                int bottom = rand.Next(1, 3);
                //for increment built to preclude checking invalid memory location
                for (int i = 0; i < 3; top = (top + 1) % 3, middle = (middle + 1) % 3, bottom = (bottom + 1) % 3, i++)
                {
                    if (possableMoves[1, 3 + middle]) return [1, middle]; //check if possible normal move on center row and return if true
                    if (possableMoves[1, bottom]) return [2, bottom]; //check if possible normal move on bottom row and return if true
                    if (possableMoves[1, 6 + top]) return [0, top]; //check if possible normal move on top row and return if true
                }
            }
            //all moves are the same now, regardless of difficulty
            while (true) 
            {
                int easymove = rand.Next(1, 9);
                if (possableMoves[0, easymove])
                {
                    return [easymove / 3, easymove % 3];
                }
            }
        }
        #endregion
    }
}
