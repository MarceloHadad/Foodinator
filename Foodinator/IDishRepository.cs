namespace Foodinator
{
    public interface IDishRepository
    {
        List<Dish> GetAllDishes();
        void AddDish(Dish dish);
    }
}