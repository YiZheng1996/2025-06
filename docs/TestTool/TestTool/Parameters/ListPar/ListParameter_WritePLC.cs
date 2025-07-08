using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTool.Parameters.ParCommunity;

namespace TestTool.Parameters.ListPar
{
    internal class ListParameter_WritePLC
    {
        public ListParameter_WritePLC()
        {
            LictPLC = new List<Parameter_WritePLC>();
        }
        public List<Parameter_WritePLC> LictPLC { get; set; }
    }
}
