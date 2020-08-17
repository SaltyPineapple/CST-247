using Milestone2.Models;
using Milestone2.Services.Business;
using Milestone2.Services.Data;
using Registration.Models;
using Registration.Services;
using Registration.Services.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registration.Controllers
{
    [CustomAuthorization]
    public class GameController : Controller
    {
        //Declaring game params
        public static Board gameBoard;
        private static GameService gService = new GameService();
        private static Stopwatch stopWatch;

        // GET: Game 
        public ActionResult Index()
        {
            //Logging
            MyLogger.GetInstance().Info(" Entering Game Controller");

            //Determing if game state needs to be reset
            if (System.Web.HttpContext.Current.Session["flag"] == null)
            {
                return View();
            }
            else
            { 
                TempData["LoadedBoard"] = null;
                return View();
            }         
        }



        /*
         * This method redirects the user to the appropriate page depending on selected board size        
         */
        [HttpPost]
        public ActionResult submitGameDetails(Board m)
        {
            //Logging
            MyLogger.GetInstance().Info(" Entering submitGameDetails method inside Game Controller");

            //Starting Game Timer
            stopWatch = new Stopwatch();
            stopWatch.Start();

            //Initializing click counter in session
            System.Web.HttpContext.Current.Session["NumClicks"] = 0;

            //If Game board has been loaded into tempdata, use this data to populate game board
            if (TempData["LoadedBoard"] != null)
            {
                m = (Board)TempData["LoadedBoard"];
                ViewBag.selectedPartial = gService.getPartialView(m);
                System.Web.HttpContext.Current.Session["GameBoard"] = m;
                System.Web.HttpContext.Current.Session["flag"] = true;

                //Logging
                MyLogger.GetInstance().Info(" Game board created --> Inside submitGameDetails method in Game controller");

                return View("Play", m);
            }

            //using game servie to set up new Game board
            gameBoard = new Board();
            gameBoard = gService.SetUpBoard(m);
            ViewBag.selectedPartial = gService.getPartialView(gameBoard);

            //Saving game board and flag variables into sesion variable
            System.Web.HttpContext.Current.Session["GameBoard"] = gameBoard;
            System.Web.HttpContext.Current.Session["flag"] = true;

            //Logging
            MyLogger.GetInstance().Info(" Game board created --> Inside submitGameDetails method in Game controller");

            //Using game service to return appropriate gameBoard based on size
            return View("Play", gameBoard);
        }


        /*
         * This method handles button clicks for the game board
         */
        public ActionResult onButtonClick(String gameButtonValue)
        {
            //Logging
            MyLogger.GetInstance().Info(" Inside onButtonClick method inside Game controller");

            //Iterating click counter
            System.Web.HttpContext.Current.Session["NumClicks"] = ((int)System.Web.HttpContext.Current.Session["NumClicks"]) + 1; //iterating clicks
            int clicks = (int)System.Web.HttpContext.Current.Session["NumClicks"];

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

                //If true game has been won
                if(gameBoard.win == "true")
                {
                    MyLogger.GetInstance().Info(" Game has been won --> Inside GameController");

                    System.Web.HttpContext.Current.Session["flag"] = null;

                    //Stopping game time
                    stopWatch.Stop();

                    //Accessing user scores
                    SecurityService service = new SecurityService();
                    int time = (int)stopWatch.Elapsed.TotalSeconds;
                    LoginModel p = (LoginModel)System.Web.HttpContext.Current.Session["user"];

                    //Saving user score in database
                    service.SaveUserScore(new PlayerScoreModel(p.UserName, (int)System.Web.HttpContext.Current.Session["NumClicks"], time));

                    //Saving alert message for display
                    TempData["alertMessage"] = "Congratulations You have Won!\nTotal Time Taken (Seconds): " + time + "\nTotal Number of Clicks: " + System.Web.HttpContext.Current.Session["NumClicks"].ToString();
                }
                //If false game has been lost
                else if(gameBoard.win == "false")
                {
                    //Logging
                    MyLogger.GetInstance().Info(" Game has been lost --> Inside GameController");

                    System.Web.HttpContext.Current.Session["flag"] = null;

                    //Stopping game time
                    stopWatch.Stop();
                    int time = (int)stopWatch.Elapsed.TotalSeconds;

                    //Saving alert message for display
                    TempData["alertMessage"] = "Oh No! You've Exploded And Lost!\nTotal Time Taken (Seconds): " + time + "\nTotal Number of Clicks: " + System.Web.HttpContext.Current.Session["NumClicks"].ToString();
                }
            }            

            //Using game service to return appropriate gameBoard based on size
            return PartialView(gService.getPartialView(gameBoard), gameBoard);
        }

        /*
         *This method saves the current game state to a txt file  
         */
        [HttpPost]
        public void SaveGameState()
        {
            //Logging
            MyLogger.GetInstance().Info(" Saving current Game State --> Inside SaveGameState method ");

            SecurityService service = new SecurityService();
            service.SaveGameState(gameBoard);
        }

        /*
         * This method retrieves game state from the txt file to populate the game board
         */
        [HttpPost]
        public void GetGameState()
        {
            //Logging
            MyLogger.GetInstance().Info(" Retrieving saved Game State --> Inside GetGameState method");

            SecurityService service = new SecurityService();
            var board = service.GetGameState();
            if(board != null)
            {
                //Saving game board in temp data and redirecting to game page
                TempData["LoadedBoard"] = board;
                RedirectToAction("Index");
            }            
            //submitGameDetails(board);
        }

        /*
         * This method clears game session variables and resets game state
         */
        public ActionResult StartNewGame()
        {
            //Logging
            MyLogger.GetInstance().Info(" Resetting Game State --> Inside StartNewGame method");

            System.Web.HttpContext.Current.Session["flag"] = null;
            System.Web.HttpContext.Current.Session["GameBoard"] = null;

            return RedirectToAction("Index");
        }

    }
}