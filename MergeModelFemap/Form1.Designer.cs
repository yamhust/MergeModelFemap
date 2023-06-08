namespace MergeModelFemap
{
    partial class MergeFemapModel
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
            System.Windows.Forms.Button button1;
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btMerge = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ckb1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(91, 26);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(126, 39);
            button1.TabIndex = 0;
            button1.Text = "Merge Property";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(91, 29);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(462, 22);
            this.tbPath.TabIndex = 0;
            this.tbPath.UseWaitCursor = true;
            // 
            // btMerge
            // 
            this.btMerge.Location = new System.Drawing.Point(527, 87);
            this.btMerge.Name = "btMerge";
            this.btMerge.Size = new System.Drawing.Size(99, 29);
            this.btMerge.TabIndex = 1;
            this.btMerge.Text = "Assembly";
            this.btMerge.UseVisualStyleBackColor = true;
            this.btMerge.Click += new System.EventHandler(this.btMerge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Folder Path";
            // 
            // ckb1
            // 
            this.ckb1.AutoSize = true;
            this.ckb1.Location = new System.Drawing.Point(91, 71);
            this.ckb1.Name = "ckb1";
            this.ckb1.Size = new System.Drawing.Size(228, 21);
            this.ckb1.TabIndex = 3;
            this.ckb1.Text = "Merge Property and Rearrange";
            this.ckb1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.btn1);
            this.groupBox1.Controls.Add(this.btMerge);
            this.groupBox1.Controls.Add(this.ckb1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbPath);
            this.groupBox1.Location = new System.Drawing.Point(18, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 137);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Parameter";
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(557, 27);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(69, 26);
            this.btn1.TabIndex = 4;
            this.btn1.Text = "Browser";
            this.btn1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(button1);
            this.groupBox2.Location = new System.Drawing.Point(18, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(632, 81);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Option";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(91, 102);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(217, 21);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Hold Origrin PID and Layer ID";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // MergeFemapModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 258);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MergeFemapModel";
            this.Text = "Merge Femap Model 2023ver1.0.1 by CanhPM";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btMerge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckb1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

