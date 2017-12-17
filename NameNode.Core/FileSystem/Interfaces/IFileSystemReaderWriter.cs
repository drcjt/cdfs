namespace NameNode.Core.FileSystem.Interfaces
{
    public interface IFileSystemReaderWriter
    {
        void WriteFileSystem(IDirectory root);
        IDirectory ReadFileSystem();
    }
}
