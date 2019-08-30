using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace TaiTruyen_V4
{

    /// <summary>
    /// Load Form
    /// Load List host
    /// detect host with url when textbox url change
    /// load data from list host to form
    /// </summary>
    /// 


    public partial class Window_Add_Att : Window
    {

       

        ListHost listHost;
        HostTag TAG = new HostTag();
        String filePathJson = "Host.json";
        public Window_Add_Att()
        {
            InitializeComponent();
            if (File.Exists(filePathJson))
            {
                listHost = Lib.LoadJsonFileToListHost(filePathJson);
                Console.WriteLine("Data exits.");
            }
            else
            {
                listHost = new ListHost();
                Console.WriteLine("Data does not exits.");
            }
            TAG = new HostTag();
            
            this.TextBox_Result.Text = Lib.GetHostInUrl(this.textBox_Url_Test.Text);
            this.textBox_Url_Test.Text = "https://wikidich.com/truyen/than-con-xuong-nui-ky/chuong-1-nhu-the-thay-tro-XEB~xsQsRFCg_2DR";
        }
        private HostTag GetHostFromForm()
        {
            HostTag host = new HostTag();
            for (int i = 0; i < 4; i++)
            {
                host.Type[i] = (this.comboBox_Type.SelectedItem as ItemComboBox).Value;
                host.AttStrName[i] = this.TextBox_Name.Text;
                host.TypeToGet[i] = (this.comboBox_TypeToGet.SelectedItem as ItemComboBox).Value;
                host.IndexInElement[i] = Int16.Parse(this.textBox_Index.Text);
                host.AttTypeToCompare[i] = (this.comboBox_Att_Type_Compare.SelectedItem as ItemComboBox).Value;
                host.attValueStrToCompare[i] = this.textBox_StrToCompare.Text;
                host.AttTypeToGetStr[i] = (this.comboBox_Att_Type_Get.SelectedItem as ItemComboBox).Value;
            }
            string strHost = Lib.GetHostInUrl(this.textBox_Url_Test.Text);
            if (strHost == "null")
            {
                return null;
            }
            host.Host = strHost;

            return host;
        }
        private void SetContentForFrom(int Tag,HostTag TAG)
        {

            
            for (int i = 0; i < this.comboBox_Tag.Items.Count; i++)
            {
                if ((this.comboBox_Tag.Items[i] as ItemComboBox).Value == Tag)
                {
                    this.comboBox_Tag.SelectedIndex = i;
                    break;
                }
            }


            for (int i = 0; i < this.comboBox_Type.Items.Count; i++)
            {
                if ((this.comboBox_Type.Items[i] as ItemComboBox).Value == TAG.Type[Tag])
                {
                    this.comboBox_Type.SelectedIndex = i;
                    break;
                }
            }
            if (TAG.AttStrName[Tag] != null)
                this.TextBox_Name.Text = TAG.AttStrName[Tag];
            for (int i = 0; i < this.comboBox_TypeToGet.Items.Count; i++)
            {
                if ((this.comboBox_TypeToGet.Items[i] as ItemComboBox).Value == TAG.TypeToGet[Tag])
                {
                    this.comboBox_TypeToGet.SelectedIndex = i;
                    break;
                }
            }
            if (TAG.IndexInElement[Tag] != null)
                this.textBox_Index.Text = TAG.IndexInElement[Tag].ToString();

            for (int i = 0; i < this.comboBox_Att_Type_Compare.Items.Count; i++)
            {
                if ((this.comboBox_Att_Type_Compare.Items[i] as ItemComboBox).Value == TAG.AttTypeToCompare[Tag])
                {
                    this.comboBox_Att_Type_Compare.SelectedIndex = i;
                    break;
                }
            }
            if (TAG.attValueStrToCompare[Tag] != null)
                this.textBox_StrToCompare.Text = TAG.attValueStrToCompare[Tag].ToString();
            for (int i = 0; i < this.comboBox_Att_Type_Get.Items.Count; i++)
            {
                if ((this.comboBox_Att_Type_Get.Items[i] as ItemComboBox).Value == TAG.AttTypeToGetStr[Tag])
                {
                    this.comboBox_Att_Type_Get.SelectedIndex = i;
                    break;
                }
            }
        }
        private void SetHostTag(HostTag host)
        {
            TAG = host;
        }
        private void ComboBox_Copy3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TAG == null)
            {
                return;
            }
            int tag = (this.comboBox_Tag.SelectedItem as ItemComboBox).Value;
            Console.WriteLine(this.comboBox_Tag.SelectedItem.ToString());
            SetContentForFrom(tag,TAG);


        }
        
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_BookName, "BookName"));
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_ChapName, "ChapName"));
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_ChapContent, "ChapContent"));
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_UrlNext, "UrlNext"));



            this.comboBox_Type.Items.Add(new ItemComboBox(Web_Document.Type_Class,"Class"));
            this.comboBox_Type.Items.Add(new ItemComboBox(Web_Document.Type_Id, "Id"));
            this.comboBox_Type.Items.Add(new ItemComboBox(Web_Document.Type_Tag, "ListHost"));
                                  
            this.comboBox_TypeToGet.Items.Add(new ItemComboBox(Web_Document.Get_With_Index, "IndexInElement"));
            this.comboBox_TypeToGet.Items.Add(new ItemComboBox(Web_Document.Get_With_Value, "Value"));




           
            this.comboBox_Att_Type_Compare.Items.Add(new ItemComboBox(Web_Document.AttType_InnterText, "InnerText"));
            this.comboBox_Att_Type_Compare.Items.Add(new ItemComboBox(Web_Document.AttType_InnerHtml, "InnerHtml"));
            this.comboBox_Att_Type_Compare.Items.Add(new ItemComboBox(Web_Document.AttType_Src, "Src"));
            this.comboBox_Att_Type_Compare.Items.Add(new ItemComboBox(Web_Document.AttType_Href, "Href"));
            this.comboBox_Att_Type_Compare.Items.Add(new ItemComboBox(Web_Document.AttType_Style, "Style"));

            this.comboBox_Att_Type_Get.Items.Add(new ItemComboBox(Web_Document.AttType_InnterText, "InnerText"));
            this.comboBox_Att_Type_Get.Items.Add(new ItemComboBox(Web_Document.AttType_InnerHtml, "InnerHtml"));
            this.comboBox_Att_Type_Get.Items.Add(new ItemComboBox(Web_Document.AttType_Src, "Src"));
            this.comboBox_Att_Type_Get.Items.Add(new ItemComboBox(Web_Document.AttType_Href, "Href"));
            this.comboBox_Att_Type_Get.Items.Add(new ItemComboBox(Web_Document.AttType_Style, "Style"));

            foreach(var ee in this.listHost.tag)
            {
                this.comboBox_Host.Items.Add(ee);
            }
            
            
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            HostTag TAG = new HostTag();

            int index = (this.comboBox_Tag.SelectedItem as ItemComboBox).Value;
            String test = (this.comboBox_Tag.SelectedItem.ToString());
            TAG=GetHostFromForm();
            
            test += "  " + (this.comboBox_Type.SelectedItem as ItemComboBox).Value;           
            test += "  \"" + this.TextBox_Name.Text+"\"";
            test += "  " + (this.comboBox_TypeToGet.SelectedItem as ItemComboBox).Value;
            test += "  " + Int16.Parse(this.textBox_Index.Text);
            test += "  " + (this.comboBox_Att_Type_Compare.SelectedItem as ItemComboBox).Value;
            test += "  \"" + this.textBox_StrToCompare.Text+"\"";
            test += "  " + (this.comboBox_Att_Type_Get.SelectedItem as ItemComboBox).Value;



            Web_Document doc = new Web_Document("");
            doc.UpdateDocumentWithNewUrl(this.textBox_Url_Test.Text);

            //TAG.Host = "wikidich";
            //this.TextBox_Result.Text = doc.ToString();
            ListHost ls = new ListHost();
            ls.tag.Add(TAG);
            doc.IntListHost( ls);

            doc.DetectHost(this.textBox_Url_Test.Text);

            String data = doc.GetContentInDocument((Int16)index);

            this.TextBox_Result.Text = data;

            this.TextBox_Result.Text +=Environment.NewLine+ test;
        }

        private void Button_AddTag_Click(object sender, RoutedEventArgs e)
        {
            if (TAG == null)
            {
                this.TextBox_Result.AppendText(Environment.NewLine + " TAG was null.");
                return;
            }
            Console.WriteLine(this.comboBox_Tag.SelectedItem.ToString());
            String test = Environment.NewLine+" ";
            int index = (this.comboBox_Tag.SelectedItem as ItemComboBox).Value;
            test += this.comboBox_Tag.SelectedItem.ToString();

            TAG.Type[index] = (this.comboBox_Type.SelectedItem as ItemComboBox).Value;
            test +="  "+ TAG.Type[index];

            TAG.AttStrName[index] = this.TextBox_Name.Text;
            test += "  \"" + TAG.AttStrName[index]+"\"";

            TAG.TypeToGet[index] = (this.comboBox_TypeToGet.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.TypeToGet[index];

            TAG.IndexInElement[index] = Int16.Parse(this.textBox_Index.Text);
            test += "  " + TAG.IndexInElement[index];

            TAG.AttTypeToCompare[index] = (this.comboBox_Att_Type_Compare.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.AttTypeToCompare[index];

            TAG.attValueStrToCompare[index] = this.textBox_StrToCompare.Text;
            test += "  \"" + TAG.attValueStrToCompare[index]+"\"";



            TAG.AttTypeToGetStr[index] = (this.comboBox_Att_Type_Get.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.AttTypeToGetStr[index];

            this.TextBox_Result.AppendText(test);
        }

        private void Button_TestFullTag_Click(object sender, RoutedEventArgs e)
        {
            Web_Document doc = new Web_Document("");
            doc.UpdateDocumentWithNewUrl(this.textBox_Url_Test.Text);

            //TAG.Host = "wikidich";
            //this.TextBox_Result.Text = doc.ToString();
            ListHost ls = new ListHost();
            ls.tag.Add(TAG);
            doc.IntListHost(ls);

            doc.DetectHost(this.textBox_Url_Test.Text);

            String data = doc.GetContentInDocument(Web_Document.IndexOfArray_BookName)+Environment.NewLine;
            data += doc.GetContentInDocument(Web_Document.IndexOfArray_ChapName) + Environment.NewLine;
            data += doc.GetContentInDocument(Web_Document.IndexOfArray_ChapContent) + Environment.NewLine;
            data += doc.GetContentInDocument(Web_Document.IndexOfArray_UrlNext) + Environment.NewLine;
            this.TextBox_Result.Text = data;

           
        }

        private void ComboBox_Tag_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TAG == null)
            {
                return;
            }

            Console.WriteLine(this.comboBox_Tag.SelectedItem.ToString());

            int index = (this.comboBox_Tag.SelectedItem as ItemComboBox).Value;

            for (int i = 0; i < this.comboBox_Type.Items.Count; i++)
            {
                if ((this.comboBox_Type.Items[i] as ItemComboBox).Value == TAG.Type[index])
                {
                    this.comboBox_Type.SelectedIndex = i;
                    break;
                }
            }




            this.TextBox_Name.Text=TAG.AttStrName[index];


            for (int i = 0; i < this.comboBox_TypeToGet.Items.Count; i++)
            {
                if ((this.comboBox_TypeToGet.Items[i] as ItemComboBox).Value == TAG.TypeToGet[index])
                {
                    this.comboBox_TypeToGet.SelectedIndex = i;
                    break;
                }
            }



            this.textBox_Index.Text=TAG.IndexInElement[index].ToString();



            for (int i = 0; i < this.comboBox_Att_Type_Compare.Items.Count; i++)
            {
                if ((this.comboBox_Att_Type_Compare.Items[i] as ItemComboBox).Value == TAG.AttTypeToCompare[index])
                {
                    this.comboBox_Att_Type_Compare.SelectedIndex = i;
                    break;
                }
            }






            this.textBox_StrToCompare.Text="asdsfhgh "/*TAG.attValueStrToCompare[index].ToString()*/;



            for (int i = 0; i < this.comboBox_Att_Type_Get.Items.Count; i++)
            {
                if ((this.comboBox_Att_Type_Get.Items[i] as ItemComboBox).Value == TAG.AttTypeToGetStr[index])
                {
                    this.comboBox_Att_Type_Get.SelectedIndex = i;
                    break;
                }
            }
            

        }

        private void Button_AddTagToList_Click(object sender, RoutedEventArgs e)
        {
            Lib.UpdateHostTag(listHost, TAG, true);
            this.comboBox_Host.Items.Clear();
            
            foreach (var ee in this.listHost.tag)
            {
                this.comboBox_Host.Items.Add(ee);
            }


        }

        private void TextBox_Result_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Url_Test_TextChanged(object sender, TextChangedEventArgs e)
        {
            TAG = Lib.FindHostTagWithUrl(this.textBox_Url_Test.Text, listHost);
            this.TextBox_Result.AppendText(Environment.NewLine+ Lib.GetHostInUrl(this.textBox_Url_Test.Text));
        }

        private void Button_WriteJson_Click(object sender, RoutedEventArgs e)
        {
            Lib.WriteListHostToFile(listHost, filePathJson);

        }

        private void ComboBox_Host_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBox_Host.Items.Count > 0)
            {
                TAG = Lib.FindHostTagWithUrl(this.comboBox_Host.SelectedItem.ToString(), listHost);
            }
        }

        private void Button_ViewFullHostTag_Click(object sender, RoutedEventArgs e)
        {
            string data = "";
            for (int i=0;i<4;i++)
            {
                data += TAG.Type[i] + "  \"" + TAG.AttStrName[i] + "\"  " + TAG.TypeToGet[i] + "  " + TAG.IndexInElement[i] + "  " + TAG.AttTypeToCompare[i] + "  \"" + TAG.attValueStrToCompare[i] + "\"  " + TAG.AttTypeToGetStr[i] + Environment.NewLine;
            }
            this.TextBox_Result.Text = data;
        }

        private void Button_UpdateHost_Click(object sender, RoutedEventArgs e)
        {
            TAG = new HostTag();
            
            TAG.Host = Lib.GetHostInUrl(this.textBox_Url_Test.Text);
        }
    }
    class ItemComboBox {
        String str;
        Int16 value;
        public ItemComboBox( Int16 value,String str)
        {
            this.str = str;
            this.value = value;
        }

        public string Str { get => str; set => str = value; }
        public short Value { get => value; set => this.value = value; }

        public override string ToString()
        {
            return str;
        }
    }
}
