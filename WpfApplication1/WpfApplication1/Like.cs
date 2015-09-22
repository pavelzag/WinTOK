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
    class Like
    {
        public static void LikeTOK(string sObjectID, string sGroupName)
        {
            //string sStatus = "";
            if (sGroupName == "Group Zero")
                sGroupName = "";
            string sPostData = "{\"group\":\"" + sGroupName + "\", \"objectId\":\"" + sObjectID + "\"   }";
            ParseConnector.DefaultConnector(Variables.LIKE_TOK, sPostData);
            //System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/functions/likeTOK");
            //request.ContentType = "application/json";
            //request.Method = "POST";
            //request.Headers["X-Parse-Application-Id"] = Variables.APPLICATION_ID;
            //request.Headers["X-Parse-REST-API-Key"] = Variables.API_KEY;
            //string postData = "{\"group\":\"" + sGroupName + "\", \"objectId\":\"" + sObjectID + "\"   }";
            //byte[] data2 = Encoding.ASCII.GetBytes(postData);
            //request.ContentLength = data2.Length;
            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(data2, 0, data2.Length);
            //requestStream.Close();
            //HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
            //string status = myHttpWebResponse.StatusCode.ToString();
            //return sStatus;
        }
    }
}
