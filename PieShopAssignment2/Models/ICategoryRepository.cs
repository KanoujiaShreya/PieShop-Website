namespace PieShopAssignment2.Models
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> AllCategories { get; }
    }
}
