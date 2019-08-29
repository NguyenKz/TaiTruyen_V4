using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiTruyen_V4
{
    public class HostTag
    {
        private string host;
        private Int16[] type;
        private string[] name;
        private Int16[] typeToGet;
        private Int16[] index;
        private Int16[] attTypeToCompare;
        private string[] strCompare;
        private Int16[] attTypeToGet;
        public HostTag()
        {
            type = new Int16[5];
            typeToGet=new Int16[5];
            index=new Int16[5];
            attTypeToCompare=new Int16[5];
            strCompare=new string[5];
            attTypeToGet=new Int16[5];
            name = new string[5];
        }
        public string Host { get => host; set => host = value; }
        public short[] Type { get => type; set => type = value; }
        public string[] AttStrName { get => name; set => name = value; }
        public short[] TypeToGet { get => typeToGet; set => typeToGet = value; }
        public short[] IndexInElement { get => index; set => index = value; }
        public short[] AttTypeToCompare { get => attTypeToCompare; set => attTypeToCompare = value; }
        public string[] StrCompare { get => strCompare; set => strCompare = value; }
        public short[] AttTypeToGetStr { get => attTypeToGet; set => attTypeToGet = value; }
     
    }

    public class ListHost
    {
        
        public List<HostTag> tag { get; set; }

        public ListHost()
        {
            this.tag = new List<HostTag>();
        }
    }
}
