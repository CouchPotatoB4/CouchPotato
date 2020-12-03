namespace CouchPotato
{
    partial class TestFrontend
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
            this.btnGet = new System.Windows.Forms.Button();
            this.btnGetFilm = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtFilm = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(13, 13);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "Get Status";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.BtnGet_Click);
            // 
            // btnGetFilm
            // 
            this.btnGetFilm.Location = new System.Drawing.Point(13, 64);
            this.btnGetFilm.Name = "btnGetFilm";
            this.btnGetFilm.Size = new System.Drawing.Size(75, 23);
            this.btnGetFilm.TabIndex = 1;
            this.btnGetFilm.Text = "Get Film";
            this.btnGetFilm.UseVisualStyleBackColor = true;
            this.btnGetFilm.Click += new System.EventHandler(this.BtnGetFilm_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(104, 13);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(673, 20);
            this.txtStatus.TabIndex = 2;
            // 
            // txtFilm
            // 
            this.txtFilm.Location = new System.Drawing.Point(104, 66);
            this.txtFilm.Multiline = true;
            this.txtFilm.Name = "txtFilm";
            this.txtFilm.Size = new System.Drawing.Size(673, 372);
            this.txtFilm.TabIndex = 3;
            // 
            // TestFrontend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtFilm);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnGetFilm);
            this.Controls.Add(this.btnGet);
            this.Name = "TestFrontend";
            this.Text = "TestFrontend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnGetFilm;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtFilm;
    }
}