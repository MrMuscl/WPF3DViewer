using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class Scene
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FigureId { get; set; }

        public virtual Figure Figure { get; set; }
    }
}
