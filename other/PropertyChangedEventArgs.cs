using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.other
{
   public class PropertyChangedEventArgs
    {
        private string s;
       public PropertyChangedEventArgs(string a)
        {
            this.s = a;
        }
        public string getS()
        {
            return this.s;
        }
     
    }
}
