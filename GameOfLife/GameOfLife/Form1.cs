using System;
using GameOfLife.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        System.Drawing.Color gridColor = Properties.Settings.Default.GridColor;
        System.Drawing.Color cellColor = System.Drawing.Color.DarkGreen;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;
        int Alivecells = 0;

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
                            Alivecells++;
                        }

                        if (count > N_TooMuch)
                        {
                            ScrtchUni[x, y] = false;
                            Alivecells++;
                        }
                        if (count >= LifeRange[0] && count <= LifeRange[1])
                        {
                            ScrtchUni[x, y] = true;
                            Alivecells++;
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

            // Update status strip generations and Cell Count
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabel1.Text = "Cell Count = " + Alivecells.ToString();
            graphicsPanel1.Invalidate();
            Alivecells = 0;

            graphicsPanel1.Invalidate();

            #endregion

        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        public void graphicsPanel1_Paint(object sender, PaintEventArgs e)
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


                    //neighbor count
                    Font font = new Font("Arial", 20f);
                    int neighbors = Counter(x, y);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if (Counter(x, y) == 0)
                    {
                    }
                    else if (Counter(x, y) == 1)
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.DarkRed, cellRect, stringFormat);
                    }
                    else if (Counter(x, y) == 2)
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Green, cellRect, stringFormat);
                    }
                    else if (Counter(x, y) == 3)
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.GreenYellow, cellRect, stringFormat);
                    }
                    else if (Counter(x, y) == 4)
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.IndianRed, cellRect, stringFormat);

                    }
                    else
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Red, cellRect, stringFormat);
                    }



                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        public void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
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
                    Alivecells++;
                    Alivecells = 0;
                    graphicsPanel1.Invalidate();
                }
            }
            catch (Exception)
            {
            }
            // If the left mouse button was clicked

        }

        #region Methods
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
        #endregion

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
            //paints the cells
            graphicsPanel1.Invalidate();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                writer.WriteLine("!Currently Saving...");

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] == true)
                        {
                            currentRow += 'O';
                            //universe[x, y].ToString();
                        }
                        else if (universe[x, y] == false)
                        {
                            currentRow += '.';
                        }
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }
                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row[0] == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.
                    else
                    {
                        maxHeight += 1;
                        maxWidth = row.Length;
                    }
                    // Get the length of the current row string
                    // and adjust the maxWidth variable if necessary.
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.

                #region NeedMoreWork
                universe = new bool [maxWidth, maxHeight];
                ScrtchUni = new bool [maxWidth, maxHeight];
                #endregion


                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int yPos = 0;

                // Iterate through the file again, this time reading in the cells.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row[0] == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.
                    for (int xPos = 0; xPos < row.Length; xPos++)
                    {
                        // If row[xPos] is a 'O' (capital O) then
                        // set the corresponding cell in the universe to alive.
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, yPos] = true;
                        }
                        // If row[xPos] is a '.' (period) then
                        // set the corresponding cell in the universe to dead.
                        else if (row[xPos] == '.')
                        {
                            universe[xPos, yPos] = false;
                        }

                    }
                    yPos++;
                }

                // Close the file.
                reader.Close();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                writer.WriteLine("!Currently Saving...");

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] == true)
                        {
                            currentRow += 'O';
                            //universe[x, y].ToString();
                        }
                        else if (universe[x, y] == false)
                        {
                            currentRow += '.';
                        }
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }
                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Color form2 = new Properties.Color();

            if (DialogResult.OK == form2.ShowDialog())
            {
                int x = 10;
            }


            //ColorDialog colors = new ColorDialog();
            //colors.Color = gridColor;

            //if(DialogResult.OK == colors.ShowDialog())
            //{
            //    gridColor = colors.Color;
            //    graphicsPanel1.Invalidate();
            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Update Property
            Properties.Settings.Default.GridColor = gridColor;

            Properties.Settings.Default.Save();
        }
    }
}
