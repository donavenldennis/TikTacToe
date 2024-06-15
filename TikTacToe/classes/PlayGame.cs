using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace TikTacToe.classes
{
    class PlayGame
    {
        /// <summary>
        /// Attributes for the play game class
        /// </summary>
        #region PlayGameClassVariables
        private List<Player> players;
        private int playerX;
        private int playerO;
        private GameBoard board;
        private bool playersTurn;//false is X, true is O
        private bool? winner;
        private int turnsTaken;
        #endregion

        public PlayGame()
        {
            players = new List<Player>();
            board = new GameBoard();
            //Addplayers()
        }

        public void NewGame(int playerX, int PlayerO)
        { 
        
        }

        public bool takeTurn(int row, int column)
        {
            bool[] returnCode = board.takeTurn(playersTurn, row, column);
            if (returnCode[0]) 
            {
                turnsTaken++;
            }
            if (returnCode[1])
            {
                winner = playersTurn;
            }
            return false;
        }
    }
}
