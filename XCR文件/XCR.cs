using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace XCR文件
{
    class XCR
    {
        private static Dictionary<byte, byte> encryptMap = new Dictionary<byte, byte>();
        private static Dictionary<byte, byte> decryptMap = new Dictionary<byte, byte>();
        private static int _init = LoadCryptoMaps();
        public static byte[] fileData;
        public static byte[] fileData2;
        public static List<XCRFile> xCRFiles = new List<XCRFile>();

        public static void saveXCR(string path)
        {
            string filename = "in.xcr";
            if (!Directory.Exists(path)) return;
            if (File.Exists(filename)) File.Delete(filename);
            string[] files = Directory.GetFiles(path);
            //File.WriteAllBytes(filename,);
            //打包文件数量
            //int fileN = files.Length;
            int offset = 28 + files.Length * 532;

            List<XCRFile> xfs = new List<XCRFile>();

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                XCRFile xf = new XCRFile();
                xf.FileName = fileInfo.Name;
                xf.Size = fileInfo.Length;
                xf.data = File.ReadAllBytes(file);
                xf.dataEncrypt = crypt("encrypt", xf.data);
                xf.Checksum = CalculateFourByteXorChecksum(xf.dataEncrypt);
                xf.Offset = offset;
                offset += (int)xf.Size;
                xfs.Add(xf);
            }
            //开始写入
            // 使用 BinaryWriter 写入文件（追加模式）
            using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(string2byte("xcr File 1.00", 20));
                writer.Write(files.Length);
                writer.Write(offset);
                foreach (XCRFile xf in xfs)
                {
                    writer.Write(string2byte(xf.FileName, 512));
                    writer.Write(xf.Offset);
                    writer.Write(xf.Size);
                    writer.Write((int)0);
                    writer.Write(xf.Checksum);
                }
                foreach (XCRFile xf in xfs)
                {
                    writer.Write(xf.dataEncrypt);
                }
                Console.WriteLine("数据已写入文件");
            }



        }
        public static byte[] string2byte(string str,int n)
        {
            byte[] bytes = new byte[n];
            //string headstr = "xcr File 1.00";
            byte[] stringBytes = Encoding.ASCII.GetBytes(str);
            Array.Copy(stringBytes, bytes, Math.Min(stringBytes.Length, bytes.Length));
            return bytes;
        }
        public static void loadXCR(string filename)
        {
            fileData = File.ReadAllBytes(filename);
            fileData2 = crypt("decrypt", fileData);

            string str = ReadString(0, 3);
            if (str != "xcr") return;
            int fileN = ReadInt(20);
            int fileMAXSize = ReadInt(24);
            xCRFiles.Clear();
            int Fileoffset = 0;
            for (int i = 0; i < fileN; i++)
            {
                Fileoffset = i * 532;
                XCRFile xCRFile = new XCRFile();
                xCRFile.FileName = ReadString(28 + Fileoffset, 512).TrimEnd('\0'); ;
                xCRFile.Offset = ReadInt(540 + Fileoffset);
                xCRFile.Size = ReadInt(544 + Fileoffset);
                xCRFile.Checksum = ReadInt(552 + Fileoffset);
                xCRFiles.Add(xCRFile);
            }

        }
        public static string ReadStringDecrypt(XCRFile xCRFile)
        {
            if (xCRFile == null) return "";
            int offset = xCRFile.Offset;
            long length = xCRFile.Size;
            if (offset + length > fileData2.Length)
            {
                length = fileData2.Length - offset;
            }
            string result = Encoding.ASCII.GetString(fileData2, offset, (int)length).Split('\0')[0];
            return result;
        }
        public static byte[] ReadByteDecrypt(XCRFile xCRFile)
        {
            if (xCRFile == null) return new byte[1];
            int offset = xCRFile.Offset;
            long length = xCRFile.Size;
            if (offset + length > fileData2.Length)
            {
                length = fileData2.Length - offset;
            }
            byte[] result = new byte[length];
            Array.Copy(fileData2, offset, result, 0, length);
            //Encoding.ASCII.GetString(fileData2, offset, (int)length);
            return result;
        }
        public static string ReadString(int offset, int length)
        {
            if (offset + length > fileData.Length)
            {
                length = fileData.Length - offset;
            }
            string result = Encoding.ASCII.GetString(fileData, offset, length).Split('\0')[0];
            return result;
        }
        public static int ReadInt(int offset)
        {
            int result = BitConverter.ToInt32(fileData, offset);
            return result;
        }



        public static byte[] crypt(string operation, byte[] fileBytes)
        {
            int i = 0;
            if (_init == 0) _init = LoadCryptoMaps();

            byte[] processedBytes = new byte[fileBytes.Length];
            if (operation == "encrypt")
                for (i = 0; i < fileBytes.Length; i++)
                {
                    processedBytes[i] = encryptMap.ContainsKey(fileBytes[i]) ? encryptMap[fileBytes[i]] : fileBytes[i];
                }
            else if (operation == "decrypt")
                for (i = 0; i < fileBytes.Length; i++)
                {
                    processedBytes[i] = decryptMap.ContainsKey(fileBytes[i]) ? decryptMap[fileBytes[i]] : fileBytes[i];
                }
            else
            {
                throw new ArgumentException("无效的操作，请使用 'encrypt' 或 'decrypt'");
            }
            return processedBytes;
        }
        public static void cryptFile(string operation, string filename)
        {
            try
            {

                byte[] fileBytes = File.ReadAllBytes(filename);
                byte[] processedBytes = crypt(operation, fileBytes);

                File.WriteAllBytes(filename, processedBytes);
                Console.WriteLine($"操作成功完成! 结果已保存到: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
        }

        public static void BaseFile()
        {
            string filePath = "BaseFile.bin"; // 输出文件名

            try
            {
                // 创建256字节的数组，每个字节值递增
                byte[] byteArray = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    byteArray[i] = (byte)i;
                }

                // 写入文件
                File.WriteAllBytes(filePath, byteArray);

                Console.WriteLine($"成功创建文件: {filePath}");
                Console.WriteLine($"文件大小: {new FileInfo(filePath).Length} 字节");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
        }

        static int LoadCryptoMaps()
        {
            // 加载基础文件
            byte[] originalBytes = new byte[256];
            for (int i = 0; i < 256; i++) originalBytes[i] = (byte)i;

            byte[] encryptedBytes = ReadEmbeddedResource("BaseFile加密后.bin"); //File.ReadAllBytes("BaseFile加密后.bin");
            byte[] decryptedBytes = ReadEmbeddedResource("BaseFile解密后.bin"); //File.ReadAllBytes("BaseFile解密后.bin");

            // 验证文件大小
            if (encryptedBytes.Length != 256 || decryptedBytes.Length != 256)
            {
                throw new InvalidOperationException("基础文件大小必须为256字节");
            }

            // 构建加密和解密映射表
            for (int i = 0; i < 256; i++)
            {
                encryptMap[(byte)i] = encryptedBytes[i];
                decryptMap[(byte)i] = decryptedBytes[i];

            }

            Console.WriteLine("加密映射表已成功加载");
            return 1;
        }


        public static int CalculateFourByteXorChecksum(byte[] fileBytes)
        {
            try
            {
                byte[] checksum = new byte[4] { 0, 0, 0, 0 };
                for (int i = 0; i < fileBytes.Length; i++)
                { 
                    int checksumPos = i % 4;
                    checksum[checksumPos] ^= fileBytes[i];
                }
                return BitConverter.ToInt32(checksum, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("计算校验和时出错: " + ex.Message);
            }
        }
        static byte[] ReadEmbeddedResource(string filename)
        {
            // 获取当前程序集
            var assembly = Assembly.GetExecutingAssembly();

            // 构建资源名称 (默认命名空间.文件名)
            string resourceName = $"{assembly.GetName().Name}.{filename}";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"资源 '{resourceName}' 未找到.");
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}


