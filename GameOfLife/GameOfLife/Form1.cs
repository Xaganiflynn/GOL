using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        static int xSize = 10;
        static int ySize = 10;

        // The universes array
        bool[,] universe = new bool[xSize, ySize];
        bool[,] ScrtchUni = new bool[xSize, ySize];

        int N_NotEnough = 2;
        int N_TooMuch = 3;

        int[] LifeRange = { 2, 3 };

        int TheBirth = 3;

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.DarkGreen;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    ScrtchUni[x, y] = false;
                    int count = Counter(x, y);
                    if (universe[x, y] == true)
                    {
                        if (count < N_NotEnough)
                        {
                            ScrtchUni[x, y] = false;
                        }

                        if (count > N_TooMuch)
                        {
                            ScrtchUni[x, y] = false;
                        }
                        if (count >= LifeRange[0] && count <= LifeRange[1])
                        {
                            ScrtchUni[x, y] = true;
                        }
                    }
                    if (universe[x, y] == false && count == TheBirth)
                    {
                        ScrtchUni[x, y] = true;
                    }
                }
                graphicsPanel1.Invalidate();
            }
            bool[,] temp = universe;
            universe = ScrtchUni;
            ScrtchUni = temp;

            #region OG code
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            #endregion

        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private int Counter(int x, int y)
        {
            int count = 0;
            int xL = universe.GetLength(0);
            int yL = universe.GetLength(1);

            for (int Yoffset = -1; Yoffset <= 1; Yoffset++)
            {
                for (int Xoffset = -1; Xoffset <= 1; Xoffset++)
                {
                    int xCheck = x + Xoffset;
                    int yCheck = y + Yoffset;

                    // if y and x are equal to 0, continue
                    if (Xoffset == 0 && Yoffset == 0)
                        continue;

                    // if xcheck is less tha 0 then set xlen-1 and viceversa
                    if (xCheck < 0)
                        xCheck = xL - 1;

                    if (yCheck < 0)
                        yCheck = yL - 1;

                    // if xcheck is greater than or equal to xlen then set to 0 and viceversa
                    if (xCheck >= xL)
                        xCheck = 0;

                    if (yCheck >= yL)
                        yCheck = 0;

                    if (universe[xCheck, yCheck] == true)
                        count++;

                }
            }
            return count;
        }

        private void Pause_Button(object sender, EventArgs e)
        {
            timer.Enabled = false;
            graphicsPanel1.Invalidate();

        }

        private void Play_Button(object sender, EventArgs e)
        {
            timer.Enabled = true;

        }

        private void Next_Generation(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();

        }

        private void New_Button(object sender, EventArgs e)
        {
            int count = generations;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                    generations = 0 - 1;
                    Next_Generation(sender, e);
                    graphicsPanel1.Invalidate();
                }
            }
            timer.Enabled = false;
        }

        private void CellRandom(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int y = 0; y < universe.GetLength(0); y++)
            {
                for (int x = 0; x < universe.GetLength(1); x++)
                {
                    int randValue = rand.Next(0, 2);

                    if (randValue == 0)
                    {
                        universe[x, y] = true;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }
            //shows the pain
            graphicsPanel1.Invalidate();
        }



    }
}
