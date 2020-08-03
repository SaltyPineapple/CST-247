using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Registration.Models;

namespace Registration.Services
{
    public class GameService
    {
        
        public String DirectToGame(String size)
        {
            //Redirecting user back to game board
            switch (size)
            {
                case "small":
                    return "PlaySmall";
                    break;
                case "medium":
                    return "PlayMedium";
                    break;
                case "large":
                    return "PlayLarge";
                    break;
                default:
                    return "Index";
            }
        }

        public Board SetUpBoard(Board b)
        {
            b.setSizeFromRadio(b.size);
            b.setUpLiveNeighbors();
            b.calculateLiveNeighbors();

            return b;
        }

        public bool checkWin(Board gameBoard)
        {
            int numBombs = 0;
            int unvisitedCells = 0;

            for (int i = 0; i < gameBoard.length; i++)
            {
                for (int j = 0; j < gameBoard.width; j++)
                {
                    if (!gameBoard.grid[i, j].visited)
                    {
                        unvisitedCells++;
                    }
                    if (gameBoard.grid[i, j].live)
                    {
                        numBombs++;
                    }
                }
            }

            /*If spaces left unvisited are equal to amount of bombs*/
            if (numBombs == unvisitedCells)
            {
                return true;//Win
            }
            else { return false; }//Not win
        }

        /*
         * This method wraps up the game by displaying full game board and win message 
         * 
         */
        public string finishGame(Board gameBoard)
        {
            //Flag used to ensure win conditions are met
            bool flag = checkWin(gameBoard);

            if (flag)//win
            {
                return "true";
            }
            else//loss
            {
 
                return "false";
            }
        }

        public String getPartialView(Board gameboard)
        {
            switch (gameboard.size)
            {
                case "small":
                    return "~/Views/Game/_PlaySmall.cshtml";
                    break;
                case "medium":
                    return "~/Views/Game/_PlayMedium.cshtml";
                    break;
                case "large":
                    return "~/Views/Game/_PlayLarge.cshtml";
                    break;
                default:
                    return "";
            }
        }

    }
}