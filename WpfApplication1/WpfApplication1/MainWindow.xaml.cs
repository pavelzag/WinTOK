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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string sURL;
        public static string sLocation;
        public static string sGroupName;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void TOKButton(object sender, RoutedEventArgs e)
        {
            sGroupName = GroupName.Text;
            if (sGroupName == "Enter Group Name..")
                sGroupName = "";
            List<string> ParseData = ParseConnector.ParseCall(sGroupName);
            sURL = ParseData[0];
            sLocation = ParseData[1];
            sGroupName = ParseData[2];
            if (sGroupName == null)
                sGroupName = "Group Zero";
            LocationBlock.Text = "The TOK is coming from: " + sLocation;
            GroupBlock.Text = "The Group is: " + sGroupName;
            MediaContent.Source = new Uri(sURL, UriKind.Absolute);
            MediaContent.Play();
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


        //private void Insert_the_group_name_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
