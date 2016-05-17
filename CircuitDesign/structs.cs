namespace CircuitDesign
{
    class ComponentStruct
    {
        public string Name;
        public int Number;
        public string[] Array = new string[6];
        public int[,] Connect = new int[6, 6];
    }

    class SwtichStruct
    {
        public string Name;
        public int Num;
        public string[] Array = new string[6];
        public int[] Position = new int[6];
    }

    class CapacitorStruct
    {
        public string Name;
        public string[] Array = new string[2];
        public int[] Position = new int[2];
    }

    class PowerStruct
    {
        public string Name;
        public string OutputName;
        public int Position;
    }

    class GroundStruct
    {
        public string Name;
        public int Position;
    }
}