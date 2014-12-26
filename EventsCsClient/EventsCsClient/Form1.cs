using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
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
        public string SubscribeUrl = "api/Friends/Follow";
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            var dtu = DateTime.UtcNow.ToString("r");
            var dt = DateTime.Now.ToString("r");
            var dtu1 = DateTime.UtcNow.ToString();
            var dt1 = DateTime.Now.ToString();

            var dtu2 = DateTime.UtcNow.ToString("s");
            var dt2 = DateTime.Now.ToString("s");

            var wc = new WebClient();
            var data = "grant_type=password&username=" + textBox1.Text + "&password=" + textBox2.Text;
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            try
            {
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
                var stream = new System.IO.StreamReader(we.Response.GetResponseStream());
                var line = stream.ReadToEnd();
                MsgBox1.Text = we.Message + line;
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
            try
            {
                WaitLab.Show();
                var result = await wc.UploadStringTaskAsync(SiteUrlTb.Text + RegisterUrl, body);
                WaitLab.Hide();
                MsgBox1.Text = "200\r\n" + result;
            }
            catch (WebException we)
            {
                MsgBox2.Text = we.Message;
                var stream = new System.IO.StreamReader(we.Response.GetResponseStream() );
                var line = stream.ReadToEnd();
                var aaa = 5;
            }
        }

        public string UTF8to1251(string s) 
        {
            Encoding srcEncodingFormat = Encoding.UTF8;
            Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
            byte[] originalByteString = srcEncodingFormat.GetBytes(s);
            byte[] convertedByteString = Encoding.Convert(srcEncodingFormat, dstEncodingFormat, originalByteString);
            return dstEncodingFormat.GetString(convertedByteString);
        }

        private async void GetEventsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var wc = new WebClient();
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Accept-Charset", "cp1251");
                WaitLab.Show();
                var result = await wc.DownloadStringTaskAsync(SiteUrlTb.Text + GetEventsUrl);
                //result = UTF8to1251(result);
                MsgBox1.Text = "200\r\n" + result;
                EventsListView.Items.Clear();
                EventsListView.Items.AddRange(JArray.Parse(result).Select(tok =>  {
                    var descr = (tok as JObject)["Description"].Value<string>();
                    var id = (tok as JObject)["EventId"].Value<int>();
                    return new ListViewItem { Text = descr, Tag = id };
                }).ToArray());
                WaitLab.Hide();
                //new ListViewItem { Text = "aaa" });
                    
            }
            catch (WebException we)
            {
                MsgBox2.Text = we.Message;
                var stream = new System.IO.StreamReader(we.Response.GetResponseStream());
                var line = stream.ReadToEnd();
                MsgBox1.Text += line;
            }
        }

        private async void AddEvent_Click(object sender, EventArgs e)
        {
            var token = TbToken.Text;
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Authorization", "Bearer " + token);
            string sdata;
            var photoIds = new string[0];
            if (!String.IsNullOrWhiteSpace(AddFileTB.Text))
            {
                var url = new WebClient().DownloadString(new Uri(SiteUrlTb.Text + "api/Endpoints/GetUploadUrl/AddEvent"));
                url = url.Trim(new char[] { '\"' });
                url = url.TrimStart(new char[] { '/' });
                using (var client = new HttpClient())
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    var fs = new FileStream(AddFileTB.Text, FileMode.Open);
                    form.Add(new StreamContent(fs), "file", "file.jpg");
                    var response = await client.PostAsync(SiteUrlTb.Text + url, form);
                    sdata = await response.Content.ReadAsStringAsync();
                }
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    var content = new StringContent(sdata, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(SiteUrlTb.Text 
                                            + "api/Endpoints/SaveUploadedFile/AddEvent", content);
                    sdata = await response.Content.ReadAsStringAsync();
                    photoIds = JArray.Parse(sdata).Select(tok => (tok as JObject)["Id"].Value<string>()).ToArray();
                    /*EventsListView.Items.AddRange(JArray.Parse(result).Select(tok =>
                    {
                        var descr = (tok as JObject)["Description"].Value<string>();
                        var id = (tok as JObject)["EventId"].Value<int>();
                        return new ListViewItem { Text = descr, Tag = id };
                    }).ToArray());*/
                }
            }
            try
            {
                //var descr = MsgBox2.Text.Select(c => string.Format(@"\u{0:x4}", (int)c)).Aggregate("", (a, b) => a + b);
                var descr = MsgBox2.Text;
                var dtu = DateTime.UtcNow.ToString("r");
                var data = JsonConvert.SerializeObject(new
                {
                    Latitude = EventAddLatitude.Text,
                    Longitude = EventAddLongitude.Text,
                    Description = descr,
                    EventDate = dtu,
                    PhotoIds = photoIds
                });
                //data = data.Replace(@"\\", @"\");
                WaitLab.Show();
                var result = await wc.UploadStringTaskAsync(SiteUrlTb.Text + AddEventUrl, data);
                WaitLab.Hide();
                MsgBox1.Text = result;
            }
            catch (WebException we)
            {
                var stream = new System.IO.StreamReader(we.Response.GetResponseStream());
                var line = stream.ReadToEnd();
                MsgBox2.Text = we.Message + line;
                WaitLab.Hide();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                WaitLab.Hide();
            }
        }

        private async void AddComment_Click(object sender, EventArgs e)
        {
            var token = TbToken.Text;
            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Authorization", "Bearer " + token);
            try
            {
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
        private async void Follow_Click(object sender, EventArgs e)
        {
            //var token = TbToken.Text;
            //var wc = new WebClient();
            //wc.Headers.Add("Content-Type", "application/json");
            //wc.Headers.Add("Authorization", "Bearer " + token);
            //try
            //{
            //    var data = JsonConvert.SerializeObject(new
            //    {
            //        SubscribedTo = friendTb.Text
            //    });
            //    WaitLab.Show();
            //    var result = await wc.UploadStringTaskAsync(SiteUrlTb + SubscribeUrl);
            //    WaitLab.Hide();
            //    MsgBox1.Text = result;
            //}
            //catch (WebException we)
            //{
            //    MsgBox2.Text = we.Message;
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message);
            //}
            var token = TbToken.Text;
            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                var data = JsonConvert.SerializeObject(new
                {
                    SubscribedTo = friendTb.Text,
                });
                WaitLab.Show();
                var st = SiteUrlTb.Text + SubscribeUrl + "/" + "{" + friendTb.Text + "}";
                var result = await wc.DownloadStringTaskAsync(SiteUrlTb.Text + SubscribeUrl + "/" + "{" + friendTb.Text + "}");
                WaitLab.Hide();
                MsgBox1.Text = result;
            }
            catch (WebException we)
            {
                MsgBox2.Text = we.Message;
                WaitLab.Hide();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                WaitLab.Hide();
            }
            //var wc = new WebClient();
            //wc.Headers.Add("Content-Type", "application/json");
            //WaitLab.Show();
            //var result = await wc.DownloadStringTaskAsync(SiteUrlTb.Text + SubscribeUrl);
            //WaitLab.Hide();
            //MsgBox1.Text = "200\r\n" + result;
        }

        private void SelectAddFile_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            DialogResult res = d.ShowDialog();
            AddFileTB.Text = d.FileName;
        }
    }
}
