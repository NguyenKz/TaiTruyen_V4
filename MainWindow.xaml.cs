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
        private ListHost listHost;
        private Web_Document document;
        public MainWindow()
        {
            InitializeComponent();
            document = new Web_Document(" ");
            listHost = new ListHost();
            listHost = Lib.LoadJsonFileToListHost(Web_Document.filePathJson);
            document.IntListHost(listHost);
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Lib.CheckUrl(this.TextBox_UrlCurrent.Text))
            {
                return;

            }

            //Web_Document dc = new Web_Document("hhh");
            //dc.InitBookName(Web_Document.Type_Class, "book-title", Web_Document.Get_With_Index, 0, 0," ", Web_Document.AttType_InnterText);
            //dc.InitChapName(Web_Document.Type_Class, "book-title", Web_Document.Get_With_Index, 1, 0, " ", Web_Document.AttType_InnterText);
            //dc.InitChapContent(Web_Document.Type_Id, "bookContentBody", Web_Document.Get_With_Index, 0, 0, " ", Web_Document.AttType_InnerHtml);
            //dc.InitUrlNext(Web_Document.Type_Class, "btn-bot", Web_Document.Get_With_Value,0,Web_Document.AttType_Style , "margin-left: 0.3rem; display: inline-block; margin-right: 1rem", Web_Document.AttType_Href);

            //dc.UpdateDocumentWithNewUrl(this.TextBox_UrlCurrent.Text);
            //if (dc.CheckInitSite())
            //{
            //    this.TextBox_Result.Text = dc.GetContentInDocument(Web_Document.IndexOfArray_BookName) + Environment.NewLine;
            //    this.TextBox_Result.AppendText(dc.GetContentInDocument(Web_Document.IndexOfArray_ChapName) + Environment.NewLine);
            //    this.TextBox_Result.AppendText(dc.GetContentInDocument(Web_Document.IndexOfArray_ChapContent) + Environment.NewLine);
            //    this.TextBox_Result.AppendText(dc.GetContentInDocument(Web_Document.IndexOfArray_UrlNext) + Environment.NewLine);
            //}
            bool detect=document.DetectHost(this.TextBox_UrlCurrent.Text);
            if (detect)
            {

                
                document.UpdateDocumentWithNewUrl(this.TextBox_UrlCurrent.Text);
                Console.WriteLine(document.HostStr);
                if (document.CheckInitSite())
                {
                    
                    this.TextBox_Result.Text = document.GetContentInDocument(Web_Document.IndexOfArray_BookName) + Environment.NewLine;
                    this.TextBox_Result.AppendText(document.GetContentInDocument(Web_Document.IndexOfArray_ChapName) + Environment.NewLine);
                    this.TextBox_Result.AppendText(document.GetContentInDocument(Web_Document.IndexOfArray_ChapContent) + Environment.NewLine);
                    this.TextBox_Result.AppendText(document.GetContentInDocument(Web_Document.IndexOfArray_UrlNext) + Environment.NewLine);
                }
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



            

            foreach (var aa in listHost.tag)
            {
                this.TextBox_Result.Text += aa.Host + "  " + aa.AttStrName[0] + Environment.NewLine;

            }
        

        }

        private void TextBox_UrlCurrent_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
