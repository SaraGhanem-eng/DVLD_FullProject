namespace DVLD.License
{
    partial class frmIssueLicenseFirstTime
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
            this.Next = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlLocalApplicationInfo1 = new DVLD.Applications.Local_Driving_License.Controls.ctrlLocalApplicationInfo();
            this.SuspendLayout();
            // 
            // Next
            // 
            this.Next.AutoSize = true;
            this.Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Next.Location = new System.Drawing.Point(21, 400);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(65, 24);
            this.Next.TabIndex = 1;
            this.Next.Text = "Next :";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(92, 400);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(725, 74);
            this.txtNotes.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(250, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 37);
            this.label1.TabIndex = 45;
            this.label1.Text = "Issue New License";
            // 
            // btnIssue
            // 
            this.btnIssue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssue.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.Image = global::DVLD.Properties.Resources.License_View_327;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIssue.Location = new System.Drawing.Point(708, 488);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(109, 37);
            this.btnIssue.TabIndex = 44;
            this.btnIssue.Text = "Issue";
            this.btnIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnClose.Location = new System.Drawing.Point(578, 488);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 37);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlLocalApplicationInfo1
            // 
            this.ctrlLocalApplicationInfo1.Location = new System.Drawing.Point(12, 49);
            this.ctrlLocalApplicationInfo1.Name = "ctrlLocalApplicationInfo1";
            this.ctrlLocalApplicationInfo1.Size = new System.Drawing.Size(838, 338);
            this.ctrlLocalApplicationInfo1.TabIndex = 0;
            // 
            // frmIssueLicenseFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(829, 531);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.ctrlLocalApplicationInfo1);
            this.Name = "frmIssueLicenseFirstTime";
            this.Text = "Issue License For The FirstTime";
            this.Load += new System.EventHandler(this.frmIssueLicenseFirstTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.Local_Driving_License.Controls.ctrlLocalApplicationInfo ctrlLocalApplicationInfo1;
        private System.Windows.Forms.Label Next;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}