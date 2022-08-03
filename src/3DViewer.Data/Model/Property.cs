using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class Property
    {
        public Property()
        {
            FigureTypes = new HashSet<FigureType>();
            PropertyValues = new HashSet<PropertyValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FigureType> FigureTypes { get; set; }
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
    }
}
