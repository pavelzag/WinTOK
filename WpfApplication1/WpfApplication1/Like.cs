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
            string sLikedObjects = ReadLikedText();
            if (!CheckIfLiked(sObjectID, sLikedObjects))
                AddToLiked(sObjectID);
        }

        public static bool CheckIfLiked(string sObjectID, string sLikedObjects)
        {
            bool bIsContains = true;
            if (sLikedObjects.Contains(sObjectID))
                return bIsContains;
            else
                return false;
        }

        public static void AddToLiked(string sObjectID = "2PGuHAYnMA")
        {
            List<string> data = new List<string>();
            data.Add(sObjectID);
            string json = JsonConvert.SerializeObject(data.ToArray()) + Environment.NewLine;
            string path = "1.txt";
            string fullPath = System.IO.Path.GetFullPath(path);
            System.IO.File.AppendAllText(fullPath, json);
        }

        public static string ReadLikedText()
        {
            string path = "1.txt";
            string fullPath = System.IO.Path.GetFullPath(path);
            return File.ReadAllText(fullPath);

        }

        public class LikedTOKS
        {
            public string objectID { get; set; }
        }



    }
}
