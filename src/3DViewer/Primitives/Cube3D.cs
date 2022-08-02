using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3DViewer.Primitives
{
    class Cube3D : Figure3D
    {
        private double _side = 0;
        public Cube3D(Point3D insPoint, double side) : base(insPoint)
        {
            _side = side;
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
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            normalCollection.Add(new Vector3D(0, 0, 1));
            mesh.Normals = normalCollection;

            double o = _side / 2;

            var points = new Point3DCollection();
            //points.Add(new Point3D(-o + X, -o + Y, o + Z));
            //points.Add(new Point3D(o + X, -o + Y, o + Z));
            //points.Add(new Point3D(-o + X, o + Y, o + Z));
            //points.Add(new Point3D(o + X, o + Y, o + Z));
            //points.Add(new Point3D(-o + X, -o + Y, -o + Z));
            //points.Add(new Point3D(o + X, -o + Y, -o + Z));
            //points.Add(new Point3D(-o + X, o + Y, -o + Z));
            //points.Add(new Point3D(o + X, o + Y, -o + Z));

            points.Add(new Point3D(-o + X, -o + Y, 0 + Z));
            points.Add(new Point3D(o + X, -o + Y, 0 + Z));
            points.Add(new Point3D(-o + X, o + Y, 0 + Z));
            points.Add(new Point3D(o + X, o + Y, 0 + Z));
            points.Add(new Point3D(-o + X, -o + Y, 2 * o + Z));
            points.Add(new Point3D(o + X, -o + Y, 2 * o + Z));
            points.Add(new Point3D(-o + X, o + Y, 2 * o + Z));
            points.Add(new Point3D(o + X, o + Y, 2 * o + Z));

            mesh.Positions = points;
            /*
            Front
            0, 2, 1
            1, 2, 3
            Left
            0, 4, 2
            2, 4, 6
            Bottom
            0, 1, 4
            1, 5, 4
            Right
            1, 7, 5
            1, 3, 7
            Back
            4, 5, 6
            7, 6, 5
            Top
            2, 6, 3
            3, 6, 7
            */
            var triangleIdx = new Int32Collection(new int[] { 0, 2, 1, 1, 2, 3, 0, 4, 2, 2, 4, 6, 0, 1, 4, 1, 5, 4, 1, 7, 5, 1, 3, 7, 4, 5, 6, 7, 6, 5, 2, 6, 3, 3, 6, 7 });
            mesh.TriangleIndices = triangleIdx;

            return mesh;
        }
    }
}

