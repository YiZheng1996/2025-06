using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.Test
{
    internal class Parent
    {
        public string parentName { get; set; }
        public string childName { get; set; }
        public List<Child> childSteps { get; set; }
    }
}
