namespace phdesign.NppToolBucket.Forms
{
    partial class frmFootnoteXML
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
            this.txtFootnote = new System.Windows.Forms.TextBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.rdAllDocument = new System.Windows.Forms.RadioButton();
            this.rdOpinion = new System.Windows.Forms.RadioButton();
            this.rdConcur = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdDissent = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFootnote
            // 
            this.txtFootnote.Location = new System.Drawing.Point(69, 30);
            this.txtFootnote.Name = "txtFootnote";
            this.txtFootnote.Size = new System.Drawing.Size(100, 20);
            this.txtFootnote.TabIndex = 0;
            this.txtFootnote.Text = "1";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(69, 56);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(100, 20);
            this.txtToken.TabIndex = 0;
            this.txtToken.Text = "1";
            // 
            // rdAllDocument
            // 
            this.rdAllDocument.AutoSize = true;
            this.rdAllDocument.Location = new System.Drawing.Point(209, 31);
            this.rdAllDocument.Name = "rdAllDocument";
            this.rdAllDocument.Size = new System.Drawing.Size(88, 17);
            this.rdAllDocument.TabIndex = 1;
            this.rdAllDocument.TabStop = true;
            this.rdAllDocument.Text = "All Document";
            this.rdAllDocument.UseVisualStyleBackColor = true;
            this.rdAllDocument.Visible = false;
            // 
            // rdOpinion
            // 
            this.rdOpinion.AutoSize = true;
            this.rdOpinion.Location = new System.Drawing.Point(209, 54);
            this.rdOpinion.Name = "rdOpinion";
            this.rdOpinion.Size = new System.Drawing.Size(85, 17);
            this.rdOpinion.TabIndex = 1;
            this.rdOpinion.TabStop = true;
            this.rdOpinion.Text = "Opinion Only";
            this.rdOpinion.UseVisualStyleBackColor = true;
            this.rdOpinion.Visible = false;
            // 
            // rdConcur
            // 
            this.rdConcur.AutoSize = true;
            this.rdConcur.Location = new System.Drawing.Point(209, 77);
            this.rdConcur.Name = "rdConcur";
            this.rdConcur.Size = new System.Drawing.Size(85, 17);
            this.rdConcur.TabIndex = 1;
            this.rdConcur.TabStop = true;
            this.rdConcur.Text = "radioButton1";
            this.rdConcur.UseVisualStyleBackColor = true;
            this.rdConcur.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "footnote";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "token";
            // 
            // rdDissent
            // 
            this.rdDissent.AutoSize = true;
            this.rdDissent.Location = new System.Drawing.Point(209, 100);
            this.rdDissent.Name = "rdDissent";
            this.rdDissent.Size = new System.Drawing.Size(85, 17);
            this.rdDissent.TabIndex = 1;
            this.rdDissent.TabStop = true;
            this.rdDissent.Text = "radioButton1";
            this.rdDissent.UseVisualStyleBackColor = true;
            this.rdDissent.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmFootnoteXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 120);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdDissent);
            this.Controls.Add(this.rdConcur);
            this.Controls.Add(this.rdOpinion);
            this.Controls.Add(this.rdAllDocument);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.txtFootnote);
            this.Name = "frmFootnoteXML";
            this.Text = "HH";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFootnote;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.RadioButton rdAllDocument;
        private System.Windows.Forms.RadioButton rdOpinion;
        private System.Windows.Forms.RadioButton rdConcur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdDissent;
        private System.Windows.Forms.Button button1;
    }
}