namespace HomeWorkOOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandFormTrain = "1";
            const string CommandExit = "2";

            bool isProgrammOn = true;
            User user = new();
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
                        depot.AddTrain(train, van, sittingVan, reservedVan);
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
        private int _lastId = 0;
        private string? _direction;
        private int _totalPlaces = 540;
        private int _occupiedPlaces;
        private int _vans;
        private bool _isSend = false;

        private Random _random = new();

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
                    Console.WriteLine($"Направление: {train.Direction}, всего мест в поезде: {train.AllPlaces}, свободно мест: {train.FreePlace}, занято мест: {train.OccupiedPlace}, вагонов: {train.Vans}, отправлен: {train.IsSend}");
                }
            }
        }

        public void AddTrain(Train train, Van van, SittingVan sitting, ReservedVan reserved)
        {
            ++_lastId;
            Console.WriteLine("Введите направление поезда: ");
            _direction = Console.ReadLine();
            _occupiedPlaces = _random.Next(1, 540);
            Console.WriteLine($"Занято мест: {_occupiedPlaces}");
            ChooseVan(train, van, sitting, reserved);
            _trains.Add(new Train(_lastId, _direction, _occupiedPlaces, _totalPlaces, _vans, _isSend));
        }

        public void ChooseVan(Train train, Van van, SittingVan sitting, ReservedVan reserved)
        {
            CalculatePlaceForVans(train, van, sitting, reserved);
        }

        public void CalculatePlaceForVans(Train train, Van van, SittingVan sitting, ReservedVan reserved)
        {
            const string CommandChooseCommonVan = "1";
            const string CommandChooseSittingVan = "2";
            const string CommandChooseReservedVan = "3";

            Console.WriteLine("Выберите вагоны: ");
            Console.WriteLine($"1.{van.Name} - мест: {van.Places}, 2.{sitting.Name} - мест: {sitting.Places}, 3.{reserved.Name} - мест: {reserved.Places}");

            string userInput = Console.ReadLine();
            bool isOn = true;

            if (CounterVans(van))
            {
                switch (userInput)
                {
                    case CommandChooseCommonVan:
                        Console.WriteLine($"Осталось незанятых мест: {_occupiedPlaces - van.Places}");
                        van.SetVanNumber(train);
                        Console.ReadKey();
                        break;

                    case CommandChooseSittingVan:
                        sitting.SetVanNumber();
                        break;

                    case CommandChooseReservedVan:
                        reserved.SetVanNumber();
                        break;
                }
            }

        }

        public bool CounterVans(Van van)
        {
            if(_occupiedPlaces >= van.Places)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    class Train
    {
        private Random random = new();

        public int Id { get; private set; }
        public string Direction { get; private set; }
        public int OccupiedPlace { get; private set; }
        public int AllPlaces { get; private set; } = 540;
        public int FreePlace { get { return FreePlace = AllPlaces - OccupiedPlace; } private set { } }
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

        public string SetDirection()
        {
            Console.WriteLine("Введите желаемое направление: ");
            string userInput = Console.ReadLine();

            if (userInput != null)
            {
                Direction = userInput;
            }

            return Direction;
        }

        public void SellTickets()
        {
            OccupiedPlace = random.Next(1, 540);
        }
    }

    class User
    {
        public string? Name { get; private set; }

        public User(string name = "Аноним")
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
        private List<Van> _van = new();
        public int Places { get; private set; } = 36;
        public string Name { get; set; }
        public int VansNumber { get; private set; }

        public Van()
        {
            Name = "Обычный вагон";
            VansNumber = 0;
        }

        public void ShowVan()
        {
            Console.WriteLine($"{Name}, количество мест: {Places}");
        }

        public void SetVanNumber(Train train)
        {
            _van.Add(new Van());
            VansNumber++;
            Console.WriteLine("Вагон добавлен");
            ShowVan();
        }

        public int InitPlaces()
        {
            int result = VansNumber * Places;
            return result;
        }
    }

    class SittingVan : Van
    {
        private List<SittingVan> _sittingVan = new();
        public new int Places { get; private set; } = 68;

        public SittingVan()
        {
            Name = "Сидячий вагон";
        }

        public void SetVanNumber()
        {
            _sittingVan.Add(new SittingVan());
            Console.WriteLine("Вагон добавлен");
            Console.ReadKey();
        }
    }

    class ReservedVan : Van
    {
        private List<ReservedVan> _reservedVan = new();

        public new int Places { get; private set; } = 54;

        public ReservedVan()
        {
            Name = "Плацкарт";
        }

        public void SetVanNumber()
        {
            _reservedVan.Add(new ReservedVan());
            Console.WriteLine("Вагон добавлен");
            Console.ReadKey();
        }
    }
}