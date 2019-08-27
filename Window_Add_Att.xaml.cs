﻿using System;
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBox_Tag.Items.Insert(Web_Document.IndexOfArray_BookName, "BookName");
            this.comboBox_Tag.Items.Insert(Web_Document.IndexOfArray_ChapName, "ChapName");
            this.comboBox_Tag.Items.Insert(Web_Document.IndexOfArray_ChapContent, "ChapContent");
            this.comboBox_Tag.Items.Insert(Web_Document.IndexOfArray_UrlNext, "UrlNext");



            this.comboBox_Type.Items.Insert(Web_Document.Type_Class,"Class");
            this.comboBox_Type.Items.Insert(Web_Document.Type_Id, "Id");
            this.comboBox_Type.Items.Insert(Web_Document.Type_Tag, "Tag");

            this.comboBox_TypeToGet.Items.Insert(Web_Document.Get_With_Index, "Index");
            this.comboBox_TypeToGet.Items.Insert(Web_Document.Get_With_Value, "Value");

            
            

            

            this.comboBox_Att_Type_Compare.Items.Insert(Web_Document.AttType_InnterText, "InnerText");
            this.comboBox_Att_Type_Compare.Items.Insert(Web_Document.AttType_InnerHtml, "InnerHtml");
            this.comboBox_Att_Type_Compare.Items.Insert(Web_Document.AttType_Src, "Src");
            this.comboBox_Att_Type_Compare.Items.Insert(Web_Document.AttType_Href, "Href");
            this.comboBox_Att_Type_Compare.Items.Insert(Web_Document.AttType_Style, "Style");

            this.comboBox_Att_Type_Get.Items.Insert(Web_Document.AttType_InnterText, "InnerText");
            this.comboBox_Att_Type_Get.Items.Insert(Web_Document.AttType_InnerHtml, "InnerHtml");
            this.comboBox_Att_Type_Get.Items.Insert(Web_Document.AttType_Src, "Src");
            this.comboBox_Att_Type_Get.Items.Insert(Web_Document.AttType_Href, "Href");
            this.comboBox_Att_Type_Get.Items.Insert(Web_Document.AttType_Style, "Style");

        }
    }
}
