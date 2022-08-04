using _3DViewer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3DViewer.Primitives
{
    class Cylinder3D : Figure3D
    {
        public double Radius { get; set; }
        public double Height { get; set; }
        public Cylinder3D(Point3D insPoint, double height, double radius, _3DViewerContext context) : base(insPoint, context)
        {
            Height = height;
            Radius = radius;
        }
        public override MeshGeometry3D GetMesh()
        {
            var mesh = new MeshGeometry3D();
            BuildCylinderMesh(mesh, new Point3D(X, Y, Z), new Vector3D(0, 1 + Height, 0), Radius, 50);

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

            var propHeight = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Height").SingleOrDefault(),
                Value = this.Height.ToString()
            };
            props.Add(propHeight);

            var propRadius = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Radius").SingleOrDefault(),
                Value = this.Radius.ToString()
            };
            props.Add(propRadius);

            obj.FigureType = _dbContext.FigureTypes.Where(t => t.Name == "Cylinder").SingleOrDefault();
            foreach (var p in props)
                obj.PropertyValues.Add(p);

            scene.Object3Ds.Add(obj);
        }
                
        private void BuildCylinderMesh(MeshGeometry3D mesh, Point3D end_point, Vector3D axis, double radius, int num_sides)
        {
            // Get two vectors perpendicular to the axis.
            Vector3D v1;
            if ((axis.Z < -0.01) || (axis.Z > 0.01))
                v1 = new Vector3D(axis.Z, axis.Z, -axis.X - axis.Y);
            else
                v1 = new Vector3D(-axis.Y - axis.Z, axis.X, axis.X);
            Vector3D v2 = Vector3D.CrossProduct(v1, axis);

            // Make the vectors have length radius.
            v1 *= (radius / v1.Length);
            v2 *= (radius / v2.Length);

            // Make the top end cap.
            double theta = 0;
            double dtheta = 2 * Math.PI / num_sides;
            for (int i = 0; i < num_sides; i++)
            {
                Point3D p1 = end_point +
                    Math.Cos(theta) * v1 +
                    Math.Sin(theta) * v2;
                theta += dtheta;
                Point3D p2 = end_point +
                    Math.Cos(theta) * v1 +
                    Math.Sin(theta) * v2;
                AddTriangle(mesh, end_point, p1, p2);
            }

            // Make the bottom end cap.
            Point3D end_point2 = end_point + axis;
            theta = 0;
            for (int i = 0; i < num_sides; i++)
            {
                Point3D p1 = end_point2 +
                    Math.Cos(theta) * v1 +
                    Math.Sin(theta) * v2;
                theta += dtheta;
                Point3D p2 = end_point2 +
                    Math.Cos(theta) * v1 +
                    Math.Sin(theta) * v2;
                AddTriangle(mesh, end_point2, p2, p1);
            }

            // Make the sides.
            theta = 0;
            for (int i = 0; i < num_sides; i++)
            {
                Point3D p1 = end_point +
                    Math.Cos(theta) * v1 +
                    Math.Sin(theta) * v2;
                theta += dtheta;
                Point3D p2 = end_point +
                    Math.Cos(theta) * v1 +
                    Math.Sin(theta) * v2;

                Point3D p3 = p1 + axis;
                Point3D p4 = p2 + axis;

                AddTriangle(mesh, p1, p3, p2);
                AddTriangle(mesh, p2, p3, p4);
            }
        }
        private void AddTriangle(MeshGeometry3D mesh, Point3D point1, Point3D point2, Point3D point3)
        {
            // Get the points' indices.
            int index1 = AddPoint(mesh.Positions, point1);
            int index2 = AddPoint(mesh.Positions, point2);
            int index3 = AddPoint(mesh.Positions, point3);

            // Create the triangle.
            mesh.TriangleIndices.Add(index1);
            mesh.TriangleIndices.Add(index2);
            mesh.TriangleIndices.Add(index3);
        }

        // If the point already exists, return its index.
        // Otherwise create the point and return its new index.
        private int AddPoint(Point3DCollection points, Point3D point)
        {
            // See if the point exists.
            for (int i = 0; i < points.Count; i++)
            {
                if ((point.X == points[i].X) &&
                    (point.Y == points[i].Y) &&
                    (point.Z == points[i].Z))
                    return i;
            }

            // We didn't find the point. Create it.
            points.Add(point);
            return points.Count - 1;
        }
    }
}
