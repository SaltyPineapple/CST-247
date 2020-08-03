using Registration.Models;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registration.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        public static Board gameBoard;
        private static GameService gService = new GameService();

        // GET: Game
        
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        /*
         * This method redirects the user to the appropriate page depending on selected board size
         * 
         */
        public ActionResult submitGameDetails(Board m)
        {
            //using game servie to set up game board
            gameBoard = new Board();
            gameBoard = gService.SetUpBoard(m);

            //Using game service to return appropriate gameBoard based on size
            return View(gService.DirectToGame(gameBoard.size), gameBoard);
        }


        /*
         * This method handles button clicks for the game board
         * 
         */
        public ActionResult onButtonClick(String gameButtonValue)
        {
            //Geting location of selected button and storing within local variables
            String[] strArr = gameButtonValue.Split('|');
            int x = int.Parse(strArr[0]);
            int y = int.Parse(strArr[1]);

            //Testing if selected cell is live
            if (gameBoard.grid[x, y].live)
            {
                gameBoard.win = gService.finishGame(gameBoard);
            }
       
            //Calling flood fill to open neighboring cells with zero live neighbors
            gameBoard.floodFill(x, y);

            //Checking if win conditions are met
            if (gService.checkWin(gameBoard) || gameBoard.win == "false")
            {
                gameBoard.win = gService.finishGame(gameBoard);
                if(gameBoard.win == "true")
                {
                    TempData["alertMessage"] = "Congratulations You have Won!";
                }
                else if(gameBoard.win == "false")
                {
                    TempData["alertMessage"] = "Oh No! You've Exploded And Lost!!";
                }
            }            

            //Using game service to return appropriate gameBoard based on size
            return View(gService.DirectToGame(gameBoard.size), gameBoard);
        }

    }
}