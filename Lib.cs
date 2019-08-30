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
            string httpStr = "://";
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

            return host;
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
