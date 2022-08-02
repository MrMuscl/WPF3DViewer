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
            FigureProperties = new HashSet<FigureProperty>();
            PropertyValues = new HashSet<PropertyValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FigureProperty> FigureProperties { get; set; }
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
    }
}
