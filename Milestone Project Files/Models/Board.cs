using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Board
    {
        public int length { get; set; }
        public int width { get; set; }
        public Cell[,] grid { get; set; }
        public String difficulty { get; set; }
        public String size { get; set; }
        public string win { get; set; }

        public Board() { win = "inProgress"; }

        public Board(String s)
        {
            s = s.ToLower();

            //Setting board grid dimensions based on selected difficulty
            switch (s)
            {
                case "small":
                    this.length = 6;
                    this.width = 6;
                    break;
                case "medium":
                    this.length = 8;
                    this.width = 8;
                    break;
                case "large":
                    this.length = 12;
                    this.width = 10;
                    break;
                default:
                    Console.WriteLine("ERROR: Inside -> Board class constructor. Board object not created.");
                    return;

            }

            grid = new Cell[this.length, this.width];

            for (int i = 0; i < this.length; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    grid[i, j] = new Cell();
                }
            }


        }

        public void setSizeFromRadio(String s)
        {
            s = s.ToLower();

            //Setting board grid dimensions based on selected difficulty
            switch (s)
            {
                case "small":
                    this.length = 6;
                    this.width = 6;
                    break;
                case "medium":
                    this.length = 8;
                    this.width = 8;
                    break;
                case "large":
                    this.length = 12;
                    this.width = 10;
                    break;
                default:
                    Console.WriteLine("ERROR: Inside -> Board class constructor. Board object not created.");
                    return;

            }

            grid = new Cell[this.length, this.width];

            for (int i = 0; i < this.length; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    grid[i, j] = new Cell();
                }
            }
        }

        public int setUpLiveNeighbors()
        {
            double temp = 0;
            int numLive = 0;
            int x, y;
            //Getting number of live tiles based on difficulty
            switch (this.difficulty)
            {
                case "easy":
                    temp = this.length * this.width;
                    temp = temp * 0.1;
                    numLive = (int)temp;
                    break;
                case "medium":
                    temp = this.length * this.width;
                    temp = temp * 0.2;
                    numLive = (int)temp;
                    break;
                case "hard":
                    temp = this.length * this.width;
                    temp = temp * 0.3;
                    numLive = (int)temp;
                    break;
                default:
                    Console.WriteLine("ERROR: Inside -> Board class setUpLiveNeighbors method.");
                    break;
            }

            /*Randomly placing bombs within game grid based on difficulty*/
            Random rand = new Random();
            for (int i = 0; i < numLive; i++)
            {
                grid[rand.Next(0, this.length), rand.Next(0, this.width)].live = true;
            }
            return numLive;
        }

        public void calculateLiveNeighbors()
        {
            for (int i = 0; i < this.length; i++)
            {
                for (int j = 0; j < this.width; j++)
                {

                    //Checking if current cell is live
                    if (grid[i, j].live == true)
                    {
                        grid[i, j].numLiveNeighbors = 9;
                    }
                    else
                    {
                        if (j - 1 >= 0) //Left
                        {
                            if (grid[i, j - 1].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (j + 1 < this.width)//Right
                        {
                            if (grid[i, j + 1].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (i + 1 < this.length)//Down
                        {
                            if (grid[i + 1, j].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (i - 1 >= 0)//Up
                        {
                            if (grid[i - 1, j].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (i - 1 >= 0 && j - 1 >= 0) //TopLeft
                        {
                            if (grid[i - 1, j - 1].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (i - 1 >= 0 && j + 1 < this.width) //TopRight
                        {
                            if (grid[i - 1, j + 1].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (i + 1 < this.length && j - 1 >= 0) //BottomLeft
                        {
                            if (grid[i + 1, j - 1].live == true) { grid[i, j].numLiveNeighbors++; }
                        }
                        if (i + 1 < this.length && j + 1 < this.width) //BottomRight
                        {
                            if (grid[i + 1, j + 1].live == true) { grid[i, j].numLiveNeighbors++; }
                        }

                    }


                }
            }
        }

        public void floodFill(int x, int y)
        {
            if (x >= 0 && x < length && y < width && y >= 0)
            {
                if (grid[x, y].numLiveNeighbors == 0 && grid[x, y].visited == false)
                {
                    grid[x, y].visited = true;
                    floodFill(x - 1, y);//upper middle
                    floodFill(x, y + 1); //middle right
                    floodFill(x + 1, y); //bottom middle
                    floodFill(x, y - 1); // Middle Left
                }
                else if (grid[x, y].visited == false && grid[x, y].numLiveNeighbors > 0)
                {
                    grid[x, y].visited = true;
                }
            }
        }
    }
}