﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab08.Entities
{
    public class StudentGroup
    {
        public int IdGroup { get; set; }
        public int IdStudent { get; set; }
        public DateTime AddedAt { get; set; }

        public virtual Group IdGroupNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }

    }
}
