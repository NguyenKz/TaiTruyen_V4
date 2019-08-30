using Grpc.Core;
using System.Runtime.Serialization;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaiTruyen_V4
{
    public class Lib
    {

        public static Boolean WriteListHostToFile(ListHost listHost,String filePath) {

            if (listHost == null)
            {
                return false;
            }
            if (listHost.tag == null)
            {
                return false;
            }

            String outPut=JsonConvert.SerializeObject(listHost, Formatting.Indented).ToString();

            if (outPut.Length<=0)
            {
                return false;
            }
            if (!ValidateJSON(outPut))
            {
                return false;
            }

            Console.WriteLine(outPut);

            WriteFile(filePath, outPut);




            return true;
            
        }
        /// <summary>
        /// add hostTag to listHost
        /// </summary>
        /// <param name="listHost">listHost</param>
        /// <param name="hostTag">hostTag</param>
        /// <param name="Update">true= replace hostTag in listHost when hostTag exits in listhost</param>
        /// <returns>false= add hostTag to listHost
        /// true = Update hostTag in listHost (Update=true)
        /// </returns>
        public static bool UpdateHostTag(ListHost listHost,HostTag hostTag,bool Update) 
        {
            if (Lib.FindHostTagWithUrl(hostTag.Host, listHost) ==null)
            {
                listHost.tag.Add(hostTag);
                return false;
            }
            if (Update)
            {
                for (int i = 0; i < listHost.tag.Count; i++)
                {
                    if (listHost.tag[i].Host.IndexOf(hostTag.Host) >= 0)
                    {
                        listHost.tag[i] = hostTag;
                        break;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// find hostTag whit url
        /// </summary>
        /// <param name="url">url-ex:https://wikidich.com/truyen/xxxx  </param>
        /// <param name="listhost"></param>
        /// <returns> HostTag or null </returns>
        public static HostTag FindHostTagWithUrl(String url,ListHost listhost)
        {

            if (listhost == null)
            {
                return null;
            }
            if (listhost.tag == null)
            {
                return null;
            }
            foreach (var ee in listhost.tag)
            {
                if (ee.Host != null)
                {
                    if (url.IndexOf(ee.Host) >= 0)
                    {
                        return ee;
                    }
                }
            }
            return null;
        }

        public static String DecoderString(String str)
        {
            str=str.Replace("&Agrave;","À");
            str=str.Replace("&Aacute;","Á");
            str=str.Replace("&Acirc;","Â");
            str=str.Replace("&Atilde;","Ã");
            str=str.Replace("&Auml;","Ä");
            str=str.Replace("&Aring;","Å");
            str=str.Replace("&agrave;","à");
            str=str.Replace("&aacute;","á");
            str=str.Replace("&acirc;","â");
            str=str.Replace("&atilde;","ã");
            str=str.Replace("&auml;","ä");
            str=str.Replace("&aring;","å");
            str=str.Replace("&AElig;","Æ");
            str=str.Replace("&aelig;","æ");
            str=str.Replace("&szlig;","ß");
            str=str.Replace("&Ccedil;","Ç");
            str=str.Replace("&ccedil;","ç");
            str=str.Replace("&Egrave;","È");
            str=str.Replace("&Eacute;","É");
            str=str.Replace("&Ecirc;","Ê");
            str=str.Replace("&Euml;","Ë");
            str=str.Replace("&egrave;","è");
            str=str.Replace("&eacute;","é");
            str=str.Replace("&ecirc;","ê");
            str=str.Replace("&euml;","ë");
            str=str.Replace("&#131;","ƒ");
            str=str.Replace("&Igrave;","Ì");
            str=str.Replace("&Iacute;","Í");
            str=str.Replace("&Icirc;","Î");
            str=str.Replace("&Iuml;","Ï");
            str=str.Replace("&igrave;","ì");
            str=str.Replace("&iacute;","í");
            str=str.Replace("&icirc;","î");
            str=str.Replace("&iuml;","ï");
            str=str.Replace("&Ntilde;","Ñ");
            str=str.Replace("&ntilde;","ñ");
            str=str.Replace("&Ograve;","Ò");
            str=str.Replace("&Oacute;","Ó");
            str=str.Replace("&Ocirc;","Ô");
            str=str.Replace("&Otilde;","Õ");
            str=str.Replace("&Ouml;","Ö");
            str=str.Replace("&ograve;","ò");
            str=str.Replace("&oacute;","ó");
            str=str.Replace("&ocirc;","ô");
            str=str.Replace("&otilde;","õ");
            str=str.Replace("&ouml;","ö");
            str=str.Replace("&Oslash;","Ø");
            str=str.Replace("&oslash;","ø");
            str=str.Replace("&#140;","Œ");
            str=str.Replace("&#156;","œ");
            str=str.Replace("&#138;","Š");
            str=str.Replace("&#154;","š");
            str=str.Replace("&Ugrave;","Ù");
            str=str.Replace("&Uacute;","Ú");
            str=str.Replace("&Ucirc;","Û");
            str=str.Replace("&Uuml;","Ü");
            str=str.Replace("&ugrave;","ù");
            str=str.Replace("&uacute;","ú");
            str=str.Replace("&ucirc;","û");
            str=str.Replace("&uuml;","ü");
            str=str.Replace("&#181;","µ");
            str=str.Replace("&#215;","×");
            str=str.Replace("&Yacute;","Ý");
            str=str.Replace("&#159;","Ÿ");
            str=str.Replace("&yacute;","ý");
            str=str.Replace("&yuml;","ÿ");
            str=str.Replace("&#176;","°");
            str=str.Replace("&#134;","†");
            str=str.Replace("&#135;","‡");
            str=str.Replace("&lt;","<");
            str=str.Replace("&gt;",">");
            str=str.Replace("&#177;","±");
            str=str.Replace("&#171;","«");
            str=str.Replace("&#187;","»");
            str=str.Replace("&#191;","¿");
            str=str.Replace("&#161;","¡");
            str=str.Replace("&#183;","·");
            str=str.Replace("&#149;","•");
            str=str.Replace("&#153;","™");
            str=str.Replace("&copy;","©");
            str=str.Replace("&reg;","®");
            str=str.Replace("&#167;","§");
            str=str.Replace("&#182;","¶");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&nbsp;", " ");
            return str;
        }
        /// <summary>
        /// remove all tag in htlm
        /// </summary>
        /// <param name="chapContent">ex "<br>afdaads" =>"afdaads"  "</param>
        /// <returns>string</returns>
        public static String ProcessChapContent(String chapContent)
        {
            var index = CheckTag(chapContent);
            while (index>=0)
            {
                index = CheckTag(chapContent);
                if (index < 0)
                {
                    break;
                }
                string tag = "";
                for (int i = index; i < chapContent.Length; i++)
                {
                    tag += chapContent[i];
                    if (chapContent[i] == '>')
                    {
                        break;
                    }
                }
                Console.WriteLine(tag+"   "+chapContent.Length);
                chapContent = chapContent.Replace(tag,"{_*-+_}");
            }
            chapContent = chapContent.Replace("{_*-+_}","</p>"+Environment.NewLine+"<p>");
            return chapContent;
        }
        /// <summary>
        /// check string have tag html
        /// </summary>
        /// <param name="chapContent"> "<p> haha</p>" =>0</param>
        /// <returns>-1: No tag</returns>
        public static int CheckTag(String chapContent)
        {
            string[] list = { "<!--", "<!DOCTYPE", "<a", "<abbr", "<acronym", "<address", "<applet", "<area", "<article", "<aside", "<audio", "<b", "<base", "<basefont", "<bdo", "<big", "<blockquote", "<body", "<br>", "<button", "<canvas", "<caption", "<center", "<cite", "<code", "<col", "<colgroup", "<datalist", "<dd", "<del", "<dfn", "<div", "<dl", "<dt", "<em", "<embed", "<fieldset", "<figcaption", "<figure", "<font", "<footer", "<form", "<frame", "<frameset", "<head", "<header", "<h1>", "<h2>", "<h3>", "<h4>", "<h5>", "<h6>", "<hr", "<html", "<i", "<iframe", "<img", "<input", "<ins", "<kbd", "<label", "<legend", "<li", "<link", "<main", "<map", "<mark", "<meta", "<meter", "<nav", "<noscript", "<object", "<ol", "<optgroup", "<option", "<p", "<param", "<pre", "<progress", "<q", "<s", "<samp", "<script", "<section", "<select", "<small", "<source", "<span", "<strike", "<strong", "<style", "<sub", "<table", "<tbody", "<td", "<textarea", "<tfoot", "<th", "<thead", "<time", "<title", "<tr", "<u", "<ul", "<var", "<video", "<wbr", "</" };
            for (int i = 0; i < list.Count(); i++) {
                if (chapContent.IndexOf(list[i]) >= 0)
                {
                    return chapContent.IndexOf(list[i]);
                }
            } 
            return -1;

        }
        /// <summary>
        /// http://wikidich.com/ajkfdhakjs  => http://wikidich.com
        /// http::adfadfas  => "null"
        /// </summary>
        /// <param name="url">ex: http://wikidich.com/ajkfdhakjs </param>
        /// <returns>ex:http://wikidich.com or "null"</returns>
        public static String GetHostInUrl(String url)
        {
            if (url == null) return "null";
            if (!Lib.CheckUrl(url))
            {
                
                return "null";
            }
            int index = 0;
            string httpStr = "https://";
            if (url.IndexOf(httpStr) < 0){
                httpStr = "http://";
            }
            String host = "";
            if (url.IndexOf("://") >= 0)
            {

                for (int i = url.IndexOf("://")+3 ; i < url.Length; i++)
                {
                    if (url[i] == '/')
                    {

                        index = i;
                        break;
                    }
                    else
                    {
                        host += url[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < url.Length; i++)
                {
                    if (url[i] == '/')
                    {
                        index = i;
                        break;
                    }
                    else
                    {
                        host += url[i];
                    }
                }
            }

            return httpStr+host;
        }
        /// <summary>
        /// Check url is true format
        /// return true is true format
        /// return flase is flase format
        /// </summary>
        /// <param name="url"> url to connect</param>
        /// <returns>bool</returns>
        public static bool CheckUrl(String url)
        {
            //Console.WriteLine("Check format url.");
            Uri uriResult;
            bool tryCreateResult = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
            if (tryCreateResult == true && uriResult != null)
            {
                //Console.WriteLine("     url is true format.");
                return true;
            }
            //Console.WriteLine("     url is false format.");
            return false;

        }
        /// <summary>
        /// Load Json file To object ListHost
        /// </summary>
        /// <param name="path">FilePath of json File</param>
        /// <returns>ListHost</returns>
        public static ListHost LoadJsonFileToListHost(String path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("file path not exits");
                return null;
            }
            String StrJson = Lib.ReadFile(path);
            //Console.WriteLine(j);
            //// Needs a reference to "System.Runtime.Serialization"
            ListHost rootObject = new JavaScriptSerializer().Deserialize<ListHost>(StrJson);        
            
            return rootObject;

        }

        /// <summary>
        /// Read file in path
        /// </summary>
        /// <param name="path">ex: c:\hello.txt</param>
        /// <returns>string</returns>
        public static String ReadFile(String path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            string text;
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }

            return text;

        }
        /// <summary>
        /// check Json string is valid
        /// </summary>
        /// <param name="s">json string</param>
        /// <returns>true if valid and false if not valid</returns>
        public static bool ValidateJSON(String s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (JsonReaderException ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }
   
        /// <summary>
        /// write text to file with FilePath
        /// </summary>
        /// <param name="FilePath">ex: C:\abc.json</param>
        /// <param name="Text">content ex: hello</param>
        public static void WriteFile(String FilePath,String Text) {
            try
            {
                StreamWriter sw = new StreamWriter(FilePath);
                sw.WriteLine(Text);
                sw.Close();
            }
            catch (Exception ee)
            {
                Console.WriteLine("Exception: " + ee.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

    }
}
