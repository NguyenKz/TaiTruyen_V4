using System;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
using HtmlAgilityPack;
using System.IO;
//using Nancy.Json;
using System.Runtime.Serialization.Json;

namespace TaiTruyen_V4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Lib.CheckUrl(this.TextBox_UrlCurrent.Text))
            {
                return;

            }
            
            Web_Document dc = new Web_Document("hhh");
            dc.InitBookName(Web_Document.Type_Class, "book-title", Web_Document.Get_With_Index, 0, 0," ", Web_Document.AttType_InnterText);
            dc.InitChapName(Web_Document.Type_Class, "book-title", Web_Document.Get_With_Index, 1, 0, " ", Web_Document.AttType_InnterText);
            dc.InitChapContent(Web_Document.Type_Id, "bookContentBody", Web_Document.Get_With_Index, 0, 0, " ", Web_Document.AttType_InnerHtml);
            dc.InitUrlNext(Web_Document.Type_Class, "btn-bot", Web_Document.Get_With_Value,0,Web_Document.AttType_Style , "margin-left: 0.3rem; display: inline-block; margin-right: 1rem", Web_Document.AttType_Href);

            dc.UpdateDocumentWithNewUrl(this.TextBox_UrlCurrent.Text);
            if (dc.CheckInitSite())
            {
                this.TextBox_Result.Text = dc.GetContentInDocument(Web_Document.IndexOfArray_BookName) + Environment.NewLine;
                this.TextBox_Result.AppendText(dc.GetContentInDocument(Web_Document.IndexOfArray_ChapName) + Environment.NewLine);
                this.TextBox_Result.AppendText(dc.GetContentInDocument(Web_Document.IndexOfArray_ChapContent) + Environment.NewLine);
                this.TextBox_Result.AppendText(dc.GetContentInDocument(Web_Document.IndexOfArray_UrlNext) + Environment.NewLine);
            }
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_Result.Clear();
        }

        private void Button_AddAtt_Click(object sender, RoutedEventArgs e)
        {
            Window_Add_Att subWindow = new Window_Add_Att();
            subWindow.Show();
        }

        private void Button_TestJson_Click(object sender, RoutedEventArgs e)
        {






            ListHost hh = new ListHost();



            HostTag test = new HostTag();
            test.Host = "Wikidich";
            test.Type = new Int16[] { Web_Document.Type_Class, Web_Document.Type_Class, Web_Document.Type_Id, Web_Document.Type_Class };
            test.AttStrName = new string[] { "book-title", "book-title", "bookContentBody", "btn-bot" };
            test.TypeToGet = new short[] { Web_Document.Get_With_Index, Web_Document.Get_With_Index, Web_Document.Get_With_Index, Web_Document.Get_With_Value };
            test.IndexInElement = new short[] { 0, 1, 0, 0 };
            test.AttTypeToCompare = new short[] { 0, 0, 0, Web_Document.AttType_Style };
            test.attValueStrToCompare = new string[] { " ", " ", " ", "margin-left: 0.3rem; display: inline-block; margin-right: 1rem" };
            test.AttTypeToGetStr = new short[] { Web_Document.AttType_InnterText, Web_Document.AttType_InnterText, Web_Document.AttType_InnerHtml, Web_Document.AttType_Href };
            hh.tag.Add(test);

         

            test = new HostTag();
            test.Host = "haha";
            test.Type = new Int16[] { Web_Document.Type_Class, Web_Document.Type_Class, Web_Document.Type_Id, Web_Document.Type_Class };
            test.AttStrName = new string[] { "dsafsd", "book-title", "bookContentBody", "btn-bot" };
            test.TypeToGet = new short[] { Web_Document.Get_With_Index, Web_Document.Get_With_Index, Web_Document.Get_With_Index, Web_Document.Get_With_Value };
            test.IndexInElement = new short[] { 0, 1, 0, 0 };
            test.AttTypeToCompare = new short[] { 0, 0, 0, Web_Document.AttType_Style };
            test.attValueStrToCompare = new string[] { " ", " ", " ", "margin-left: 0.3rem; display: inline-block; margin-right: 1rem" };
            test.AttTypeToGetStr = new short[] { Web_Document.AttType_InnterText, Web_Document.AttType_InnterText, Web_Document.AttType_InnerHtml, Web_Document.AttType_Href };
            hh.tag.Add(test);

            Lib.WriteListHostToFile(hh,"host.json");
            

            ListHost hhh = new ListHost();
            hhh = Lib.LoadJsonFileToListHost("host.json");


           // String ou= JsonConvert.SerializeObject(hhh, Formatting.Indented).ToString();


            foreach (var aa in hhh.tag)
            {
                this.TextBox_Result.Text += aa.Host + "  " + aa.AttStrName[0] + Environment.NewLine;

            }
        

        }
    }
}
