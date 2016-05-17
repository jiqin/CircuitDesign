using System;
using System.Collections.Generic;
using System.Text;

namespace matrix_reload
{
    public class ComponentStruct
    {
        public string Name;
        public int NodeNumber;
        public string[] Node = new string[10];
        public int[,] Connection = new int[10, 10];
    }

    public class SwtichStruct
    {
        public string Name;
        public int Position;
    }

    public class PowerStruct
    {
        public string Name;
        public int Position;
    }

    public class GroundStruct
    {
        public string Name;
        public int Position;
    }

    public class RLoadStruct
    {
        public string Name;
        public int Position;
    }

    public class ControlNode
    {
        public string Name;
        public int Position;
    }

    public class BeControlledNode
    {
        public string Name;
        public int Position;
    }
}
