using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Services
{
    public class DirectoryFinder
    {
        public Task<Data.Directory> SearchDirectory(string path)
        {
            return Task.Run(() => {


                return new Data.Directory();
            });
        }
    }
}
