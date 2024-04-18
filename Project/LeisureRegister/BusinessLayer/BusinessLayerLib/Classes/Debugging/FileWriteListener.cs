using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerLib.Classes.Debugging
{
    class FileWriteListener : TraceListener
    {
        public override void Write(string message)
        {

            StreamWriter sw = new StreamWriter("AxtiveObjects.log", true, Encoding.Default);
            sw.Write(Enumerable.Repeat("\t", this.IndentLevel));
            sw.Write(message);
            sw.Close();

        }

        public override void WriteLine(string message)
        {
            Write(message + "\n");
        }




    }
}
