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
        }
    }
}
