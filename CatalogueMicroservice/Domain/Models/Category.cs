namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class Category
{
    private Category(string name, CategoryOption categoryOption)
    {
        this.name = name;
        this.categoryOption = categoryOption;
    }

    public static Category? FromID(string? id)
    {
        switch(id)
        {
            case "0":
                return SNEAKERS;
            
            case "1":
                return FORMALS;

            case "2":
                return SLIPPERS;
            
            case "3":
                return SANDALS;

            case null:
                return null;

            default:
                throw new CategoryWithIdNotAvailableException();
        }
    }

    public override bool Equals(object? obj)
    {
        if(obj == null)
            return false;

        if(obj.GetType() != this.GetType())
            return false;
        
        Category other = (Category)obj;
        return other.categoryOption == categoryOption;
    }

    public override int GetHashCode() => (int)categoryOption;

    public String toDatabaseForm() => name;


    public readonly string name;
    private readonly CategoryOption categoryOption;


    public readonly static Category SNEAKERS = new Category("sneakers", CategoryOption.Sneakers); 
    public readonly static Category FORMALS = new Category("formals", CategoryOption.Formals);
    public readonly static Category SLIPPERS = new Category("slippers", CategoryOption.Slippers);
    public readonly static Category SANDALS = new Category("sandals", CategoryOption.Sandals);

    private enum CategoryOption
    {
        Sneakers,
        Formals,
        Slippers,
        Sandals
    }
}

public class CategoryWithIdNotAvailableException : Exception {}