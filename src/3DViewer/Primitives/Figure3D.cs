using _3DViewer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3DViewer.Primitives
{
    public enum FigureTypeEnum
    {
        Куб,
        Пирамида,
        Конус,
        Цилиндр,
        Сфера
    }

    public abstract class Figure3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        protected _3DViewerContext _dbContext;

        public Figure3D(Point3D insPoint, _3DViewerContext dbContext)
        {
            X = insPoint.X; Y = insPoint.Y; Z = insPoint.Z;
            _dbContext = dbContext;
        }

        public abstract MeshGeometry3D GetMesh();

        public abstract void SaveToScene(Scene scene);

    }
}
