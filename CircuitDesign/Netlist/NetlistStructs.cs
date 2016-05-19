using System.Collections.Generic;
using System.Data.OleDb;
using System.Collections;

namespace CircuitDesign
{
    public class BaseStruct : System.IComparable
    {
        public string Name;
        public int PositionInNodeList;

        public int CompareTo(object y)
        {
            return string.Compare(this.Name, ((BaseStruct)y).Name);
        }
    }

    public class SwtichStruct : BaseStruct
    {
    }

    public class PowerStruct : BaseStruct
    {
    }

    public class GroundStruct : BaseStruct
    {
    }

    public class RLoadStruct : BaseStruct
    {
    }

    public class ControlNode : BaseStruct
    {
    }

    public class BeControlledNode : BaseStruct
    {
    }
}