namespace MyRevisionHelper
{
    partial class Create
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create));
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.createQna_btn = new System.Windows.Forms.Button();
            this.createWordedQ_btn = new System.Windows.Forms.Button();
            this.createMathQ_btn = new System.Windows.Forms.Button();
            this.createTimedMC_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // homeIcon
            // 
            this.homeIcon.Image = ((System.Drawing.Image)(resources.GetObject("homeIcon.Image")));
            this.homeIcon.Location = new System.Drawing.Point(5, 5);
            this.homeIcon.Name = "homeIcon";
            this.homeIcon.Size = new System.Drawing.Size(16, 16);
            this.homeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeIcon.TabIndex = 10;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.homeIcon_Click);
            // 
            // createQna_btn
            // 
            this.createQna_btn.Location = new System.Drawing.Point(27, 12);
            this.createQna_btn.Name = "createQna_btn";
            this.createQna_btn.Size = new System.Drawing.Size(100, 100);
            this.createQna_btn.TabIndex = 11;
            this.createQna_btn.Text = "Create a simple question and answer activity";
            this.createQna_btn.UseVisualStyleBackColor = true;
            this.createQna_btn.Click += new System.EventHandler(this.createQna_btn_Click);
            // 
            // createWordedQ_btn
            // 
            this.createWordedQ_btn.Location = new System.Drawing.Point(133, 12);
            this.createWordedQ_btn.Name = "createWordedQ_btn";
            this.createWordedQ_btn.Size = new System.Drawing.Size(100, 100);
            this.createWordedQ_btn.TabIndex = 12;
            this.createWordedQ_btn.Text = "Create a question that requires a worded answer";
            this.createWordedQ_btn.UseVisualStyleBackColor = true;
            this.createWordedQ_btn.Click += new System.EventHandler(this.createWordedQ_btn_Click);
            // 
            // createMathQ_btn
            // 
            this.createMathQ_btn.Location = new System.Drawing.Point(27, 118);
            this.createMathQ_btn.Name = "createMathQ_btn";
            this.createMathQ_btn.Size = new System.Drawing.Size(100, 100);
            this.createMathQ_btn.TabIndex = 13;
            this.createMathQ_btn.Text = "Create a math question";
            this.createMathQ_btn.UseVisualStyleBackColor = true;
            this.createMathQ_btn.Click += new System.EventHandler(this.createMathQ_btn_Click);
            // 
            // createTimedMC_btn
            // 
            this.createTimedMC_btn.Location = new System.Drawing.Point(133, 118);
            this.createTimedMC_btn.Name = "createTimedMC_btn";
            this.createTimedMC_btn.Size = new System.Drawing.Size(100, 100);
            this.createTimedMC_btn.TabIndex = 14;
            this.createTimedMC_btn.Text = "Create a multiple choice activity";
            this.createTimedMC_btn.UseVisualStyleBackColor = true;
            this.createTimedMC_btn.Click += new System.EventHandler(this.createTimedMC_btn_Click);
            // 
            // Create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 231);
            this.Controls.Add(this.createTimedMC_btn);
            this.Controls.Add(this.createMathQ_btn);
            this.Controls.Add(this.createWordedQ_btn);
            this.Controls.Add(this.createQna_btn);
            this.Controls.Add(this.homeIcon);
            this.Name = "Create";
            this.Text = "Create";
            this.Load += new System.EventHandler(this.Create_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Button createQna_btn;
        private System.Windows.Forms.Button createWordedQ_btn;
        private System.Windows.Forms.Button createMathQ_btn;
        private System.Windows.Forms.Button createTimedMC_btn;
    }
}