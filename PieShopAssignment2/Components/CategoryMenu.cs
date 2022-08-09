using Microsoft.AspNetCore.Mvc;
using PieShopAssignment2.Models;

namespace PieShopAssignment2.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categoryItems = this.categoryRepository.AllCategories;
            return View(categoryItems);
        }
    }
}
