using NameNode.Services;

namespace NameNode.Status
{
    public class DataNodesStatus : IDataNodesStatus
    {
        private readonly IDataNodeRepository _dataNodeRepository;

        public DataNodesStatus(IDataNodeRepository dataNodeRepository)
        {
            _dataNodeRepository = dataNodeRepository;
        }

        int IDataNodesStatus.LiveNodes => _dataNodeRepository.LiveNodes;
        int IDataNodesStatus.DeadNodes => _dataNodeRepository.DeadNodes;
    }
}
