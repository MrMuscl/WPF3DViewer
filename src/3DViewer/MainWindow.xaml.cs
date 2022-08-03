using _3DViewer.Data.Model;
using _3DViewer.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3DViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private _3DViewerContext _dbContext;
        private static readonly string _dbName = "_3DViewerDB_new";
        private readonly string _connStr = $"data source=ANTONK-573;initial catalog={_dbName};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";

        private ModelVisual3D _modelVisual = new ModelVisual3D();
        private Model3DGroup _model3DGroup = new Model3DGroup();

        private Point _clickedPosition = new Point(0, 0);
        private bool _capturedLeft = false;
        private bool _capturedRight = false;

        private List<Figure3D> _figures = new List<Figure3D>();

        public MainWindow()
        {
             _dbContext = new _3DViewerContext(_connStr);
            
            InitializeComponent();
            ResetDB();
            ComboBox_SelectionChanged(this, null);
            SetupLight();
            ConfigureViewPortAndModelVisual();

            //AddCube(new Point3D(0,0,0), 4, Colors.Black);
            //AddPyramid(new Point3D(0, 0, 0), 4, 6, Colors.Blue);
        }

        private void SetupLight()
        {
            var ambientLight = new AmbientLight(Colors.LightGray);
            var directionalLight = new DirectionalLight();
            directionalLight.Color = Colors.White;
            directionalLight.Direction = new Vector3D(-0.61, -0.5, -0.61);

            _model3DGroup.Children.Add(directionalLight);
            _model3DGroup.Children.Add(ambientLight);
        }

        private void ResetDB() 
        {
            _dbContext.Database.Delete();

            var types = new List<FigureType>();
            types.Add(new FigureType { Name = "Cube" });
            types.Add(new FigureType { Name = "Piramid" });
            types.Add(new FigureType { Name = "Cone" });
            types.Add(new FigureType { Name = "Cylinder" });
            types.Add(new FigureType { Name = "Sphere" });
            _dbContext.FigureTypes.AddRange(types);

            var properties = new List<Property>();
            properties.Add(new Property { Name = "X" });
            properties.Add(new Property { Name = "Y" });
            properties.Add(new Property { Name = "Z" });
            properties.Add(new Property { Name = "Side" });
            properties.Add(new Property { Name = "Height" });
            properties.Add(new Property { Name = "Radius" });
            _dbContext.Properties.AddRange(properties);
            _dbContext.SaveChanges();

            var cube = _dbContext.FigureTypes.Where(t => t.Name == "Cube").SingleOrDefault();
            var props = new List<Property>();
            props.Add(_dbContext.Properties.Where(p => p.Name == "X").SingleOrDefault());
            props.Add(_dbContext.Properties.Where(p => p.Name == "Y").SingleOrDefault());
            props.Add(_dbContext.Properties.Where(p => p.Name == "Z").SingleOrDefault());
            props.Add(_dbContext.Properties.Where(p => p.Name == "Side").SingleOrDefault());
            //cube.FigureProperties.

        }

        private void ConfigureViewPortAndModelVisual()
        {
            // Add the group of models to the ModelVisual3d.
            _modelVisual.Content = _model3DGroup;
            _viewPort.Children.Add(_modelVisual);

            // Add surface plane
            //AddPlane(new Point3D(0, 0, 0), 1000);
        }

        private void AddCube(Point3D pt, double size, Color color)
        {
            var geometryModel = new GeometryModel3D();
            var box = new Cube3D(pt, size);

            var mesh = box.GetMesh();
            geometryModel.Geometry = mesh;

            DiffuseMaterial material = new DiffuseMaterial(new SolidColorBrush(color));
            geometryModel.Material = material;

            _model3DGroup.Children.Add(geometryModel);
        }

        private void AddPyramid(Point3D pt, double size, double height, Color color)
        {
            var geometryModel = new GeometryModel3D();
            var pyramid = new Pyramid3D(pt, size, height);

            var mesh = pyramid.GetMesh();
            geometryModel.Geometry = mesh;

            DiffuseMaterial material = new DiffuseMaterial(new SolidColorBrush(color));
            geometryModel.Material = material;

            _model3DGroup.Children.Add(geometryModel);
        }

        private void MoveCamera(double dx, double dy, double dz)
        {
            var v = new Vector3D(_camera.Position.X, _camera.Position.Y, _camera.Position.Z);
            var r = v.Length;

            var theta = Math.Acos(v.Y / r);
            var phi = Math.Atan2(-v.Z, v.X);

            theta -= dy * 0.01;
            phi -= dx * 0.01;
            r *= 1.0 - 0.1 * dz;

            theta = Math.Min(Math.Max(theta, 0.0001), Math.PI - 0.0001);

            v.X = r * Math.Sin(theta) * Math.Cos(phi);
            v.Z = -r * Math.Sin(theta) * Math.Sin(phi);
            v.Y = r * Math.Cos(theta);

            _camera.Position = new Point3D(v.X, v.Y, v.Z);
            _camera.LookDirection = new Vector3D(-v.X, -v.Y, -v.Z);
        }

        private Figure3D CreateFigureFromUserInput()
        {
            FigureTypeEnum ft = FigureTypeEnum.Конус;
            var item = figureTypeCombobox.SelectedItem;
            var type = (FigureTypeEnum)TypeDescriptor.GetConverter(ft).ConvertFrom(item.ToString());
            Figure3D figure = null;
            
            switch (type)
            {
                case FigureTypeEnum.Куб:
                    figure = new Cube3D(new Point3D(figurePropControl.X,
                                                      figurePropControl.Y,
                                                      figurePropControl.Z),
                                                      figurePropControl.Side);
                    break;

                case FigureTypeEnum.Пирамида:
                    figure = new Pyramid3D(new Point3D(figurePropControl.X,
                                                      figurePropControl.Y,
                                                      figurePropControl.Z),
                                                      figurePropControl.Side,
                                                      figurePropControl.Heihgt);
                    break;
            }

            return figure;
        }

        private MeshGeometry3D GetMeshForFigure() 
        {
            FigureTypeEnum ft = FigureTypeEnum.Конус;
            var item = figureTypeCombobox.SelectedItem;
            var type = (FigureTypeEnum)TypeDescriptor.GetConverter(ft).ConvertFrom(item.ToString());
            
            MeshGeometry3D mesh = null;

            switch (type) 
            {
                case FigureTypeEnum.Куб:
                    var cube = new Cube3D(new Point3D(figurePropControl.X,
                                                      figurePropControl.Y,
                                                      figurePropControl.Z),
                                                      figurePropControl.Side);
                    mesh = cube.GetMesh();
                    break;
                
                case FigureTypeEnum.Пирамида:
                    var pyramid = new Pyramid3D(new Point3D(figurePropControl.X,
                                                      figurePropControl.Y,
                                                      figurePropControl.Z),
                                                      figurePropControl.Side,
                                                      figurePropControl.Heihgt);
                    mesh = pyramid.GetMesh();
                    break;
            }

            return mesh;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            var item = figureTypeCombobox.SelectedItem;

            FigureTypeEnum ft = FigureTypeEnum.Конус;
            var res =(FigureTypeEnum)TypeDescriptor.GetConverter(ft).ConvertFrom(item.ToString());

            if (figurePropControl != null)
                figurePropControl.Update(res);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickedPosition = e.GetPosition(this);
            //UpdateUI();

            if (_clickedPosition.X <= _viewPort.ActualWidth &&
                _clickedPosition.Y <= _viewPort.ActualHeight)
            {
                _capturedLeft = true;
                Mouse.Capture(sender as UIElement);
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _capturedLeft = false;
            if (!_capturedRight)
                Mouse.Capture(null);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            if (pos.X > _viewPort.ActualWidth && pos.Y > _viewPort.ActualHeight)
                return;

            if (!_capturedLeft && !_capturedRight)
                return;

            var d = pos - _clickedPosition;

            if (_capturedLeft && !_capturedRight)
            {
                MoveCamera(d.X, d.Y, 0);
            }
            else if (!_capturedLeft && _capturedRight)
            {
                var cp = _camera.Position;
                var yOffset = d.Y * 0.001 * Math.Sqrt(cp.X * cp.X + cp.Z * cp.Z);
                var xOffset = d.X * 0.001 * Math.Sqrt(cp.Y * cp.Y + cp.Z * cp.Z);
                _camera.Position = new Point3D(_camera.Position.X - xOffset,
                                               _camera.Position.Y + yOffset,
                                               _camera.Position.Z);
            }

            _clickedPosition = pos;
            //UpdateUI();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickedPosition = e.GetPosition(this);
            _capturedRight = true;
            Mouse.Capture(sender as UIElement);
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _capturedRight = false;
            if (!_capturedLeft)
                Mouse.Capture(null);
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MoveCamera(0, 0, Math.Sign(e.Delta));
            //UpdateUI();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            Figure3D figure = CreateFigureFromUserInput();
            
            var mesh = GetMeshForFigure();
            var geometryModel = new GeometryModel3D();
            geometryModel.Geometry = mesh;

            DiffuseMaterial material = new DiffuseMaterial(new SolidColorBrush(Colors.LightGray));
            geometryModel.Material = material;

            _model3DGroup.Children.Add(geometryModel);
            _figures.Add(figure);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var figure in _figures) 
            {
                string str = "";
                if (figure is Cube3D)
                {
                    str = "It is cube";
                }
                else if (figure is Pyramid3D) 
                {
                    str = "It is pyramide";
                }
                
                
                //switch (figure.GetType()) 
                //{
                //    case typeof(Cube3D):
                //        break;
                //    case typeof(Pyramid3D):
                //        break;
                //    default:
                //        break;
                //}
            }
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            _model3DGroup.Children.Clear();
            _model3DGroup.Children = new Model3DCollection();

            SetupLight();
        }
    }
}
