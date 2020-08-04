using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Model
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
