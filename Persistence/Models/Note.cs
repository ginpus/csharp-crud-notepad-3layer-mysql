using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Persistence.Models;
using Persistence.Repositories;

namespace Persistence.Models
{
    public class Note //names of fields match names of fields in SQL DB
    {
        public int Note_Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date_Created { get; set; }

        public override string ToString()
        {
            return $"{Note_Id} {Title} {Text} {Date_Created}";
        }
    }
}