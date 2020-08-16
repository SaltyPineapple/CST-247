using Milestone2.Models;
using Milestone2.Services.Business;
using Milestone2.Services.Data;
using Registration.Models;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static Stopwatch stopWatch;

        // GET: Game 
        public ActionResult Index()
        {
            if(System.Web.HttpContext.Current.Session["flag"] == null)
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
         * 
         */
        [HttpPost]
        public ActionResult submitGameDetails(Board m)
        {            
            stopWatch = new Stopwatch();
            stopWatch.Start();

            System.Web.HttpContext.Current.Session["NumClicks"] = 0;

            if (TempData["LoadedBoard"] != null)
            {
                m = (Board)TempData["LoadedBoard"];
                ViewBag.selectedPartial = gService.getPartialView(m);
                System.Web.HttpContext.Current.Session["GameBoard"] = m;
                System.Web.HttpContext.Current.Session["flag"] = true;
                return View("Play", m);
            }

            //using game servie to set up game board
            gameBoard = new Board();
            gameBoard = gService.SetUpBoard(m);
            ViewBag.selectedPartial = gService.getPartialView(gameBoard);
            System.Web.HttpContext.Current.Session["GameBoard"] = gameBoard;
            System.Web.HttpContext.Current.Session["flag"] = true;

            //Using game service to return appropriate gameBoard based on size
            return View("Play", gameBoard);
        }


        /*
         * This method handles button clicks for the game board
         * 
         */

        public ActionResult onButtonClick(String gameButtonValue)
        {
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
                if(gameBoard.win == "true")
                {
                    System.Web.HttpContext.Current.Session["flag"] = null;

                    //Stopping game time
                    stopWatch.Stop();

                    //Accessing user scores
                    SecurityService service = new SecurityService();
                    int time = (int)stopWatch.Elapsed.TotalSeconds;
                    LoginModel p = (LoginModel)System.Web.HttpContext.Current.Session["user"];

                    //Saving user score in database
                    service.SaveUserScore(new PlayerScoreModel(p.UserName, (int)System.Web.HttpContext.Current.Session["NumClicks"], time));

                    TempData["alertMessage"] = "Congratulations You have Won!\nTotal Time Taken (Seconds): " + time + "\nTotal Number of Clicks: " + System.Web.HttpContext.Current.Session["NumClicks"].ToString();
                }
                else if(gameBoard.win == "false")
                {
                    System.Web.HttpContext.Current.Session["flag"] = null;

                    //Stopping game time
                    stopWatch.Stop();
                    int time = (int)stopWatch.Elapsed.TotalSeconds;

                    TempData["alertMessage"] = "Oh No! You've Exploded And Lost!\nTotal Time Taken (Seconds): " + time + "\nTotal Number of Clicks: " + System.Web.HttpContext.Current.Session["NumClicks"].ToString();
                }
            }            

            //Using game service to return appropriate gameBoard based on size
            return PartialView(gService.getPartialView(gameBoard), gameBoard);
        }

        [HttpPost]
        public void SaveGameState()
        {
            SecurityService service = new SecurityService();
            service.SaveGameState(gameBoard);
        }

        [HttpPost]
        public void GetGameState()
        {
            SecurityService service = new SecurityService();
            var board = service.GetGameState();
            if(board == null)
            {

            }
            else
            {
                TempData["LoadedBoard"] = board;
                RedirectToAction("Index");
            }
            //submitGameDetails(board);
        }

        public ActionResult StartNewGame()
        {
            System.Web.HttpContext.Current.Session["flag"] = null;
            System.Web.HttpContext.Current.Session["GameBoard"] = null;

            return RedirectToAction("Index");
        }

    }
}