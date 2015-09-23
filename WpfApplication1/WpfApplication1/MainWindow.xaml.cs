using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinTOK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string sURL;
        public static string sLocation;
        public static string sGroupName;
        public static string sObjectID;

        public MainWindow()
        {
            InitializeComponent();
            EmptyHeart.Visibility = Visibility.Hidden;
        }

        //private void TOKButton(object sender, RoutedEventArgs e)
        //{
        //    FullHeart.Visibility = Visibility.Hidden;
        //    sGroupName = GroupName.Text;
        //    if (sGroupName == "Enter Group Name..")
        //        sGroupName = "";
        //    List<string> ParseData = ParseConnector.ParseCall(sGroupName);
        //    sURL = ParseData[0];
        //    sLocation = ParseData[1];
        //    sGroupName = ParseData[2];
        //    sObjectID = ParseData[3];
        //    string sLikedObjects = Like.ReadLikedText();
        //    if (!Like.CheckIfLiked(sObjectID, sLikedObjects))
        //        EmptyHeart.Visibility = Visibility.Visible;
        //    else
        //        FullHeart.Visibility = Visibility.Visible;
        //    if (sGroupName == null)
        //        sGroupName = "Group Zero";
        //    MediaContent.Source = new Uri(sURL, UriKind.Absolute);
        //    MediaContent.Play();
        //    LocationBlock.Text = "The TOK is coming from: " + sLocation;
        //    GroupBlock.Text = "The Group is: " + sGroupName;
        //    ObjectID.Text = sObjectID;
        //    EmptyHeart.Visibility = Visibility.Visible;
        //}

        private void PlayBTN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FullHeart.Visibility = Visibility.Hidden;
            sGroupName = GroupName.Text;
            if (sGroupName == "Enter Group Name..")
                sGroupName = "";
            List<string> ParseData = ParseConnector.ParseCall(sGroupName);
            sURL = ParseData[0];
            sLocation = ParseData[1];
            sGroupName = ParseData[2];
            sObjectID = ParseData[3];
            string sLikedObjects = Like.ReadLikedText();
            if (!Like.CheckIfLiked(sObjectID, sLikedObjects))
                EmptyHeart.Visibility = Visibility.Visible;
            else
                FullHeart.Visibility = Visibility.Visible;
            if (sGroupName == null)
                sGroupName = "Group Zero";
            MediaContent.Source = new Uri(sURL, UriKind.Absolute);
            MediaContent.Play();
            LocationBlock.Text = "The TOK is coming from: " + sLocation;
            GroupBlock.Text = "The Group is: " + sGroupName;
            ObjectID.Text = sObjectID;
            EmptyHeart.Visibility = Visibility.Visible;
        }

        private void ClickLikeButton(object sender, RoutedEventArgs e)
        {
            //sObjectID = "7etmIXYJmW";
            //sGroupName = "altizahen";
            if (sObjectID != null)
            {
                Like.LikeTOK(sObjectID, sGroupName);
                ObjectID.Text = sObjectID;
            }
        }

        private void SelectGroup(object sender, RoutedEventArgs e)
        {
            sGroupName = GroupName.Text;
        }

        public void GroupNameBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= GroupNameBoxGotFocus;
        }

        private void GroupName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickLikeButton(sender, e);
            //EmptyHeart.   ility = Visibility.Hidden;
            if(sObjectID != null)
            FullHeart.Visibility = Visibility.Visible;
        }

    }
}
