﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Models
{
    public class Result
    {
        public int Id { get; set; }
        public string ResultName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }


        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }

}
