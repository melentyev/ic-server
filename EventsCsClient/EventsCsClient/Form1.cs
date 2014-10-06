using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventsCsClient
{
    public partial class MainForm : Form
    {
        public string TokenUrl = "Token";
        public string RegisterUrl = "api/Account/Register";
        public string GetEventsUrl = "api/Events";
        public string AddEventUrl = "api/Events";
        public string AddCommentUrl = "api/eventComments";
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            var data = "grant_type=password&username=" + textBox1.Text + "&password=" + textBox2.Text;
			wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            try {
                WaitLab.Show();
                var result = await wc.UploadStringTaskAsync(SiteUrlTb.Text + TokenUrl, data);
                WaitLab.Hide();
                JObject o1 = JObject.Parse(result);
                var token = o1["access_token"].Value<string>();
                MsgBox1.Text = result;
                TbToken.Text = token;
            }
            catch (WebException we)
            {
                MsgBox1.Text = we.Message;
            }
        }

        private async void RegisterBtn_Click(object sender, EventArgs e)
        {
            var body = JsonConvert.SerializeObject(new 
            {
                UserName = TbLoginUserName.Text,
                Password = TbLoginPassword.Text,
                ConfirmPassword = textBox5.Text
            });
            /*var request = HttpWebRequest.Create(SiteUrlTb.Text + RegisterUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            .Headers.*/
            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            try {
                WaitLab.Show();
                var result = await wc.UploadStringTaskAsync(SiteUrlTb.Text + RegisterUrl, body);
                WaitLab.Hide();
                MsgBox1.Text = "200\r\n" + result;
            }
            catch (WebException we)
            {
                MsgBox2.Text = we.Message;
            }
        }

        private async void GetEventsBtn_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            WaitLab.Show();
            var result = await wc.DownloadStringTaskAsync(SiteUrlTb.Text + GetEventsUrl);
            WaitLab.Hide();
            MsgBox1.Text = "200\r\n" + result;

        }

        private async void AddEvent_Click(object sender, EventArgs e)
        {
            var token = TbToken.Text;
            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Authorization", "Bearer " + token);
            try {
                var data = JsonConvert.SerializeObject(new
                {
                    Latitude = EventAddLatitude.Text,
                    Longitude = EventAddLongitude.Text,
                    Description = MsgBox2.Text,
                    EventDate = DateTime.Now.ToString()
                });
                WaitLab.Show();
                var result = await wc.UploadStringTaskAsync(SiteUrlTb.Text + AddEventUrl, data);
                WaitLab.Hide();
                MsgBox1.Text = result;
            }
            catch (WebException we)
            {
                MsgBox2.Text = we.Message;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void AddComment_Click(object sender, EventArgs e)
        {
            var token = TbToken.Text;
            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Authorization", "Bearer " + token);
            try {
                var data = JsonConvert.SerializeObject(new
                {
                    Text = richTextBox4.Text,
                    EntityId = textBox9.Text
                });
                WaitLab.Show();
                var result = await wc.UploadStringTaskAsync(SiteUrlTb.Text + AddCommentUrl, data);
                WaitLab.Hide();
                MsgBox1.Text = result;
            }
            catch (WebException we)
            {
                MsgBox2.Text = we.Message;
            }
            catch
            {
                
            }
            
        }
    }
}
