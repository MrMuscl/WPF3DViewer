using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public partial class _3DViewerContext : DbContext
    {
        public _3DViewerContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
