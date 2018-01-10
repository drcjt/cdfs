namespace NameNode.FileSystem.Interfaces
{
    public interface IFileSystemReaderWriter
    {
        void WriteFileSystem(IDirectory root);
        IDirectory ReadFileSystem();
    }
}
