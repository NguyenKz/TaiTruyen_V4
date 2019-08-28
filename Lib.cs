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
        /// <summary>
        /// Load Json file To object Tag
        /// </summary>
        /// <param name="path">FilePath of json File</param>
        /// <returns>Tag</returns>
        public static Tag LoadJsonFileToTag(String path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("file path not exits");
                return null;
            }
            String j = Lib.ReadFile(path);
            Console.WriteLine(j);
            //// Needs a reference to "System.Runtime.Serialization"
            Tag rootObject = new JavaScriptSerializer().Deserialize<Tag>(j);        
            
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
