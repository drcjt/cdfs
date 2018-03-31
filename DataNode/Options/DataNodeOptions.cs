namespace DataNode.Options
{
    public class DataNodeOptions : IDataNodeOptions
    {
        public string NameNodeUri { get; set; }
        public string BlocksPath { get; set; }
    }
}
