﻿using System.ComponentModel.DataAnnotations;

namespace Lesson05.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

        public Department()
        {
            Instructors = new List<Instructor>();
        }
    }
}
