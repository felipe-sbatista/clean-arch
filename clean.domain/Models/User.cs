﻿using System;
using clean.domain.core.Models;

namespace clean.domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
    }
}
