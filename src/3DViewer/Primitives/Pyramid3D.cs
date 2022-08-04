using _3DViewer.Data.Model;
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
        private double Side { get; set; }
        private double Height { get; set; }

        public Pyramid3D(Point3D insPt, double side, double height, _3DViewerContext context) : base(insPt, context)
        {
            Side = side;
            Height = height;
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

            //mesh.Normals = normalCollection;

            double o = Side / 2;
            double h = Height;
            var points = new Point3DCollection();
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

        public override void SaveToScene(Scene scene)
        {
            var obj = new Object3D();

            var props = new List<PropertyValue>();
            var propX = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "X").SingleOrDefault(),
                Value = this.X.ToString()
            };
            props.Add(propX);

            var propY = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Y").SingleOrDefault(),
                Value = this.Y.ToString()
            };
            props.Add(propY);

            var propZ = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Z").SingleOrDefault(),
                Value = this.Z.ToString()
            };
            props.Add(propZ);

            var propSide = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Side").SingleOrDefault(),
                Value = this.Side.ToString()
            };
            props.Add(propSide);

            var propHeight = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Height").SingleOrDefault(),
                Value = this.Height.ToString()
            };
            props.Add(propHeight);

            obj.FigureType = _dbContext.FigureTypes.Where(t => t.Name == "Pyramid").SingleOrDefault();
            foreach (var p in props)
                obj.PropertyValues.Add(p);

            scene.Object3Ds.Add(obj);
        }
    }
}
