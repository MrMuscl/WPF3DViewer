using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class FigurePropertiesSet
    {
        public int Id { get; set; }
        public int PropertyValueId { get; set; }
        public int Object3Did { get; set; }

        public virtual Object3D Object3D { get; set; }
        public virtual PropertyValue PropertyValue { get; set; }
    }
}
