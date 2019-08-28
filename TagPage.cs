using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiTruyen_V4
{
    public class TagPage
    {
        private string host;
        private Int16[] type;
        private string[] name;
        private Int16[] typeToGet;
        private Int16[] index;
        private Int16[] attTypeToCompare;
        private string[] strCompare;
        private Int16[] attTypeToGet;
        public TagPage()
        {
            type = new Int16[5];
            typeToGet=new Int16[5];
            index=new Int16[5];
            attTypeToCompare=new Int16[5];
            strCompare=new string[5];
            attTypeToGet=new Int16[5];
        }
        public string Host { get => host; set => host = value; }
        public short[] Type { get => type; set => type = value; }
        public string[] Name { get => name; set => name = value; }
        public short[] TypeToGet { get => typeToGet; set => typeToGet = value; }
        public short[] Index { get => index; set => index = value; }
        public short[] AttTypeToCompare { get => attTypeToCompare; set => attTypeToCompare = value; }
        public string[] StrCompare { get => strCompare; set => strCompare = value; }
        public short[] AttTypeToGet { get => attTypeToGet; set => attTypeToGet = value; }
        //private int n1;
        //private int n2;

        //public int N1 { get => n1; set => n1 = value; }
        //public int N2 { get => n2; set => n2 = value; }
    }

    public class Tag
    {
        public List<TagPage> tag { get; set; }
    }
}
