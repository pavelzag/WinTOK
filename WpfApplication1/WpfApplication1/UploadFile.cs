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
    class UploadFile
    {
        public static void UploadTOKParse()
        {
            System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/files/hi.wav");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["X-Parse-Application-Id"] = Variables.APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = Variables.API_KEY;
            request.ContentType = "audio/mpeg3";
            byte[] data = System.IO.File.ReadAllBytes(@"C:\Users\Pavel\Desktop\delete\hi.wav");
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Flush();
            requestStream.Close();
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
            string status = myHttpWebResponse.StatusCode.ToString();
            var response = (HttpWebResponse)request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string json = JObject.Parse(rawJson).ToString();  //Turns your raw string into a key value lookup
            var data2 = JsonSerializer.DeserializeData<RootObject>(json);
            string sName = data2.name;
            string sURL = data2.url;
            myHttpWebResponse.Close();
            CreateParseObject(sName);
        }

        public static void CreateParseObject(string sName = "soemthing")
        {
            System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/TOK");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["X-Parse-Application-Id"] = Variables.APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = Variables.API_KEY;
            string postData = "{\"audio_file\": {\"name\": " + "\"" + sName + "\"" + ",\"__type\": \"File\"}}";
            byte[] data = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = postData.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, postData.Length);
            requestStream.Close();
            HttpWebResponse myHttpWebResponse2 = (HttpWebResponse)request.GetResponse();
            string status = myHttpWebResponse2.StatusCode.ToString();
            var response = (HttpWebResponse)request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string json = JObject.Parse(rawJson).ToString();  //Turns your raw string into a key value lookup
        }

        public class RootObject
        {
            public string name { get; set; }
            public string url { get; set; }
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
