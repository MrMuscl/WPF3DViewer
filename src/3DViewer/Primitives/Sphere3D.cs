using _3DViewer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3DViewer.Primitives
{
    public class Sphere3D : Figure3D
    {
        public double Radius { get; set; }
        
        public Sphere3D(Point3D insPoint, double radius, _3DViewerContext context) : base(insPoint, context)
        {
            Radius = radius;
        }
        public override MeshGeometry3D GetMesh()
        {
            var mesh = new MeshGeometry3D();

            AddSphere(mesh, new Point3D(X, Y, Z), Radius, 40, 40);

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

            var propRadius = new PropertyValue
            {
                Property = _dbContext.Properties.Where(p => p.Name == "Radius").SingleOrDefault(),
                Value = this.Radius.ToString()
            };
            props.Add(propRadius);

            obj.FigureType = _dbContext.FigureTypes.Where(t => t.Name == "Sphere").SingleOrDefault();
            foreach (var p in props)
                obj.PropertyValues.Add(p);

            scene.Object3Ds.Add(obj);
        }


        private void AddSphere(MeshGeometry3D mesh, Point3D center, double radius, int num_phi, int num_theta)
        {
            double phi0, theta0;
            double dphi = Math.PI / num_phi;
            double dtheta = 2 * Math.PI / num_theta;

            phi0 = 0;
            double y0 = radius * Math.Cos(phi0);
            double r0 = radius * Math.Sin(phi0);
            for (int i = 0; i < num_phi; i++)
            {
                double phi1 = phi0 + dphi;
                double y1 = radius * Math.Cos(phi1);
                double r1 = radius * Math.Sin(phi1);

                // Point ptAB has phi value A and theta value B.
                // For example, pt01 has phi = phi0 and theta = theta1.
                // Find the points with theta = theta0.
                theta0 = 0;
                Point3D pt00 = new Point3D(
                    center.X + r0 * Math.Cos(theta0),
                    center.Y + y0,
                    center.Z + r0 * Math.Sin(theta0));
                Point3D pt10 = new Point3D(
                    center.X + r1 * Math.Cos(theta0),
                    center.Y + y1,
                    center.Z + r1 * Math.Sin(theta0));
                for (int j = 0; j < num_theta; j++)
                {
                    // Find the points with theta = theta1.
                    double theta1 = theta0 + dtheta;
                    Point3D pt01 = new Point3D(
                        center.X + r0 * Math.Cos(theta1),
                        center.Y + y0,
                        center.Z + r0 * Math.Sin(theta1));
                    Point3D pt11 = new Point3D(
                        center.X + r1 * Math.Cos(theta1),
                        center.Y + y1,
                        center.Z + r1 * Math.Sin(theta1));

                    // Create the triangles.
                    AddTriangle(mesh, pt00, pt11, pt10);
                    AddTriangle(mesh, pt00, pt01, pt11);

                    // Move to the next value of theta.
                    theta0 = theta1;
                    pt00 = pt01;
                    pt10 = pt11;
                }

                // Move to the next value of phi.
                phi0 = phi1;
                y0 = y1;
                r0 = r1;
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

