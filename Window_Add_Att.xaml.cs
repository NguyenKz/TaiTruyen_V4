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
using System.Windows.Shapes;

namespace TaiTruyen_V4
{
    /// <summary>
    /// Interaction logic for Window_Add_Att.xaml
    /// </summary>
    public partial class Window_Add_Att : Window
    {
        public Window_Add_Att()
        {
            InitializeComponent();
        }

        private void ComboBox_Copy3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private TagPage TAG;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_BookName, "BookName"));
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_ChapName, "ChapName"));
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_ChapContent, "ChapContent"));
            this.comboBox_Tag.Items.Add(new ItemComboBox(Web_Document.IndexOfArray_UrlNext, "UrlNext"));



            this.comboBox_Type.Items.Add(new ItemComboBox(Web_Document.Type_Class,"Class"));
            this.comboBox_Type.Items.Add(new ItemComboBox(Web_Document.Type_Id, "Id"));
            this.comboBox_Type.Items.Add(new ItemComboBox(Web_Document.Type_Tag, "Tag"));
                                  
            this.comboBox_TypeToGet.Items.Add(new ItemComboBox(Web_Document.Get_With_Index, "Index"));
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

            TAG = new TagPage();
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = (this.comboBox_Tag.SelectedItem as ItemComboBox).Value;
            String test = (this.comboBox_Tag.SelectedItem.ToString());
            
            TAG.Type[index]= (this.comboBox_Type.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.Type[index];

            TAG.Name[index] = this.TextBox_Name.Text;
            test += "  " + TAG.Name[index];

            TAG.TypeToGet[index]= (this.comboBox_TypeToGet.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.TypeToGet[0];

            TAG.Index[index] = Int16.Parse(this.textBox_Index.Text);
            test += "  " + TAG.Index[index];

            TAG.StrCompare[index] = this.textBox_StrToCompare.Text;
            test += "  " + TAG.StrCompare[index];

            TAG.AttTypeToCompare[index]= (this.comboBox_Att_Type_Compare.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.AttTypeToCompare[index];

            TAG.AttTypeToGet[index] = (this.comboBox_Att_Type_Get.SelectedItem as ItemComboBox).Value;
            test += "  " + TAG.AttTypeToGet[index];



            this.TextBox_Result.Text +=Environment.NewLine+ test;
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
