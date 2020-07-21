using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registration.Controllers
{
    public class GameController : Controller
    {
        public static Board gameBoard;

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        /*This method redirects the user to the appropriate page depending on selected board size*/
        public ActionResult submitGameDetails(Board m)
        {
            //Creating board and sizing it based on user choices
            gameBoard = new Board();

            m.setSizeFromRadio(m.size);
            m.setUpLiveNeighbors();
            m.calculateLiveNeighbors();

            gameBoard = m;

            //Redirecting user to appropriate game board
            switch (m.size)
            {
                case "small":
                    return View("PlaySmall", m);
                    break;
                case "medium":
                    return View("PlayMedium", m);
                    break;
                case "large":
                    return View("PlayLarge", m);
                    break;
                default:
                    return View("Index");
            }
        }

        //This method handles button clicks for the game board
        public ActionResult onButtonClick(String gameButtonValue)
        {
            //Geting location of selected button and storing within local variables
            String[] strArr = gameButtonValue.Split('|');
            int x = int.Parse(strArr[0]);
            int y = int.Parse(strArr[1]);

            //Testing if selected cell is live
            if (gameBoard.grid[x, y].live)
            {
                finishGame();
            }
       
            //Calling flood fill to open neighboring cells with zero live neighbors
            gameBoard.floodFill(x, y);

            //Checking if win conditions are met
            if (checkWin())
            {
                finishGame();
            }

            //Redirecting user back to game board
            switch (gameBoard.size)
            {
                case "small":
                    return View("PlaySmall", gameBoard);
                    break;
                case "medium":
                    return View("PlayMedium", gameBoard);
                    break;
                case "large":
                    return View("PlayLarge", gameBoard);
                    break;
                default:
                    return View("Index");
            }
        }

        /*This method checks if win conditions have been met*/
        public bool checkWin()
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

        /*This method wraps up the game by displaying full game board and win message */
        public void finishGame()
        {
            //Flag used to ensure win conditions are met
            bool flag = checkWin();

            if (flag)//win
            {
                gameBoard.win = "true";
            }
            else//loss
            {
                gameBoard.win = "false";
            }
        }

    }
}