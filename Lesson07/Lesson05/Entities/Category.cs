namespace Lesson05.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; }

    public int? ParentId { get; set; }
    public Category? Parent { get; set; }

    public virtual ICollection<Course> Courses { get; set; }
    public virtual ICollection<Category> ChildCategories { get; set; }

    public Category()
    {
        Courses = [];
        ChildCategories = [];
    }
}
