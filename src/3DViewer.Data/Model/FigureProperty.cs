using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class FigureProperty
    {
        public int Id { get; set; }
        public int FigureTypeId { get; set; }
        public int PropertyId { get; set; }

        public virtual FigureType FigureType { get; set; }
        public virtual Property Property { get; set; }
    }
}
