using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Cell
    {
        public int row { get; set; }
        public int column { get; set; }
        public bool visited { get; set; }
        public bool live { get; set; }
        public int numLiveNeighbors { get; set; }

        public Cell()//Default constructor
        {
            this.row = -1;
            this.column = -1;
            this.visited = false;
            this.live = false;
            this.numLiveNeighbors = 0;
        }
        public Cell(int r, int c, bool v, bool l, int n) //Non default constructor
        {
            this.row = r;
            this.column = c;
            this.visited = v;
            this.live = l;
            this.numLiveNeighbors = n;
        }
    }
}