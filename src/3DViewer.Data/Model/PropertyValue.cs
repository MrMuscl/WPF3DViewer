using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class PropertyValue
    {
        public PropertyValue()
        {
            Figures = new HashSet<Figure>();
        }

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }

        public virtual Property Property { get; set; }
        public virtual ICollection<Figure> Figures { get; set; }
    }
}
