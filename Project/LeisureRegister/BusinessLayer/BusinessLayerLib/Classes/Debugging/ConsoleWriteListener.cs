using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerLib.Classes.Debugging
{
    class ConsoleWriteListener:TraceListener
    {
       
            public override void Write(string message)
            {

                
                Console.Write(Enumerable.Repeat("\t", this.IndentLevel));
                Console.Write(message);
                

            }

            public override void WriteLine(string message)
            {
                Write(message + "\n");
            }




        
    }
}
