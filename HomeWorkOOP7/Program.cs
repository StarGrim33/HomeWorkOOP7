namespace HomeWorkOOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandFormTrain = "1";
            const string CommandExit = "2";

            bool isProgrammOn = true;
            User user = new("Аноним");
            Depot depot = new();
            Van van = new();
            SittingVan sittingVan = new();
            ReservedVan reservedVan = new();
            Train train = new();
            while (isProgrammOn)
            {
                Console.Clear();
                Console.WriteLine($"Здравствуйте, {user.Name}");
                Console.WriteLine($"Текущие рейсы: ");
                depot.ShowDepot();
                Console.WriteLine($"Меню:");
                Console.WriteLine($"{CommandFormTrain}-Составить поезд");
                Console.WriteLine($"{CommandExit}-Выйти из программы");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandFormTrain:
                        depot.CreateTrain(train, van, sittingVan, reservedVan);
                        break;

                    case CommandExit:
                        isProgrammOn = false;
                        break;

                    default:
                        Console.WriteLine("Выберите цифрой пункт меню");
                        break;
                }
            }
        }
    }

    class Depot
    {
        private List<Train> _trains = new();
        
        private bool _isSend = false;

        public void ShowDepot()
        {
            if (_trains.Count == 0)
            {
                Console.WriteLine("Список пуст");
            }
            else
            {
                foreach (Train train in _trains)
                {
                    Console.WriteLine($"Направление: {train.Direction}, всего мест в поезде: {train.FreePlace}, свободно мест: {train.FreePlace}, занято мест: {train.OccupiedPlace}, вагонов: {train.Vans}, отправлен: {train.IsSend}");
                }
            }
        }

        public void CreateTrain(Train train, Van van, SittingVan sittingVan, ReservedVan reservedVan)
        {
            train.SetDirection(out _);
            train.SellTickets(out _);

            if(train.OccupiedPlace >1)
            {
                Console.WriteLine($"На поезд зарегистрировано {train.OccupiedPlace} пассажиров");
                Console.WriteLine("Нужно сформировать вагоны, какие вы выберете?:");
                Console.WriteLine($"{van.Name} мест: {van.Places}, {sittingVan.Name} мест: {sittingVan.Places}, {reservedVan.Name} мест: {reservedVan.Places}");
                string userInput = Console.ReadLine();
            }



            _trains.Add(train);
        }
    }
    class Train
    {
        public int Id { get; private set; }
        public string Direction { get; private set; }
        public int OccupiedPlace { get; private set; }
        public int FreePlace { get; private set; } = 540;
        public int Vans { get; private set; }
        public bool IsSend { get; private set; }

        public Train(int id, string direction, int occupiedPlace, int freePlace, int vans, bool isSend)
        {
            Id = id;
            Direction = direction;
            OccupiedPlace = occupiedPlace;
            FreePlace = freePlace;
            Vans = vans;
            IsSend = isSend;
        }

        public Train()
        {
        }

        public Train SetDirection(out Train? train)
        {
            train = null;
            Console.WriteLine("Введите желаемое направление: ");
            string userInput = Console.ReadLine();

            if(userInput != null)
            {
                Direction = userInput;
            }

            return train;
        }

        public Train SellTickets(out Train? train)
        {
            train = null;
            Random random = new();
            OccupiedPlace = random.Next(1, 540);
            return train;
        }

    }

    class User
    {
        public string? Name { get; private set; }

        public User(string name)
        {
            Name = name;
            SetName();
        }

        public void SetName()
        {
            Console.WriteLine("Здравствуйте, как я могу к Вам обращаться?: ");
            Name = Console.ReadLine();

            Name ??= "Аноним";
        }
    }

    class Van
    {
        public int Places { get; private set; } = 36;
        public string Name { get; set; }

        public Van()
        {
            Name = "Обычный вагон";
        }

        public void ShowVan()
        {
            Console.WriteLine($"{Name}, количество мест: {Places}");
        }
    }

    class SittingVan : Van
    {
        public new int Places { get; private set; } = 68;

        public SittingVan()
        {
            Name = "Сидячий вагон";
        }
    }

    class ReservedVan : Van
    {
        public new int Places { get; private set; } = 54;

        public ReservedVan()
        {
            Name = "Плацкарт";
        }
    }
}