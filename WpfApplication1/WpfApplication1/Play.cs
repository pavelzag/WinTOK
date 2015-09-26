using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;
using System.Windows.Media;

namespace WinTOK
{
    class Play
    {
        public static void PlayTOK()
        {
            var mainWindow = new MainWindow();
            mainWindow.SetFullHeartInvisible();
            string sGroupName = mainWindow.GroupName.Text;
            if (sGroupName == "Enter Group Name..")
                sGroupName = "";
            List<string> ParseData = ParseConnector.ParseCall(sGroupName);
            string sURL = ParseData[0];
            string sLocation = ParseData[1];
            sGroupName = ParseData[2];
            string sObjectID = ParseData[3];
            string sLikedObjects = Like.ReadLikedText();
            if (!Like.CheckIfLiked(sObjectID, sLikedObjects))
                mainWindow.SetEmptyHeartVisible();
            else
                mainWindow.SetFullHeartVisible();
            if (sGroupName == null)
                sGroupName = "Group Zero";
            mainWindow.MediaContent.Source = new Uri(sURL, UriKind.Absolute);
            mainWindow.MediaContent.Play();
            mainWindow.LocationBlock.Text = "The TOK is coming from: " + sLocation;
            mainWindow.GroupBlock.Text = "The Group is: " + sGroupName;
            mainWindow.ObjectID.Text = sObjectID;
            mainWindow.SetEmptyHeartVisible();
        }
    }
}
