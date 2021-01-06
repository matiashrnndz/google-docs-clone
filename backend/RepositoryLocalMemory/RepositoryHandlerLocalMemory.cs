using Repository;

namespace RepositoryLocalMemory
{
    public class RepositoryHandlerLocalMemory : ISessionRepositoryHandler
    {
        public ISessionRepository GetSessionRepository()
        {
            return SessionRepositoryLocalMemory.GetInstance();
        }
    }
}
