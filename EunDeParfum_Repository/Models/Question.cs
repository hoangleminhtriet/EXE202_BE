﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Status { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }

}
