﻿using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class OwnerSign
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Animal> Animals { get; set; }
    }
}
