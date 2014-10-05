namespace EventsCsClient
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.LabelBearer = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.SiteUrlTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddEvent = new System.Windows.Forms.Button();
            this.GetEventsBtn = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.WaitLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "user";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "123456";
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(118, 71);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(75, 23);
            this.LoginBtn.TabIndex = 2;
            this.LoginBtn.Text = "LoginBtn";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(335, 95);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(75, 23);
            this.RegisterBtn.TabIndex = 3;
            this.RegisterBtn.Text = "RegisterBtn";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(2, 232);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(306, 149);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // LabelBearer
            // 
            this.LabelBearer.AutoSize = true;
            this.LabelBearer.Location = new System.Drawing.Point(118, 48);
            this.LabelBearer.Name = "LabelBearer";
            this.LabelBearer.Size = new System.Drawing.Size(38, 13);
            this.LabelBearer.TabIndex = 5;
            this.LabelBearer.Text = "Token";
            this.LabelBearer.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(217, 45);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "user";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(217, 72);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 7;
            this.textBox4.Text = "123456";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(217, 98);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 8;
            this.textBox5.Text = "123456";
            // 
            // SiteUrlTb
            // 
            this.SiteUrlTb.Location = new System.Drawing.Point(54, 12);
            this.SiteUrlTb.Name = "SiteUrlTb";
            this.SiteUrlTb.Size = new System.Drawing.Size(398, 20);
            this.SiteUrlTb.TabIndex = 9;
            this.SiteUrlTb.Text = "http://localhost:45457/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "SiteUrl";
            // 
            // AddEvent
            // 
            this.AddEvent.Location = new System.Drawing.Point(698, 166);
            this.AddEvent.Name = "AddEvent";
            this.AddEvent.Size = new System.Drawing.Size(75, 23);
            this.AddEvent.TabIndex = 11;
            this.AddEvent.Text = "AddEvent";
            this.AddEvent.UseVisualStyleBackColor = true;
            this.AddEvent.Click += new System.EventHandler(this.AddEvent_Click);
            // 
            // GetEventsBtn
            // 
            this.GetEventsBtn.Location = new System.Drawing.Point(886, 164);
            this.GetEventsBtn.Name = "GetEventsBtn";
            this.GetEventsBtn.Size = new System.Drawing.Size(98, 23);
            this.GetEventsBtn.TabIndex = 12;
            this.GetEventsBtn.Text = "GetEventsBtn";
            this.GetEventsBtn.UseVisualStyleBackColor = true;
            this.GetEventsBtn.Click += new System.EventHandler(this.GetEventsBtn_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(698, 11);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 13;
            this.textBox6.Text = "1232,322";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(698, 38);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 14;
            this.textBox7.Text = "45334,8";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(698, 64);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(153, 96);
            this.richTextBox2.TabIndex = 15;
            this.richTextBox2.Text = "sasdasd";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(426, 232);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(372, 149);
            this.richTextBox3.TabIndex = 16;
            this.richTextBox3.Text = "";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(12, 166);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(440, 20);
            this.textBox8.TabIndex = 17;
            // 
            // WaitLab
            // 
            this.WaitLab.AutoSize = true;
            this.WaitLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WaitLab.Location = new System.Drawing.Point(550, 78);
            this.WaitLab.Name = "WaitLab";
            this.WaitLab.Size = new System.Drawing.Size(68, 25);
            this.WaitLab.TabIndex = 18;
            this.WaitLab.Text = "WAIT";
            this.WaitLab.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 384);
            this.Controls.Add(this.WaitLab);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.GetEventsBtn);
            this.Controls.Add(this.AddEvent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SiteUrlTb);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.LabelBearer);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label LabelBearer;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox SiteUrlTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddEvent;
        private System.Windows.Forms.Button GetEventsBtn;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label WaitLab;
    }
}

