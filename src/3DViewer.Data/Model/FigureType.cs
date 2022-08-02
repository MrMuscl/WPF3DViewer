using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class FigureType
    {
        public FigureType()
        {
            FigureProperties = new HashSet<FigureProperty>();
            Figures = new HashSet<Figure>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FigureProperty> FigureProperties { get; set; }
        public virtual ICollection<Figure> Figures { get; set; }
    }
}
