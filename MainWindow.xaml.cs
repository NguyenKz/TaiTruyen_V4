using System;
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
            if (!Web_Document.CheckUrl(this.TextBox_UrlCurrent.Text))
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
    }
}
