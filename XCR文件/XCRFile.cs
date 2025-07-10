using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCR文件
{
    class XCRFile
    {
        public XCRFile() { }
        public string FileName { get; set; }
        public long Size{ get; set; }
        public int Offset{ get; set; }
        public int Checksum { get; set; }
        public byte[] data { get; set; }
        public byte[] dataEncrypt { get; set; }



        public override string ToString() { return FileName; }
        public string ToALLString() 
        { 
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"FileName:{FileName}" );
            sb.AppendLine($"Offset:0x{Offset:X8}");
            sb.AppendLine($"Size:0x{Size:X8}");
            sb.AppendLine($"Checksum:0x{Checksum:X8}" );
            return sb.ToString(); 
        }
        /*
FileName: 001-2.txt
Size: 3 bytes
Reserve: 0000 h
Offset: 0444 h
Checksum: 5D5757 h
IsCrypted: True
IsEncrypted: True
Reserve: 00 h
         */
    }
}
