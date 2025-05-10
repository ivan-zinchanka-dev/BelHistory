namespace BelHistory.Domain.API.Models;

public class Category
{
    //public Category ParentCategory { get; private set; }
    public LocalizedObject LocalizedObject { get; private set; }
    public List<Category> SubCategories { get; private set; }

    public Category(LocalizedObject localizedObject)
    {
        LocalizedObject = localizedObject;
    }

    public Category SetSubCategories(List<Category> subCategories)
    {
        SubCategories = subCategories;
        return this;
    }
}