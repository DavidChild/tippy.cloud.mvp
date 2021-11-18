using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tippy.cloud.Shared.Domain
{
    public class MenuItem
    {
        public int id { get; set; }
        public virtual List<MenuItem> Children { get; set; } = default!;
       
        public string Icon { get; set; }
        public string Locale { get; set; }
        public virtual string Name { get; set; }
        public string Key { get; set; }
        public string Path { get; set; }
     }
        
    }

