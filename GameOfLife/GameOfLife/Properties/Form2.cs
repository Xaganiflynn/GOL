using System;
using System.Collections.Generic;
using System.ComponentModel;
using GameOfLife.Properties;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife.Properties
{
    public partial class Color : Form
    {

        public Color Seed { get; set; }
        public Color()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buttonGColor_Click(object sender, EventArgs e)
        {
            ColorDialog colors = new ColorDialog();
            colors.Color = Settings.Default.GridColor;

            if (DialogResult.OK == colors.ShowDialog())
            {
                Settings.Default.GridColor = colors.Color;
            }
        }

        private void buttonBGColor_Click(object sender, EventArgs e)
        {
            ColorDialog colors = new ColorDialog();
            colors.Color = Settings.Default.BGColor;

            if (DialogResult.OK == colors.ShowDialog())
            {
                Settings.Default.BGColor = colors.Color;
            }
        }

        private void buttonCellColor_Click(object sender, EventArgs e)
        {
            ColorDialog colors = new ColorDialog();
            colors.Color = Settings.Default.CellColor;

            if (DialogResult.OK == colors.ShowDialog())
            {
                Settings.Default.CellColor = colors.Color;
            }
        }
    }
}
