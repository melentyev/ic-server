using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Events.Infrastructure
{
    public class Messages
    {
        private static Dictionary<string, Message> dict = new Dictionary<string, Message>();
        static Messages() 
        {
            var src = new Message[] {
                new Message { Code = "INVALID_MODEL",    Description = "Неверный формат входных данных." },
                new Message { Code = "OBJECT_NOT_FOUND", Description = "Запрошенный объект не найден." },
                new Message { Code = "INVALID_DOUBLE",   Description = "Некорректный формат вещественного числа." }
                
            };
            foreach (var m in src) 
            {
                dict[m.Code] = m;
            }
        }
        public static string Get(string code, Func<string, string> f = null) 
        {
            var old = dict[code];
            if (f == null) 
            {
                return JsonConvert.SerializeObject(old);
            }
            else
            {
                var custom = new Message { Code = old.Code, Description = old.Description };
                custom.Description = f(old.Description);
                return JsonConvert.SerializeObject(custom);
            }
        }
        public static string Get(string code, object p0, Func<string, string> f = null)
        {
            return String.Format(Get(code, f), p0);
        }
        private class Message
        {
            public string Code;
            public string Description;
        }
    }
}