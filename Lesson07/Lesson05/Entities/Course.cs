namespace Lesson05.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Hours { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual ICollection<CourseAssignment> Assignments { get; set; }

    public Course()
    {
        Assignments = [];
    }
}
