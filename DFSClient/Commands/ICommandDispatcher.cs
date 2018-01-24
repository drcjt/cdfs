namespace DFSClient.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch(ICommand command);
    }
}
