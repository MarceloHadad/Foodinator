namespace Foodinator
{
    public class DishRepository : IDishRepository
    {
        private readonly List<Dish> _dishes;

        public DishRepository()
        {
            _dishes = new List<Dish>();
        }
        public List<Dish> GetAllDishes()
        {
            return _dishes;
        }
        public void AddDish(Dish dish)
        {
            _dishes.Add(dish);
        }
    }
}
