using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3DViewer.Primitives
{
    public enum FigureType
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

        public Figure3D(Point3D insPoint)
        {
            X = insPoint.X; Y = insPoint.Y; Z = insPoint.Z;
        }

        public abstract MeshGeometry3D GetMesh();

    }
}
