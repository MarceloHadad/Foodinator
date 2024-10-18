namespace Foodinator
{
    public class Dish
    {
        public string Name { get; private set; }
        public List<string> Characteristics { get; private set; }

        public Dish(string name, List<string> characteristics)
        {
            Name = name;
            Characteristics = characteristics;
        }

        public void AddCharacteristic(string characteristic)
        {
            if (!Characteristics.Contains(characteristic))
            {
                Characteristics.Add(characteristic);
            }
        }
    }
}
