using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapping.Models
{
    public class CallListItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }
        public DateTime ExtractDateTime { get; set; }
    }

}
