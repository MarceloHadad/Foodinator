namespace Foodinator
{
    public class Game : IGame
    {
        private readonly DishRepository _dishRepository;
        private readonly List<string> confirmedCharacteristics = [];

        public Game(DishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public void Start()
        {
            if (!_dishRepository.GetAllDishes().Any())
            {
                var initialDish = new Dish("BOLO DE CENOURA COM COBERTURA DE CHOCOLATE", ["DOCE", "COBERTURA", "CHOCOLATE"]);
                _dishRepository.AddDish(initialDish);
            }

            bool playAgain;

            do
            {
                confirmedCharacteristics.Clear();
                Console.WriteLine("Pense em um prato que você gosta.");

                GuessDish(_dishRepository.GetAllDishes());

                var response = ReadValidInput("Você gostaria de jogar novamente? (S/N)", ["S", "N"]);
                playAgain = response == "S";

            } while (playAgain);
        }

        public void GuessDish(List<Dish> dishes)
        {
            while (dishes.Count >= 1)
            {
                var dishesCharacteristics = dishes.SelectMany(d => d.Characteristics).Distinct().Except(confirmedCharacteristics).ToList();
                foreach (var characteristic in dishesCharacteristics)
                {
                    var response = ReadValidInput($"O prato que você pensou é/tem {characteristic}? (S/N)", ["S", "N"]);

                    if (response == "S")
                    {
                        confirmedCharacteristics.Add(characteristic);
                        dishes = dishes
                            .Where(dish => dish.Characteristics.Contains(characteristic))
                            .ToList();
                    }
                    else
                    {
                        dishes = dishes
                            .Where(dish => !dish.Characteristics.Contains(characteristic))
                            .ToList();
                    }

                    if (dishes.Count == 1)
                    {
                        var guessedDish = dishes[0];
                        response = ReadValidInput($"O prato que você pensou é {guessedDish.Name}? (S/N)", ["S", "N"]);

                        if (response == "S")
                        {
                            Console.WriteLine("Aha! Adivinhei seu prato!");
                            return;
                        }
                        else
                        {
                            LearnNewDish(guessedDish);
                            return;
                        }
                    }
                }

                if (dishes.Count == 0)

                {
                    LearnNewDish(null);
                    return;
                }
            }
        }

        public void LearnNewDish(Dish? incorrectDish)
        {
            if (incorrectDish == null)
            {
                string newDishName = ReadValidInput("Então eu não conheço esse prato. Qual prato você pensou?", null);

                string newCharacteristic = ReadValidInput($"Qual característica {newDishName} tem?", null);

                var newDish = new Dish(newDishName, new List<string>(confirmedCharacteristics));
                newDish.AddCharacteristic(newCharacteristic);
                _dishRepository.AddDish(newDish);
            }
            else
            {
                string newDishName = ReadValidInput("Perdi. Qual prato você pensou?", null);

                string distinguishingCharacteristic = ReadValidInput($"Qual característica {newDishName} tem _____ mas {incorrectDish.Name} não?", null);

                var newDish = new Dish(newDishName, new List<string>(confirmedCharacteristics));
                newDish.AddCharacteristic(distinguishingCharacteristic);

                _dishRepository.AddDish(newDish);
            }
        }

        public static string ReadValidInput(string message, List<string>? validInputs)
        {
            string? input;

            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(input))
                {
                    do
                    {
                        Console.WriteLine("Input inválido.");
                        Console.WriteLine(message);
                        input = Console.ReadLine();

                    } while (string.IsNullOrWhiteSpace(input));
                }

                if (validInputs == null)
                {
                    break;
                }


            } while (!validInputs.Contains(input));

            return input;
        }
    }
}
