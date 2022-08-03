using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }

        public virtual Property Property { get; set; }
        public virtual Object3D Object3D { get; set; }
    }
}
