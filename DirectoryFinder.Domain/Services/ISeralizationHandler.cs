using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryFinder.Domain.Services
{
    public interface ISeralizationHandler
    {
        void StartHandeling(CancellationToken token);
    }
}
