using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Events.Infrastructure;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Events.Infrastructure
{
    public class GCMClient
    {
        public async Task<string> SendNotification(string[] registration_ids, object data, string collapse_key = null)
        {
            using (var client = new HttpClient()) {   
                JObject body = new JObject();
                body.Add("registration_ids", JObject.FromObject(registration_ids));
                if (data != null)
                {
                    body.Add("data", (data is string ? new JValue(data) as JToken : new JObject(data) as JToken));
                }
                var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://android.googleapis.com/gcm/send", content);

                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }
    }
}