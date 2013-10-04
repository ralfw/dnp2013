namespace json.uiproxy
{
    partial class JsonPortalDlg
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
            this.cboJsonTemplates = new System.Windows.Forms.ComboBox();
            this.txtJsonInput = new System.Windows.Forms.TextBox();
            this.btnSenden = new System.Windows.Forms.Button();
            this.lvJsonIO = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // cboJsonTemplates
            // 
            this.cboJsonTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboJsonTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJsonTemplates.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboJsonTemplates.FormattingEnabled = true;
            this.cboJsonTemplates.Location = new System.Drawing.Point(12, 12);
            this.cboJsonTemplates.Name = "cboJsonTemplates";
            this.cboJsonTemplates.Size = new System.Drawing.Size(328, 22);
            this.cboJsonTemplates.TabIndex = 0;
            this.cboJsonTemplates.SelectedIndexChanged += new System.EventHandler(this.cboJsonTemplates_SelectedIndexChanged);
            // 
            // txtJsonInput
            // 
            this.txtJsonInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJsonInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJsonInput.Location = new System.Drawing.Point(12, 39);
            this.txtJsonInput.Multiline = true;
            this.txtJsonInput.Name = "txtJsonInput";
            this.txtJsonInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtJsonInput.Size = new System.Drawing.Size(328, 149);
            this.txtJsonInput.TabIndex = 1;
            this.txtJsonInput.WordWrap = false;
            // 
            // btnSenden
            // 
            this.btnSenden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSenden.Location = new System.Drawing.Point(265, 194);
            this.btnSenden.Name = "btnSenden";
            this.btnSenden.Size = new System.Drawing.Size(75, 23);
            this.btnSenden.TabIndex = 2;
            this.btnSenden.Text = "Senden";
            this.btnSenden.UseVisualStyleBackColor = true;
            this.btnSenden.Click += new System.EventHandler(this.btnSenden_Click);
            // 
            // lvJsonIO
            // 
            this.lvJsonIO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvJsonIO.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvJsonIO.Location = new System.Drawing.Point(12, 223);
            this.lvJsonIO.Name = "lvJsonIO";
            this.lvJsonIO.Size = new System.Drawing.Size(328, 321);
            this.lvJsonIO.TabIndex = 3;
            this.lvJsonIO.UseCompatibleStateImageBehavior = false;
            this.lvJsonIO.View = System.Windows.Forms.View.List;
            // 
            // JsonPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 556);
            this.Controls.Add(this.lvJsonIO);
            this.Controls.Add(this.btnSenden);
            this.Controls.Add(this.txtJsonInput);
            this.Controls.Add(this.cboJsonTemplates);
            this.Name = "JsonPortal";
            this.Text = "JsonPortal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboJsonTemplates;
        private System.Windows.Forms.TextBox txtJsonInput;
        private System.Windows.Forms.Button btnSenden;
        private System.Windows.Forms.ListView lvJsonIO;
    }
}