using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract2WPF
{
    public class Note
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        public Note (string name, string description, DateTime date)
        {
            this.name = name;
            this.description = description;
            this.date = date;
        
        }
    }
}
