using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TikTacToe.classes
{
    /// <summary>
    /// This class will represent the game board for every instance of a game
    /// </summary>
    class GameBoard
    {
        /// <summary>
        /// null-able two-dimensional array representing game board
        /// null == space not played
        /// 0 == player 0 space
        /// 1 == player 1 space
        /// </summary>
        #region GameBoardClassVariable
        private bool?[,] board = new bool?[3, 3];
        private int winningMoveCode;
        /*
         * Code key:
         * 0: Top Row
         * 1: Middle Row
         * 2: Bottom Row
         * 3: First Column
         * 4: Second Column
         * 5: Third Column
         * 6: Downward Diagonal
         * 7: Upward Diagonal
        */
        /* if static win condition is needed
        private static readonly bool[,,] winCondition = 
            { 
                { {true, true, true }, {false, false, false }, {false, false, false } },
                { {false, false, false }, {true, true, true }, {false, false, false } },
                { {false, false, false }, {false, false, false }, {true, true, true } },
                { {true, false, false }, {true, false, false }, {true, false, false } },
                { {false, true, false }, {false, true, false }, {false, true, false } },
                { {false, false, true }, {false, false, true }, {false, false, true } },
                { {true, false, false }, {false, true, false }, {false, false, true } },
                { {false, false, true }, {false, true, false }, {true, false, false } },
            };
        */
        #endregion

        /// <summary>
        /// this a constructor for the GameBoard class that ensures each "shape"
        /// on the game board is set to null
        /// </summary>
        #region GameBoardConstructor
        public GameBoard()
        {
            int i = 0; //row
            int j = 0; //column
            while (true)
            {
                board[i, j] = null;
                j++;
                if (j == 3)
                {
                    j = 0;
                    i++;
                }
                if (i == 3) break; //exit loop if i == 3 (i.e. board is set to default)
            }
        }
        #endregion

        /// <summary>
        /// Getter for class Attributes
        /// </summary>
        #region Getter(s)
        /// <summary>
        /// turns code denoting which move lead to the win
        /// </summary>
        /// <returns></returns>
        public int getWinningCode() {  return winningMoveCode; }
        #endregion

        /// <summary>
        /// check if space is available for move
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        #region spaceIsAvailable
        public bool spaceIsAvailable(int row, int column)
        {
            return !board[row, column].HasValue;
        }
        #endregion

        /// <summary>
        /// This method will handle a player (attempting to) take there turn
        /// returns [move successful, player has won game]
        /// </summary>
        /// <param name="player"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        #region takeTurn
        public bool[] takeTurn(bool player, int row, int column)
        {
            if (!board[row, column].HasValue) return [ false, false] ;
            bool winningMove = isWinningMove(player, row, column);
            board[row, column] = player;
            return [true, winningMove];
        }
        #endregion

        /// <summary>
        /// This public method will return true if the provided move for the given player will lead to a win
        /// it will also define the code for which three are in a row
        /// </summary>
        /// <param name="player"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        #region isWinningMove
        public bool isWinningMove(bool player, int row, int column)
        {
            bool winningMove = false;
            if (row == 1 && column == 1) //if move is center test both diagonal
            {
                if (diagnalDownTest(player)) 
                {
                    winningMoveCode = 6;
                    winningMove = true;
                }
                if (diagnalUpTest(player))
                {
                    winningMoveCode = 7;
                    winningMove = true;
                }
            }
            else if (row == column) //if row and column are equal test down diagonal
            {
                if (diagnalDownTest(player))
                {
                    winningMoveCode = 6;
                    winningMove = true;
                }
            }
            else if (Math.Abs(row - column) == 2) //if move is top right if bottom left test up diagonal
            { 
                if(diagnalUpTest(player))
                {
                    winningMoveCode = 7;
                    winningMove = true;
                }
            }
            if (!winningMove && rowTest(row, player))
            {
                winningMoveCode = row;
                winningMove = true;
            }
            if (!winningMove && columnTest(column, player))
            {
                winningMoveCode = column - 3;
                winningMove = true;
            }
            return winningMove; //return true if row or column of move leads to a win
        }
        #endregion

        /// <summary>
        /// This is a series of private methods used to test if the players move
        /// will result in a win
        /// </summary>
        #region WinTestCases
        /// <summary>
        /// check for win condition along player move row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool rowTest(int row, bool player)
        {
            int playercount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[row, i] == !player) return false;
                if (board[row, i] == player) playercount++;
            }
            if(playercount == 2) return true;
            return false;
        }

        /// <summary>
        /// checks for win condition along player move column
        /// </summary>
        /// <param name="column"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool columnTest(int column, bool player)
        {
            int playercount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, column] == !player) return false;
                if (board[i, column] == player) playercount++;
            }
            if (playercount == 2) return true;
            return false;
        }

        /// <summary>
        /// private method check for single condition of win along 
        /// 0,0 ; 1,1 ; 2,2 line
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool diagnalDownTest(bool player)
        {
            int playerCount = 0;
            for (int i = 0, j = 0; i < 3; i++, j++)
            {
                if (board[i, j] == !player) return false;
                if (board[i, j] == player) playerCount++;
            }
            if(playerCount == 2) return true;
            return false;
        }

        /// <summary>
        /// private method check for single condition of win along 
        /// 2,0 ; 1,1 ; 0,2 line
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool diagnalUpTest(bool player)
        {
            int playerCount = 0;
            for (int i = 0, j = 2; i < 3; i++, j--)
            {
                if (board[i, j] == !player) return false;
                if (board[i, j] == player) playerCount++;
            }
            if (playerCount == 2) return true;
            return false;
        }
        #endregion
    }
}
