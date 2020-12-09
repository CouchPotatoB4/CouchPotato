namespace CouchPotato
{
    partial class FormVoting
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
            this.lblSwipes = new System.Windows.Forms.Label();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnDislike = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSwipes
            // 
            this.lblSwipes.AutoSize = true;
            this.lblSwipes.Location = new System.Drawing.Point(12, 9);
            this.lblSwipes.Name = "lblSwipes";
            this.lblSwipes.Size = new System.Drawing.Size(44, 13);
            this.lblSwipes.TabIndex = 0;
            this.lblSwipes.Text = "Swipes:";
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(275, 306);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(75, 23);
            this.btnLike.TabIndex = 1;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.BtnLike_Click);
            // 
            // btnDislike
            // 
            this.btnDislike.Location = new System.Drawing.Point(437, 306);
            this.btnDislike.Name = "btnDislike";
            this.btnDislike.Size = new System.Drawing.Size(75, 23);
            this.btnDislike.TabIndex = 2;
            this.btnDislike.Text = "Dislike";
            this.btnDislike.UseVisualStyleBackColor = true;
            this.btnDislike.Click += new System.EventHandler(this.BtnDislike_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(353, 269);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 13);
            this.lblTitle.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(207, 358);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(396, 170);
            this.txtDescription.TabIndex = 5;
            // 
            // FormVoting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 563);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDislike);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.lblSwipes);
            this.Name = "FormVoting";
            this.Text = "Voting";
            this.Load += new System.EventHandler(this.FrmVoting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSwipes;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnDislike;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDescription;
    }
}