using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife.Properties
{
    public partial class UniverSize : Form
    {
        public UniverSize()
        {
            InitializeComponent();
        }

        public int GetNumber1()
        {
            return (int)numericUpDown1.Value;
        }
        public void SetNumber1(int number)
        {
            numericUpDown1.Value = number;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            UniverSize numbers = new UniverSize();
            //numbers.SetNumber1(xSize);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
