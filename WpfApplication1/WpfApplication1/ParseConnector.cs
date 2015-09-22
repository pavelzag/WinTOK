using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace WinTOK
{
    class ParseConnector
    {
        public static List<string> ParseCall(string sGroupName = "")
        {
            //System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/TOK/CSHgwDuhg8?");
            System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/functions/getRandomTOK");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["X-Parse-Application-Id"] = Variables.APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = Variables.API_KEY;
            string postData = "{\"group\":\"" + sGroupName + "\"}";
            byte[] data2 = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = data2.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data2, 0, data2.Length);
            requestStream.Close();
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
            string status = myHttpWebResponse.StatusCode.ToString();
            var response = (HttpWebResponse)request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string json = JObject.Parse(rawJson).ToString();  //Turns your raw string into a key value lookup
            var data = JsonSerializer.DeserializeData<RootObject>(json);
            var url = data.result.audio_file.url;
            var location = data.result.location;
            var group = data.result.group;
            var objectId = data.result.objectId;

            List<string> ParseData = new List<string>();

            ParseData.Add(url);
            ParseData.Add(location);
            ParseData.Add(group);
            ParseData.Add(objectId);
            return ParseData;
        }

        public static void DefaultConnector(string sParseMethod = "getRandomTOK", string sPostData = "")
        {
            System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/functions/" + sParseMethod);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["X-Parse-Application-Id"] = Variables.APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = Variables.API_KEY;
            //string postData = "{\"group\":\"" + sGroupName + "\", \"objectId\":\"" + sObjectID + "\"   }";
            byte[] data2 = Encoding.ASCII.GetBytes(sPostData);
            request.ContentLength = data2.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data2, 0, data2.Length);
            requestStream.Close();
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
            string status = myHttpWebResponse.StatusCode.ToString();
        }

        public class AudioFile
        {
            public string __type { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }
        //ssssasdasd
        public class Result
        {
            public string __type { get; set; }
            public AudioFile audio_file { get; set; }
            public string className { get; set; }
            public string createdAt { get; set; }
            public string group { get; set; }
            public string location { get; set; }
            public string objectId { get; set; }
            public string updatedAt { get; set; }
        }

        public class RootObject
        {
            public Result result { get; set; }
        }

        public class JsonSerializer
        {
            public static T DeserializeData<T>(string jsonData)
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
                }
                catch (Exception)
                {
                    //log exception if required
                    return default(T);
                }
            }
        }
    }
}
