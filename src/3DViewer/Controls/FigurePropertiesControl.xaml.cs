using _3DViewer.Primitives;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3DViewer.Controls
{
    /// <summary>
    /// Interaction logic for FigurePropertiesControl.xaml
    /// </summary>
    public partial class FigurePropertiesControl : UserControl
    {
        public FigurePropertiesControl()
        {
            InitializeComponent();
        }

        public double X 
        {
            get
            {
                double val = 0;
                return (double.TryParse(xCoordTextbox.Text, out val)) ? val : 0;
            } 
        }
        public double Y
        {
            get
            {
                double val = 0;
                return (double.TryParse(yCoordTextbox.Text, out val)) ? val : 0;
            }
        }
        public double Z
        {
            get
            {
                double val = 0;
                return (double.TryParse(zCoordTextbox.Text, out val)) ? val : 0;
            }
        }
        public double Side
        {
            get
            {
                double val = 0;
                return (double.TryParse(sideTextbox.Text, out val)) ? val : 0;
            }
        }

        public double Heihgt
        {
            get
            {
                double val = 0;
                return (double.TryParse(heightTextbox.Text, out val)) ? val : 0;
            }
        }
        public double Radius
        {
            get
            {
                double val = 0;
                return (double.TryParse(radiusTextbox.Text, out val)) ? val : 0;
            }
        }

        public void Update(FigureTypeEnum type) 
        {
            sideTextbox.IsEnabled = false;
            sideLabel.IsEnabled = false;
            heightTextbox.IsEnabled = false;
            heightLabel.IsEnabled = false;
            radiusTextbox.IsEnabled = false;
            radiusLabel.IsEnabled = false;


            if (type == FigureTypeEnum.Куб)
            {
                sideTextbox.IsEnabled = true;
                sideLabel.IsEnabled = true;
            }
            else if (type == FigureTypeEnum.Цилиндр|| type == FigureTypeEnum.Конус)
            {
                radiusTextbox.IsEnabled = true;
                radiusLabel.IsEnabled = true;

                heightTextbox.IsEnabled = true;
                heightLabel.IsEnabled = true;
            }
            else if (type == FigureTypeEnum.Сфера) 
            {
                radiusTextbox.IsEnabled = true;
                radiusLabel.IsEnabled = true;
            }
            else if (type == FigureTypeEnum.Пирамида)
            {
                sideTextbox.IsEnabled = true;
                sideLabel.IsEnabled = true;
                heightTextbox.IsEnabled = true;
                heightLabel.IsEnabled = true;
            }
        }
    }
}
