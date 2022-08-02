using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DViewer.Data.Model
{
    public class Figure
    {
        public Figure()
        {
            Scenes = new HashSet<Scene>();
        }

        public int Id { get; set; }
        public int FigureTypeId { get; set; }
        public int PropertyValueId { get; set; }

        public virtual FigureType FigureType { get; set; }
        public virtual PropertyValue PropertyValue { get; set; }
        public virtual ICollection<Scene> Scenes { get; set; }
    }
}
