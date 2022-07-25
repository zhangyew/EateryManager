
namespace 实力产业管理系统
{
    partial class AddPTypeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBH = new System.Windows.Forms.TextBox();
            this.txtMC = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtQT = new System.Windows.Forms.RadioButton();
            this.rbtJS = new System.Windows.Forms.RadioButton();
            this.rbtCP = new System.Windows.Forms.RadioButton();
            this.button8 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "类别编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "类别名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "类别属性：";
            // 
            // txtBH
            // 
            this.txtBH.Location = new System.Drawing.Point(91, 20);
            this.txtBH.Name = "txtBH";
            this.txtBH.ReadOnly = true;
            this.txtBH.Size = new System.Drawing.Size(209, 23);
            this.txtBH.TabIndex = 3;
            // 
            // txtMC
            // 
            this.txtMC.Location = new System.Drawing.Point(91, 62);
            this.txtMC.Name = "txtMC";
            this.txtMC.Size = new System.Drawing.Size(209, 23);
            this.txtMC.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtQT);
            this.panel1.Controls.Add(this.rbtJS);
            this.panel1.Controls.Add(this.rbtCP);
            this.panel1.Location = new System.Drawing.Point(91, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 56);
            this.panel1.TabIndex = 6;
            // 
            // rbtQT
            // 
            this.rbtQT.AutoSize = true;
            this.rbtQT.Location = new System.Drawing.Point(144, 14);
            this.rbtQT.Name = "rbtQT";
            this.rbtQT.Size = new System.Drawing.Size(50, 21);
            this.rbtQT.TabIndex = 2;
            this.rbtQT.TabStop = true;
            this.rbtQT.Text = "其他";
            this.rbtQT.UseVisualStyleBackColor = true;
            // 
            // rbtJS
            // 
            this.rbtJS.AutoSize = true;
            this.rbtJS.Location = new System.Drawing.Point(78, 14);
            this.rbtJS.Name = "rbtJS";
            this.rbtJS.Size = new System.Drawing.Size(62, 21);
            this.rbtJS.TabIndex = 1;
            this.rbtJS.TabStop = true;
            this.rbtJS.Text = "酒水类";
            this.rbtJS.UseVisualStyleBackColor = true;
            // 
            // rbtCP
            // 
            this.rbtCP.AutoSize = true;
            this.rbtCP.Location = new System.Drawing.Point(10, 14);
            this.rbtCP.Name = "rbtCP";
            this.rbtCP.Size = new System.Drawing.Size(62, 21);
            this.rbtCP.TabIndex = 0;
            this.rbtCP.TabStop = true;
            this.rbtCP.Text = "菜品类";
            this.rbtCP.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(239)))));
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.Location = new System.Drawing.Point(209, 163);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(76, 26);
            this.button8.TabIndex = 17;
            this.button8.Text = "取消";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(239)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(61, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 26);
            this.button1.TabIndex = 18;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddPTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(343, 200);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtMC);
            this.Controls.Add(this.txtBH);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPTypeForm";
            this.Text = "增加项目类别";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBH;
        private System.Windows.Forms.TextBox txtMC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtCP;
        private System.Windows.Forms.RadioButton rbtQT;
        private System.Windows.Forms.RadioButton rbtJS;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button1;
    }
}