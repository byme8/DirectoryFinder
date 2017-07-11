using System.Threading;

namespace DirectoryFinder.Domain.Services
{
    public interface ISeralizationHandler
    {
        void StartHandeling(CancellationToken token);
    }
}