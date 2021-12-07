namespace Laba1
{
    public class SsdDisk
    {
        public int DiskSizeMb { get; set; }
        public int DataTransferRateMbps { get; set; }
        public FormFactor FormFactor { get; set; }
        public string Model { get; set; }
    }

    public class Input
    {
        public int K { get; set; }
        public decimal[] Sums { get; set; }
        public int[] Muls { get; set; }
    }

    public class Output
    {
        public decimal SumResult { get; set; }
        public int MulResult { get; set; }
        public decimal[] SortedInputs { get; set; }
    }

}