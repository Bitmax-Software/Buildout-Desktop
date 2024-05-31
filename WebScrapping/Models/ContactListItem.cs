using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapping.Models
{
    public class ContactListItem
    {
        public string Text { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string ParentId { get; set; }
        public string Href { get; set; }
        public DateTime ExtractDateTime { get; set; }
    }
}
