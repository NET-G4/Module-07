namespace Lesson05.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
