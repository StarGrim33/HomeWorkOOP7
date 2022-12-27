namespace HomeWorkOOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandFormTrain = "1";
            const string CommandShowDepot = "2";
            const string CommandExit = "3";

            bool isProgrammOn = true;
            User user = new("Аноним");
            Depot depot = new();

            while (isProgrammOn)
            {
                Console.Clear();
                Console.WriteLine($"Здравствуйте, {user.Name}");
                Console.WriteLine($"Текущие рейсы: ");
                depot.ShowDepot();
                Console.WriteLine($"Меню:");
                Console.WriteLine($"{CommandFormTrain}-Составить поезд");
                Console.WriteLine($"{CommandShowDepot}-Показать депо");
                Console.WriteLine($"{CommandExit}-Выйти из программы");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandFormTrain:
                        depot.CreateTrain();
                        break;

                    case CommandShowDepot:
                        depot.ShowDepot();
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
        private string? _direction = null;
        private int _passengers = 0;
        private int _vans = 0;
        private int _freePlace = 540;
        private int _occupiedPlace = 0;
        private int _freePlaceInVan = 36;
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
                    Console.WriteLine($"Направление: {train.Direction}, занято мест: {train.OccupiedPlace}, свободно мест: {train.FreePlace}, вагонов: {train.Vans}, отправлен: {train.IsSend}");
                }
            }
        }

        public void CreateTrain()
        {
            SetDirection();
            SellTickets();
            SetTrain();
        }

        private string? SetDirection()
        {
            Console.WriteLine("Введите маршрут следования: ");
            _direction = Console.ReadLine();
            return _direction;
        }

        private float SellTickets()
        {
            int minNumber = 0;
            int maxNumber = 540;
            Random random = new();

            _passengers = random.Next(minNumber, maxNumber);
            return _passengers;
        }

        private void SetTrain()
        {
            CalculateOccupiedPlace();
            CalculateFreePlaces();
            CalculateVans();
            SendTrain();
            _trains.Add(new Train(_direction, _occupiedPlace, _freePlace, _vans, _isSend));
            Console.WriteLine("Поезд сформирован");
            _passengers = 0;
            _vans = 0;
            _freePlace = 540;
            _occupiedPlace = 0;
            _freePlaceInVan = 36;
            Console.ReadKey();
        }

        private void SendTrain()
        {
            const string CommandYes = "Да";

            Console.WriteLine("Поезд отправляется ? ");
            string? userChoice = Console.ReadLine();

            if (userChoice == CommandYes)
            {
                _isSend = true;
                Console.WriteLine("Поезд готов к убытию!");
            }
            else
            {
                Console.WriteLine("Поезд пока не отправлен, он будет ждать своего отправления в депо.");
            }
        }

        private int CalculateFreePlaces()
        {
            _freePlace -= _occupiedPlace;
            return _freePlace;
        }

        private int CalculateOccupiedPlace()
        {
            _occupiedPlace = _freePlace - _passengers;
            return _occupiedPlace;
        }

        private int CalculateVans()
        {
            _vans = _occupiedPlace / _freePlaceInVan;

            return _vans;
        }
    }

    class Train
    {
        public string Direction { get; private set; }
        public int OccupiedPlace { get; private set; }
        public int FreePlace { get; private set; }
        public int Vans { get; private set; }
        public bool IsSend { get; private set; }

        public Train(string direction, int occupiedPlace, int freePlace, int vans, bool isSend)
        {
            Direction = direction;
            OccupiedPlace = occupiedPlace;
            FreePlace = freePlace;
            Vans = vans;
            IsSend = isSend;
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
}