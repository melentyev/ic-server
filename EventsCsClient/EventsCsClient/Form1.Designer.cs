namespace EventsCsClient
{
    partial class MainForm
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
            this.MsgBox1 = new System.Windows.Forms.RichTextBox();
            this.LabelBearer = new System.Windows.Forms.Label();
            this.TbLoginUserName = new System.Windows.Forms.TextBox();
            this.TbLoginPassword = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.SiteUrlTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddEvent = new System.Windows.Forms.Button();
            this.GetEventsBtn = new System.Windows.Forms.Button();
            this.EventAddLatitude = new System.Windows.Forms.TextBox();
            this.EventAddLongitude = new System.Windows.Forms.TextBox();
            this.MsgBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.TbToken = new System.Windows.Forms.TextBox();
            this.WaitLab = new System.Windows.Forms.Label();
            this.AddComment = new System.Windows.Forms.Button();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.friendTb = new System.Windows.Forms.TextBox();
            this.Follow = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.EventsListView = new System.Windows.Forms.ListView();
            this.AddFileTB = new System.Windows.Forms.TextBox();
            this.SelectAddFile = new System.Windows.Forms.Button();
            this.UploadUserPicTB = new System.Windows.Forms.TextBox();
            this.SelectUpdateUserPic = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.UpdateUserPicBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 48);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "nbIxMaN";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 74);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "AlIeN1";
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(118, 71);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(74, 23);
            this.LoginBtn.TabIndex = 2;
            this.LoginBtn.Text = "LoginBtn";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(334, 95);
            this.RegisterBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(74, 23);
            this.RegisterBtn.TabIndex = 3;
            this.RegisterBtn.Text = "RegisterBtn";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // MsgBox1
            // 
            this.MsgBox1.Location = new System.Drawing.Point(2, 232);
            this.MsgBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MsgBox1.Name = "MsgBox1";
            this.MsgBox1.Size = new System.Drawing.Size(306, 149);
            this.MsgBox1.TabIndex = 4;
            this.MsgBox1.Text = "";
            // 
            // LabelBearer
            // 
            this.LabelBearer.AutoSize = true;
            this.LabelBearer.Location = new System.Drawing.Point(118, 48);
            this.LabelBearer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelBearer.Name = "LabelBearer";
            this.LabelBearer.Size = new System.Drawing.Size(38, 13);
            this.LabelBearer.TabIndex = 5;
            this.LabelBearer.Text = "Token";
            this.LabelBearer.Click += new System.EventHandler(this.label1_Click);
            // 
            // TbLoginUserName
            // 
            this.TbLoginUserName.Location = new System.Drawing.Point(218, 45);
            this.TbLoginUserName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TbLoginUserName.Name = "TbLoginUserName";
            this.TbLoginUserName.Size = new System.Drawing.Size(100, 20);
            this.TbLoginUserName.TabIndex = 6;
            this.TbLoginUserName.Text = "user";
            // 
            // TbLoginPassword
            // 
            this.TbLoginPassword.Location = new System.Drawing.Point(218, 72);
            this.TbLoginPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TbLoginPassword.Name = "TbLoginPassword";
            this.TbLoginPassword.Size = new System.Drawing.Size(100, 20);
            this.TbLoginPassword.TabIndex = 7;
            this.TbLoginPassword.Text = "123456";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(218, 98);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 8;
            this.textBox5.Text = "123456";
            // 
            // SiteUrlTb
            // 
            this.SiteUrlTb.Location = new System.Drawing.Point(54, 11);
            this.SiteUrlTb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SiteUrlTb.Name = "SiteUrlTb";
            this.SiteUrlTb.Size = new System.Drawing.Size(357, 20);
            this.SiteUrlTb.TabIndex = 9;
            this.SiteUrlTb.Text = "http://icreate.azurewebsites.net/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "SiteUrl";
            // 
            // AddEvent
            // 
            this.AddEvent.Location = new System.Drawing.Point(574, 200);
            this.AddEvent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.AddEvent.Name = "AddEvent";
            this.AddEvent.Size = new System.Drawing.Size(74, 23);
            this.AddEvent.TabIndex = 11;
            this.AddEvent.Text = "AddEvent";
            this.AddEvent.UseVisualStyleBackColor = true;
            this.AddEvent.Click += new System.EventHandler(this.AddEvent_Click);
            // 
            // GetEventsBtn
            // 
            this.GetEventsBtn.Location = new System.Drawing.Point(738, 16);
            this.GetEventsBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.GetEventsBtn.Name = "GetEventsBtn";
            this.GetEventsBtn.Size = new System.Drawing.Size(98, 23);
            this.GetEventsBtn.TabIndex = 12;
            this.GetEventsBtn.Text = "GetEventsBtn";
            this.GetEventsBtn.UseVisualStyleBackColor = true;
            this.GetEventsBtn.Click += new System.EventHandler(this.GetEventsBtn_Click);
            // 
            // EventAddLatitude
            // 
            this.EventAddLatitude.Location = new System.Drawing.Point(574, 19);
            this.EventAddLatitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EventAddLatitude.Name = "EventAddLatitude";
            this.EventAddLatitude.Size = new System.Drawing.Size(100, 20);
            this.EventAddLatitude.TabIndex = 13;
            this.EventAddLatitude.Text = "59.876049";
            // 
            // EventAddLongitude
            // 
            this.EventAddLongitude.Location = new System.Drawing.Point(574, 42);
            this.EventAddLongitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EventAddLongitude.Name = "EventAddLongitude";
            this.EventAddLongitude.Size = new System.Drawing.Size(100, 20);
            this.EventAddLongitude.TabIndex = 14;
            this.EventAddLongitude.Text = "29.830303";
            // 
            // MsgBox2
            // 
            this.MsgBox2.Location = new System.Drawing.Point(574, 98);
            this.MsgBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MsgBox2.Name = "MsgBox2";
            this.MsgBox2.Size = new System.Drawing.Size(154, 97);
            this.MsgBox2.TabIndex = 15;
            this.MsgBox2.Text = "sasdasd";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(414, 11);
            this.richTextBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(159, 199);
            this.richTextBox3.TabIndex = 16;
            this.richTextBox3.Text = "";
            // 
            // TbToken
            // 
            this.TbToken.Location = new System.Drawing.Point(12, 166);
            this.TbToken.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TbToken.Name = "TbToken";
            this.TbToken.Size = new System.Drawing.Size(399, 20);
            this.TbToken.TabIndex = 17;
            // 
            // WaitLab
            // 
            this.WaitLab.AutoSize = true;
            this.WaitLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WaitLab.Location = new System.Drawing.Point(116, 118);
            this.WaitLab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WaitLab.Name = "WaitLab";
            this.WaitLab.Size = new System.Drawing.Size(68, 25);
            this.WaitLab.TabIndex = 18;
            this.WaitLab.Text = "WAIT";
            this.WaitLab.Visible = false;
            // 
            // AddComment
            // 
            this.AddComment.Location = new System.Drawing.Point(738, 356);
            this.AddComment.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.AddComment.Name = "AddComment";
            this.AddComment.Size = new System.Drawing.Size(74, 23);
            this.AddComment.TabIndex = 19;
            this.AddComment.Text = "AddComment";
            this.AddComment.UseVisualStyleBackColor = true;
            this.AddComment.Click += new System.EventHandler(this.AddComment_Click);
            // 
            // richTextBox4
            // 
            this.richTextBox4.Location = new System.Drawing.Point(738, 254);
            this.richTextBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(160, 97);
            this.richTextBox4.TabIndex = 20;
            this.richTextBox4.Text = "";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(738, 228);
            this.textBox9.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 21;
            // 
            // friendTb
            // 
            this.friendTb.Location = new System.Drawing.Point(1014, 135);
            this.friendTb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.friendTb.Name = "friendTb";
            this.friendTb.Size = new System.Drawing.Size(238, 20);
            this.friendTb.TabIndex = 22;
            this.friendTb.Text = "1";
            // 
            // Follow
            // 
            this.Follow.Location = new System.Drawing.Point(1016, 161);
            this.Follow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Follow.Name = "Follow";
            this.Follow.Size = new System.Drawing.Size(74, 23);
            this.Follow.TabIndex = 23;
            this.Follow.Text = "follow";
            this.Follow.UseVisualStyleBackColor = true;
            this.Follow.Click += new System.EventHandler(this.Follow_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1096, 161);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "unfollow";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1178, 161);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "get list";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // EventsListView
            // 
            this.EventsListView.Location = new System.Drawing.Point(738, 45);
            this.EventsListView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EventsListView.Name = "EventsListView";
            this.EventsListView.Size = new System.Drawing.Size(200, 177);
            this.EventsListView.TabIndex = 26;
            this.EventsListView.UseCompatibleStateImageBehavior = false;
            // 
            // AddFileTB
            // 
            this.AddFileTB.Location = new System.Drawing.Point(574, 71);
            this.AddFileTB.Margin = new System.Windows.Forms.Padding(2);
            this.AddFileTB.Name = "AddFileTB";
            this.AddFileTB.Size = new System.Drawing.Size(70, 20);
            this.AddFileTB.TabIndex = 27;
            this.AddFileTB.Text = "C:\\Users\\user\\Downloads\\qpq8CUKjobc.jpg";
            // 
            // SelectAddFile
            // 
            this.SelectAddFile.Location = new System.Drawing.Point(646, 71);
            this.SelectAddFile.Margin = new System.Windows.Forms.Padding(2);
            this.SelectAddFile.Name = "SelectAddFile";
            this.SelectAddFile.Size = new System.Drawing.Size(26, 20);
            this.SelectAddFile.TabIndex = 28;
            this.SelectAddFile.Text = "...";
            this.SelectAddFile.UseVisualStyleBackColor = true;
            this.SelectAddFile.Click += new System.EventHandler(this.SelectAddFile_Click);
            // 
            // UploadUserPicTB
            // 
            this.UploadUserPicTB.Location = new System.Drawing.Point(310, 232);
            this.UploadUserPicTB.Margin = new System.Windows.Forms.Padding(2);
            this.UploadUserPicTB.Name = "UploadUserPicTB";
            this.UploadUserPicTB.Size = new System.Drawing.Size(122, 20);
            this.UploadUserPicTB.TabIndex = 29;
            this.UploadUserPicTB.Text = "C:\\Users\\user\\Downloads\\1368990659878.png";
            // 
            // SelectUpdateUserPic
            // 
            this.SelectUpdateUserPic.Location = new System.Drawing.Point(434, 230);
            this.SelectUpdateUserPic.Margin = new System.Windows.Forms.Padding(2);
            this.SelectUpdateUserPic.Name = "SelectUpdateUserPic";
            this.SelectUpdateUserPic.Size = new System.Drawing.Size(26, 20);
            this.SelectUpdateUserPic.TabIndex = 30;
            this.SelectUpdateUserPic.Text = "...";
            this.SelectUpdateUserPic.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(310, 252);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(10, 10);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(332, 268);
            this.webBrowser1.TabIndex = 31;
            // 
            // UpdateUserPicBtn
            // 
            this.UpdateUserPicBtn.Location = new System.Drawing.Point(470, 230);
            this.UpdateUserPicBtn.Margin = new System.Windows.Forms.Padding(2);
            this.UpdateUserPicBtn.Name = "UpdateUserPicBtn";
            this.UpdateUserPicBtn.Size = new System.Drawing.Size(91, 20);
            this.UpdateUserPicBtn.TabIndex = 32;
            this.UpdateUserPicBtn.Text = "UpdateUserPic";
            this.UpdateUserPicBtn.UseVisualStyleBackColor = true;
            this.UpdateUserPicBtn.Click += new System.EventHandler(this.UpdateUserPicBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 536);
            this.Controls.Add(this.UpdateUserPicBtn);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.SelectUpdateUserPic);
            this.Controls.Add(this.UploadUserPicTB);
            this.Controls.Add(this.SelectAddFile);
            this.Controls.Add(this.AddFileTB);
            this.Controls.Add(this.EventsListView);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Follow);
            this.Controls.Add(this.friendTb);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.AddComment);
            this.Controls.Add(this.WaitLab);
            this.Controls.Add(this.TbToken);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.MsgBox2);
            this.Controls.Add(this.EventAddLongitude);
            this.Controls.Add(this.EventAddLatitude);
            this.Controls.Add(this.GetEventsBtn);
            this.Controls.Add(this.AddEvent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SiteUrlTb);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.TbLoginPassword);
            this.Controls.Add(this.TbLoginUserName);
            this.Controls.Add(this.LabelBearer);
            this.Controls.Add(this.MsgBox1);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.RichTextBox MsgBox1;
        private System.Windows.Forms.Label LabelBearer;
        private System.Windows.Forms.TextBox TbLoginUserName;
        private System.Windows.Forms.TextBox TbLoginPassword;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox SiteUrlTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddEvent;
        private System.Windows.Forms.Button GetEventsBtn;
        private System.Windows.Forms.TextBox EventAddLatitude;
        private System.Windows.Forms.TextBox EventAddLongitude;
        private System.Windows.Forms.RichTextBox MsgBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.TextBox TbToken;
        private System.Windows.Forms.Label WaitLab;
        private System.Windows.Forms.Button AddComment;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox friendTb;
        private System.Windows.Forms.Button Follow;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView EventsListView;
        private System.Windows.Forms.TextBox AddFileTB;
        private System.Windows.Forms.Button SelectAddFile;
        private System.Windows.Forms.TextBox UploadUserPicTB;
        private System.Windows.Forms.Button SelectUpdateUserPic;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button UpdateUserPicBtn;
    }
}

