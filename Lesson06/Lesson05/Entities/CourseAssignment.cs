namespace Lesson05.Entities
{
    public class CourseAssignment
    {
        public int Id { get; set; }
        public string Room { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
