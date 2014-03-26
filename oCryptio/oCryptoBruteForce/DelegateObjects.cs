namespace oCryptoBruteForce
{
    public class DelegateObjects
    {
        public byte[] ByteArray { get; set; }
        public string Checksum { get; set; }
    }

    public delegate void DelegateObject(DelegateObjects objects);
}
