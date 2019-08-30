using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiTruyen_V4
{
    public class Web_Document
    {

        public const String StrErro= "%{E}+=.=+{r*R}{1998o}R}@";
        /// <summary>
        /// define Type
        /// </summary>
        public const Int16 Type_Class = 0;
        public const Int16 Type_Id = 1;
        public const Int16 Type_Tag = 2;
        /// <summary>
        /// Define AttType
        /// </summary>
        public const Int16 AttType_InnterText = 0;
        public const Int16 AttType_InnerHtml = 1;
        public const Int16 AttType_Src = 2;
        public const Int16 AttType_Href = 3;
        public const Int16 AttType_Style = 4;
        public const Int16 AttType_Value = 5;

        public const Int16 SizeOfArray = 6;
        /// <summary>
        /// Define IndexInElement Of Array
        /// </summary>
        public const Int16 IndexOfArray_BookName = 0;
        public const Int16 IndexOfArray_ChapName = 1;
        public const Int16 IndexOfArray_ChapContent = 2;
        public const Int16 IndexOfArray_UrlNext = 3;
        public const Int16 IndexOfArray_UrlCover = 4;
        public const Int16 IndexOfArray_Host = 5;

        /// <summary>
        /// Define type to get
        /// </summary>
        public const Int16 Get_With_Index = 0;
        public const Int16 Get_With_Value = 1;

        ///// <summary>
        ///// BookName, ChapName, ChapContent, Url...
        ///// </summary>
        //private String[] strInfo;
        /// <summary>
        /// Class name, id name, tagName, style...
        /// </summary>
        private String[] attStrName;
        /// <summary>
        /// class, id, tag....
        /// </summary>
        private Int16[] type;
        /// <summary>
        /// index of element of array element
        /// </summary>
        private Int16[] indexInElement;

        /// <summary>
        /// innerText, innerHtml...
        /// * when compare str to find element
        /// </summary>
        private Int16[] attTypeToCompare;
        /// <summary>
        /// innerText, innerHtml...
        /// *to get str
        /// </summary>
        private Int16[] attTypeToGetStr;
        /// <summary>
        /// "Chuong sau", "height: auto !important;", 1234, ....
        /// </summary>
        private String[] attValueStrToCompare;



        /// <summary>
        /// get with index or value
        /// </summary>
        private Int16[] typeToGet;

        ///
        /// temps=GetElements(document,type[x],AttStrName[x]); // get elements in document with type (id, class or tag...) and name is AttStrName
        /// 
        /// /// get with index typeToGet[x]=getWithIndex
        /// temp=temps[IndexInElement[x]];
        /// /// get with value typeToGet[x]=getWithvalue
        /// 
        /// foreach(var e in temps){
        ///     String value=GetValueOfElement(e,attTypeToCompare[x]);   // ex: GetValue(e, attTypeToCompare[IndexOfArray_BookName])          (attTypeToCompare[IndexOfArray_BookName]=InnerText) ="Chuong sau";
        ///     if (value.indexOf(attValueStrToCompare[x])>=0){    //ex: attValueStrToCompare[x] is style="height: auto !important;", value= "height: auto !important;"
        ///         temp=e;
        ///         break;
        ///     }
        ///     
        /// }
        /// 
        /// String str=
        /// 
        /// 
        /// 
        /// 
        ///
        /// clear by function clearReport;
        public String report;
        /// <summary>
        /// Flag to check error
        /// </summary>
        public bool HaveError=false;

        public const String filePathJson = "Host.json";

        public String HostStr;
        /// <summary>
        /// get String document
        /// </summary>
        /// <returns>return "" if document ==null</returns>
        public override string ToString()
        {
            if (this.document == null) return "";

            return this.document.DocumentNode.InnerHtml;
        }
        HtmlDocument document;
        public static HtmlWeb htmlWeb = new HtmlWeb()
        {
            AutoDetectEncoding = false,
            OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
        };
        /// <summary>
        /// define time get document again when get fail. 
        /// </summary>
        public const Int16 NumberCheckToStopGetData = 20;
        /// <summary>
        /// list host: wikidich, truyenfull, truyencuatui...
        /// </summary>
        private ListHost ListHost;
        public Web_Document(String Host) {  
            Init();
            //strInfo[IndexOfArray_Host] = Host;
        }
        /// <summary>
        /// add list host (ListHost) to Web Document
        /// </summary>
        /// <param name="listHost">list host in ListHost</param>
        public void IntListHost(ListHost listHost)
        {
            
            this.ListHost = listHost;
        }
        public void Init()
        {
            type = new Int16[SizeOfArray];
            attStrName = new String[SizeOfArray];
            typeToGet = new Int16[SizeOfArray];
            indexInElement = new Int16[SizeOfArray];
            attTypeToCompare = new Int16[SizeOfArray];
            attValueStrToCompare = new String[SizeOfArray];
            attTypeToGetStr = new Int16[SizeOfArray];
            //strInfo = new string[SizeOfArray];
            HaveError = false;
        }


        /// <summary>
        /// detect host with url
        /// </summary>
        /// <param name="url">url to detect</param>
        /// <returns>true found host</returns>

        public bool DetectHost(String url) {

            if (url==null)
            {
                return false;
            }
            if (this.ListHost == null)
            {
                return false;
            }
            if (this.ListHost.tag == null)
            {
                return false;
            }
            foreach (var ee in ListHost.tag)
            {
                Console.WriteLine("Host: "+ee.Host);
                if (ee.Host == null)
                {
                    return false;
                }
                if (url.IndexOf(ee.Host) >= 0)
                {
                    for (Int16 i = 0; i < 4; i++)
                    {
                        SetData(i, ee.Type[i], ee.AttStrName[i], ee.TypeToGet[i], ee.IndexInElement[i], ee.AttTypeToCompare[i], ee.attValueStrToCompare[i], ee.AttTypeToGetStr[i]);
                        this.HostStr = ee.Host;
                    }
                    return true;
                }
            }


            return false;
        }


        /// <summary>
        /// Update document with new url
        /// </summary>
        /// <param name="Url">new url</param>
        /// <returns> false if have error</returns>
        public bool UpdateDocumentWithNewUrl(String Url)
        {
            
            if (!Lib.CheckUrl(Url))
            {
                report += Environment.NewLine + " Url format not true";
                HaveError = true;
                return false;
            }
            document = GetDocumentByUrl(Url);
            document.LoadHtml(document.DocumentNode.InnerHtml.Replace("--!>","-->"));
            if (document == null)
            {
                report += Environment.NewLine + " document is null";
                HaveError = true;
                return false;
            }

            return true;
        }
        /// <summary>
        /// Check site init 
        /// </summary>
        /// <returns>false when have error</returns>
        public bool CheckInitSite() {
            String Check = GetContentInDocument(IndexOfArray_BookName);
            Console.WriteLine("BookName: "+Check);
            if (Check == null || Check.IndexOf(Web_Document.StrErro) >= 0)
            {
                report += Environment.NewLine + " att BookName not true";
                HaveError = true;
                return false;
            }
            
            Check = GetContentInDocument(IndexOfArray_ChapName);
            Console.WriteLine("ChapName: " + Check);
            if (Check==null||Check.IndexOf(Web_Document.StrErro) >= 0)
            {
                report += Environment.NewLine + " att ChapName true";
                HaveError = true;
                return false;
            }
            

            Check = GetContentInDocument(IndexOfArray_ChapContent);
            Console.WriteLine("ChapContent: " + Check);
            if (Check == null|| Check.IndexOf(Web_Document.StrErro) >= 0)
            {
                report += Environment.NewLine + " att ChapContentnot true";
                HaveError = true;
                return false;
            }
            
            Check = GetContentInDocument(IndexOfArray_UrlNext);
            Console.WriteLine("UrlNext: "+Check);
            if (Check == null || Check.IndexOf(Web_Document.StrErro) >= 0)
            {
                report += Environment.NewLine + " att UrlNext true";
                HaveError = true;
                return false;
            }
            return true;
        }





        /// <summary>
        /// Init bookname
        /// </summary>
        /// <param name="Type"> class, id, tag, src, href, style...</param>
        /// <param name="attStrName">name of class, id,tag...</param>
        /// <param name="TypeGet">get with index or value</param>
        /// <param name="indexInElements"> index of element in elements</param>
        /// <param name="attType">* to compare: href, innerText, innerHtml... </param>
        /// <param name="attValue">*to compare: "Chuong sau", 123...</param>
        /// <param name="attTypeToGetStr">* to get str: href, innerText, innerHtml...</param>
        public void InitBookName(Int16 Type,String attStrName, Int16 TypeGet,Int16 indexInElement,Int16 attType,String attValue, Int16 attTypeToGetStr) {
            Int16 index = IndexOfArray_BookName;
            SetData(index,Type, attStrName, TypeGet, indexInElement, attType, attValue, attTypeToGetStr);

        }
        /// <summary>
        /// Init ChapName
        /// </summary>
        /// <param name="Type"> class, id, tag, src, href, style...</param>
        /// <param name="attStrName">name of class, id,tag...</param>
        /// <param name="TypeGet">get with index or value</param>
        /// <param name="indexInElements"> index of element in elements</param>
        /// <param name="attType">* to compare: href, innerText, innerHtml... </param>
        /// <param name="attValue">*to compare: "Chuong sau", 123...</param>
        /// <param name="attTypeToGetStr">* to get str: href, innerText, innerHtml...</param>
        public void InitChapName(Int16 Type, String attStrName, Int16 TypeGet, Int16 indexInElements, Int16 attType, String attValue, Int16 attTypeToGetStr)
        {
            Int16 index = IndexOfArray_ChapName;
            SetData(index, Type, attStrName, TypeGet, indexInElements, attType, attValue, attTypeToGetStr);

        }
        /// <summary>
        /// Init ChapContent
        /// </summary>
        /// <param name="Type"> class, id, tag, src, href, style...</param>
        /// <param name="attStrName">name of class, id,tag...</param>
        /// <param name="TypeGet">get with index or value</param>
        /// <param name="indexInElements"> index of element in elements</param>
        /// <param name="attType">* to compare: href, innerText, innerHtml... </param>
        /// <param name="attValue">*to compare: "Chuong sau", 123...</param>
        /// <param name="attTypeToGetStr">* to get str: href, innerText, innerHtml...</param>
        public void InitChapContent(Int16 Type, String attStrName, Int16 TypeGet, Int16 indexInElements, Int16 attType, String attValue, Int16 attTypeToGetStr)
        {
            Int16 index = IndexOfArray_ChapContent;
            SetData(index, Type, attStrName, TypeGet, indexInElements, attType, attValue, attTypeToGetStr);

        }
        /// <summary>
        /// Init UrlNext
        /// </summary>
        /// <param name="Type"> class, id, tag, src, href, style...</param>
        /// <param name="attStrName">name of class, id,tag...</param>
        /// <param name="TypeGet">get with index or value</param>
        /// <param name="indexInElements"> index of element in elements</param>
        /// <param name="attType">* to compare: href, innerText, innerHtml... </param>
        /// <param name="attValue">*to compare: "Chuong sau", 123...</param>
        /// <param name="attTypeToGetStr">* to get str: href, innerText, innerHtml...</param>
        public void InitUrlNext(Int16 Type, String attStrName, Int16 TypeGet, Int16 indexInElements, Int16 attType, String attValue, Int16 attTypeToGetStr)
        {
            Int16 index = IndexOfArray_UrlNext;
            SetData(index, Type, attStrName, TypeGet, indexInElements, attType, attValue, attTypeToGetStr);

        }

        private void SetData(Int16 index,Int16 Type, String attStrName, Int16 TypeGet, Int16 indexInElement, Int16 attType, String attValue, Int16 attTypeToGetStr)
        {
            this.type[index] = Type;
            this.attStrName[index] = attStrName;
            this.typeToGet[index] = TypeGet;
            this.indexInElement[index] = indexInElement;
            this.attTypeToCompare[index] = attType;
            this.attValueStrToCompare[index] = attValue;
            this.attTypeToGetStr[index] = attTypeToGetStr;

        }

       
        /// <summary>
        /// get content
        /// </summary>
        /// <param name="Type">bookname, chapname, url ....</param>
        /// <returns>String content</returns>
        public String GetContentInDocument(Int16 Type) {
            
            Console.WriteLine("Get Content " + Type);
            Int16 index = Type;
            var temps = GetElements(document, type[index], attStrName[index]);
            
            HtmlNode temp=null;
            String strReturn="";
            if (temps == null)
            {
                report += Environment.NewLine + " att " + Type + " not true";
                HaveError = true;
                return StrErro+"  " + Type+" 1";
            }
            
            switch (typeToGet[index]) {
                case Web_Document.Get_With_Index:
                    if (index >= indexInElement.Count()|| indexInElement[index]>=temps.Count())
                    {
                        Console.WriteLine("  ** ");
                        return null;
                    }
                    temp = temps[indexInElement[index]];
                    
                    break;
                case Web_Document.Get_With_Value:
                    if (attValueStrToCompare[index].Length >= 0)
                    {
                        foreach (var e in temps)
                        {
                            String value = GetValueOfElement(e, attTypeToCompare[index]);

                            if (value.IndexOf(attValueStrToCompare[index]) >= 0)
                            {
                                temp = e;
                                break;
                            }


                        }
                    }
                    else
                    {
                        temp = temps[0];
                    }
                    break;
                default:
                    temp = null; 
                    break;
            }
            
            if (temp != null)
            {
                strReturn = GetValueOfElement(temp, attTypeToGetStr[index]);

            }
            else
            {
                report += Environment.NewLine + " att "+ Type+" not true";
                HaveError = true;
                return StrErro+ " " + Type + " 2";
            }

            switch (Type)
            {
                case IndexOfArray_UrlNext:
                    {
                        if (strReturn.IndexOf(HostStr) < 0)
                        {
                            strReturn = HostStr + strReturn;
                        }
                        break;
                    }
                case IndexOfArray_ChapContent:
                    {
                        strReturn = Lib.DecoderString(strReturn);
                        strReturn = Lib.ProcessChapContent(strReturn);
                        break;
                    }
            }
            return strReturn;
        }
        
       

        /// <summary>
        /// Get content from url
        /// return HtmlDocument
        /// return null is get false
        /// </summary>
        /// <param name="url"> ulr to get content</param>
        /// <returns> HtmlDocument</returns>
        public static HtmlDocument GetDocumentByUrl(string url)
        {
            int checkToStopGet = NumberCheckToStopGetData;
            //Console.WriteLine("Get content from url.");
            while (checkToStopGet > 0)
            {
                try
                {
                    HtmlDocument document = htmlWeb.Load(url);
                    //Console.WriteLine("     Get content from url is successful format.");
                    return document;
                }
                catch (System.NullReferenceException ex)
                {
                    //Console.WriteLine("     Get content from error, get a gain.");
                    checkToStopGet--;
                }
                catch (System.UriFormatException ex)
                {
                    //Console.WriteLine("     Get content from error, url format is false.");
                    checkToStopGet = 0;
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// Get element by class name
        /// return null when not found classname
        /// </summary>
        /// <param name="classname"> String class name</param>
        /// <param name="document"> HtmlDocument document</param>
        /// <returns>hHtmlNodeCollection</returns>
        public HtmlNodeCollection GetElementByClassName(String classname, HtmlDocument document)
        {
            try
            {
                return document.DocumentNode.SelectNodes("//*[@class='" + classname + "']");
            }
            catch (System.NullReferenceException ex)
            {
                //Console.WriteLine("get element by class name error.");
                return null;
            }
        }

        /// <summary>
        /// Get element by id, return null when not found
        /// </summary>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns> HtmlNodeCollection</returns>
        public HtmlNodeCollection GetElementByID(String id, HtmlDocument document)
        {
            try
            {
                return document.DocumentNode.SelectNodes("//*[@id='" + id + "']");
            }
            catch (System.NullReferenceException ex)
            {
                //Console.WriteLine("get element by id error.");
                return null;
            }

        }

        /// <summary>
        /// Get element by tag, return null when not found
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="document"></param>
        /// <returns> HtmlNodeCollection</returns>
        public HtmlNodeCollection GetElementByTagName(String tag, HtmlDocument document)
        {
            try
            {
                return document.DocumentNode.SelectNodes("//*/" + tag);
            }
            catch (System.NullReferenceException ex)
            {
                //Console.WriteLine("get element by tag name error.");
                return null;
            }


        }

        /// <summary>
        /// Get elements in document with type is class, id, tag... and name= name
        /// </summary>
        /// <param name="Type">class, id, tag</param>
        /// <param name="name">"abcxyz</param>
        /// <param name="document">document web</param>
        /// <returns>HtmlNodeCollection</returns>
        public HtmlNodeCollection GetElements(HtmlDocument document, int Type, String name )
        {
            //Console.Write("Get element by ");
            switch (Type)
            {
                case Web_Document.Type_Class:
                    //Console.WriteLine("Class name.");
                    return GetElementByClassName(name, document);
                case Web_Document.Type_Tag:
                    //Console.WriteLine("ListHost name.");
                    return GetElementByTagName(name, document);
                case Web_Document.Type_Id:
                    //Console.WriteLine("id.");
                    return GetElementByID(name, document);
            }
            return null;
        }
        /// <summary>
        /// return value form attbutes
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="node"></param>
        /// <returns>String</returns>
        public static String GetValueOfElement(HtmlNode node, int attributes)
        {


            switch (attributes)
            {
                case Web_Document.AttType_Href:
                    return node.Attributes["href"].Value.ToString();

                case Web_Document.AttType_Style:
                    return node.Attributes["style"].Value.ToString();

                case Web_Document.AttType_Value:
                    return node.Attributes["value"].Value.ToString();

                case Web_Document.AttType_Src:
                    return node.Attributes["src"].Value.ToString();

                case Web_Document.AttType_InnerHtml:
                    return node.InnerHtml;

                case Web_Document.AttType_InnterText:
                    return node.InnerText;

                
            }
            return "";
        }

    }
}
