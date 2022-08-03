using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class Scene
    {
        public Scene()
        {
            Object3Ds = new HashSet<Object3D>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Object3D> Object3Ds { get; set; }
    }
}
