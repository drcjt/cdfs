namespace DFSClient.Options
{
    public interface IOptionParser
    {
        CommonSubOptions ParseOptions(string[] args);
    }
}
