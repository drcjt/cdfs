namespace DataNode.Options
{
    public interface IDataNodeOptions
    {
        string NameNodeUri { get; set; }
        string BlocksPath { get; set; }
    }
}
