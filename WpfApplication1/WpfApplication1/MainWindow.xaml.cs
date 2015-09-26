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
using NAudio.Wave;


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
        NAudio.Wave.WaveIn sourceStream = null;
        NAudio.Wave.DirectSoundOut waveOut = null;
        NAudio.Wave.WaveFileWriter waveWriter = null;



        public MainWindow()
        {
            InitializeComponent();
            EmptyHeart.Visibility = Visibility.Hidden;
            Record.Visibility = Visibility.Visible;
        }

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

        private void EmptyHeart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickLikeButton(sender, e);
            //EmptyHeart.   ility = Visibility.Hidden;
            if (sObjectID != null)
                FullHeart.Visibility = Visibility.Visible;
        }

        private void FullHeart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickLikeButton(sender, e);
            //EmptyHeart.   ility = Visibility.Hidden;
            if (sObjectID != null)
                EmptyHeart.Visibility = Visibility.Visible;
        }

        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null) return;
            waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            string sPath = @"C:\Users\Pavel\Desktop\delete\";
            string sRandom = "hi.wav";

            //int deviceNumber = sourceList.SelectedItems[0].Index;
            int deviceNumber = 0;

            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
            //waveWriter = new NAudio.Wave.WaveFileWriter(save.FileName, sourceStream.WaveFormat);
            waveWriter = new NAudio.Wave.WaveFileWriter(sPath + sRandom, sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
                UploadFile.UploadTOKParse();
            }
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            UploadFile.UploadTOKParse();
        }
    }
}
