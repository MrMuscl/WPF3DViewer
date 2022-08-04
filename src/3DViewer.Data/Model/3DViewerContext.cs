using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;

namespace _3DViewer.Data.Model
{
    public partial class _3DViewerContext : DbContext
    {
        public _3DViewerContext()
            : base("name=DBConnectionString")
        {
        }

        public virtual DbSet<FigureType> FigureTypes { get; set; }
        public virtual DbSet<Object3D> Object3Ds { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyValue> PropertyValues { get; set; }
        public virtual DbSet<Scene> Scenes { get; set; }
    }
}