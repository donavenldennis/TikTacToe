using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTacToe.classes
{
    /// <summary>
    /// enumeration denoting the favorite color of the player object
    /// </summary>
    #region ColorEnum
    internal enum Color
    {
        black, 
        white,
        red,
        orange,
        yellow,
        green,
        blue,
        purple
    }
    #endregion

    /// <summary>
    /// This player class will handle the creation and information management for all player objects
    /// and will be utilized as the parent class doe the computer player class
    /// </summary>
    public class Player
    {
        /// <summary>
        /// These are the class level variables(class attributes for the player class)
        /// </summary>
        #region PlayerAttributes
        protected string name;
        protected int gamesPlayed;
        protected int gamesWon;
        protected int gamesLost;
        internal Color favoriteColor;
        #endregion

        /// <summary>
        /// This is the constructor for the player class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="favoriteColor"></param>
        #region PlayerConstructor
        public Player(string name, int favoriteColor)
        { 
            this.name = name;
            this.favoriteColor = (Color) favoriteColor;
            gamesPlayed = 0;
            gamesWon = 0;
            gamesLost = 0;
        }
        #endregion

        /// <summary>
        /// These are the getters/accessors for the player class attributes
        /// including the derived attribute gamesTired
        /// </summary>
        /// <returns></returns>
        #region Getters/Accessors
        public string getName() { return name; }
        public int getGamesPlayed() { return gamesPlayed; }
        public int getGamesWon() { return gamesWon; }
        public int getGamesLost() { return gamesLost; }
        public int getGamesTied() {  return gamesPlayed - gamesWon - gamesLost; }
        #endregion

        /// <summary>
        /// This class methods will increment the appropriate values after a player object has 
        /// played a game
        /// </summary>
        #region IncrementingMethods
        public void gameWon() { gamesWon++; gamesPlayed++; }
        public void gameLost() { gamesLost++; gamesPlayed++; }
        public void gameTied() { gamesPlayed++; }
        #endregion

    }
}
