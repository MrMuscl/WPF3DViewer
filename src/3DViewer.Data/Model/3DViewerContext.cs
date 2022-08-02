using System;
using System.Data.Entity;
using System.Linq;

namespace _3DViewer.Data.Model
{
    public class _3DViewerContext : DbContext
    {
        public _3DViewerContext()
            : base("name=3DViewerContext")
        {
        }

        public DbSet<FigureType> FigureTypes { get; set; }

    }
}