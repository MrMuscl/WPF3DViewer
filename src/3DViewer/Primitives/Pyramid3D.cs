using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3DViewer.Primitives
{
    public class Pyramid3D : Figure3D
    {
        private double _side;
        private double _height;

        public Pyramid3D(Point3D insPt, double side, double height) : base(insPt)
        {
            _side = side;
            _height = height;
        }

        public override MeshGeometry3D GetMesh()
        {
            var mesh = new MeshGeometry3D();

            var normalCollection = new Vector3DCollection();

            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));

            mesh.Normals = normalCollection;

            double o = _side / 2;
            double h = _height;
            var points = new Point3DCollection();
            //points.Add(new Point3D(-o + X, -o + Y, -h + Z));
            //points.Add(new Point3D(-o + X, o + Y, -h + Z));
            //points.Add(new Point3D(o + X, o + Y, -h + Z));
            //points.Add(new Point3D(o + X, -o + Y, -h + Z));
            //points.Add(new Point3D(0 + X, 0 + Y, h + Z));

            points.Add(new Point3D(-o + X, -o + Y, 0 + Z));
            points.Add(new Point3D(-o + X, o + Y, 0 + Z));
            points.Add(new Point3D(o + X, o + Y, 0 + Z));
            points.Add(new Point3D(o + X, -o + Y, 0 + Z));
            points.Add(new Point3D(0 + X, 0 + Y, h + Z));

            mesh.Positions = points;


            /*
             0,3,1
             3,2,1
             0,1,4
             1,2,4
             2,3,4
             3,0,4
             */

            var triangleIdx = new Int32Collection(new int[] { 0, 3, 1, 3, 2, 1, 0, 1, 4, 1, 2, 4, 2, 3, 4, 3, 0, 4 });
            mesh.TriangleIndices = triangleIdx;

            return mesh;
        }
    }
}
