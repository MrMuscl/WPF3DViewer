using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class Object3D
    {
        public Object3D()
        {
            FigurePropertiesSets = new HashSet<FigurePropertiesSet>();
        }

        public int Id { get; set; }
        public int FigureTypeId { get; set; }
        public int SceneId { get; set; }

        public virtual FigureType FigureType { get; set; }
        public virtual Scene Scene { get; set; }
        public virtual ICollection<FigurePropertiesSet> FigurePropertiesSets { get; set; }
    }
}
