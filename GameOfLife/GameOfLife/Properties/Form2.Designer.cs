namespace GameOfLife.Properties
{
    partial class Color
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCellColor = new System.Windows.Forms.Button();
            this.buttonGColor = new System.Windows.Forms.Button();
            this.buttonBGColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(91, 193);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(12, 193);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCellColor
            // 
            this.buttonCellColor.Location = new System.Drawing.Point(38, 12);
            this.buttonCellColor.Name = "buttonCellColor";
            this.buttonCellColor.Size = new System.Drawing.Size(100, 36);
            this.buttonCellColor.TabIndex = 3;
            this.buttonCellColor.Text = "Cell Color";
            this.buttonCellColor.UseVisualStyleBackColor = true;
            // 
            // buttonGColor
            // 
            this.buttonGColor.Location = new System.Drawing.Point(38, 68);
            this.buttonGColor.Name = "buttonGColor";
            this.buttonGColor.Size = new System.Drawing.Size(100, 36);
            this.buttonGColor.TabIndex = 4;
            this.buttonGColor.Text = "Grid Color";
            this.buttonGColor.UseVisualStyleBackColor = true;
            this.buttonGColor.Click += new System.EventHandler(this.buttonGColor_Click);
            // 
            // buttonBGColor
            // 
            this.buttonBGColor.Location = new System.Drawing.Point(38, 127);
            this.buttonBGColor.Name = "buttonBGColor";
            this.buttonBGColor.Size = new System.Drawing.Size(100, 36);
            this.buttonBGColor.TabIndex = 5;
            this.buttonBGColor.Text = "Background Color";
            this.buttonBGColor.UseVisualStyleBackColor = true;
            this.buttonBGColor.Click += new System.EventHandler(this.buttonBGColor_Click);
            // 
            // Color
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 223);
            this.Controls.Add(this.buttonBGColor);
            this.Controls.Add(this.buttonGColor);
            this.Controls.Add(this.buttonCellColor);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Color";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCellColor;
        private System.Windows.Forms.Button buttonGColor;
        private System.Windows.Forms.Button buttonBGColor;
    }
}