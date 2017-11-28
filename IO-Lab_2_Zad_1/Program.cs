using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace IO_Lab_2_Zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "file.txt";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            

            AutoResetEvent are = new AutoResetEvent(false); 
            fs.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadAndEndCallback), new object[] { fs, buffer, are });
            are.WaitOne();

        }

        static void ReadAndEndCallback(IAsyncResult asyncresult)
        {
            object[] input = (object[])asyncresult.AsyncState;
            FileStream fs = input[0] as FileStream;
            byte[] buffer = input[1] as byte[];
            AutoResetEvent are = input[2] as AutoResetEvent;                     
            string result = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            fs.Close();
            Console.WriteLine(result);
            are.Set(); 

        }
    }
}
