using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Domain.Providers
{
    public interface ISavePathProvider
    {
        string GetPath();
    }
}
