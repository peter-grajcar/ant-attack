using System.Drawing;
using System.Windows.Forms;

namespace AntAttack
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            timer = new System.Windows.Forms.Timer(this.components);
            canvas= new PictureBox();
            this.SuspendLayout();
            //
            // canvas
            //
            canvas.Location = new System.Drawing.Point(0, 0);
            canvas.Size = new System.Drawing.Size(800, 600);
            canvas.TabIndex = 1;
            canvas.TabStop = false;
            //q
            // timer
            //
            timer.Tick += OnTick;
            timer.Interval = 20;
            // 
            // Form1
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Ant Attack";
            this.ResumeLayout(false);

            this.Controls.Add(canvas);
            this.ResumeLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox canvas;
    }
}

