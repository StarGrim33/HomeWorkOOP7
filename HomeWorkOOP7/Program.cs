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

            while (isProgrammOn)
            {
                Console.Clear();
                Console.WriteLine($"Здравствуйте, {user.Name}");
                Console.WriteLine($"Текущие рейсы: ");

                depot.ShowDepot();

                Console.WriteLine($"Меню:");
                Console.WriteLine($"{CommandFormTrain}-Составить поезд");
                Console.WriteLine($"{CommandExit}-Выйти из программы");

                string? userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandFormTrain:
                        depot.AddTrain();
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
                    Console.WriteLine($"Направление: {train.Direction}, всего мест в поезде: {train.MaxPlaces}, свободно мест: {train.FreePlace}, занято мест: {train.OccupiedPlace}, вагонов: {train.Vans}");
                }
            }
        }

        public void AddTrain()
        {
            string? direction = ReadDirection();
            int soldTickets = SellTickets();
            List<Van> vans = AddVagons(soldTickets);
            Train train = new(direction, soldTickets, vans);
            _trains.Add(train);
            Console.Clear();
            ShowDepot();
            train.ShowVans();
        }

        private string ReadDirection()
        {
            Console.WriteLine("Введите город отправления: ");
            string? arrive = Console.ReadLine();

            Console.WriteLine("Введите город назначения: ");
            string? departure = Console.ReadLine();

            string? direction = arrive + "-" + departure;
            return direction;
        }

        private int SellTickets()
        {
            Random random = new();
            int minNumber = 300;
            int maxNumber = 540;
            return random.Next(minNumber, maxNumber);
        }

        private List<Van> AddVagons(int soldTickets)
        {
            List<Van> availableVans = new() { new Van(1,"Спальный вагон", 18), new Van(2,"Купе", 36), new Van(3,"Плацкарт", 52) };
            List<Van> vans = new();
            Van sleeping = new(1,"Спальный вагон", 18);
            Van sitting = new(2,"Купе", 36);
            Van reserved = new(3,"Плацкарт", 52);

            while (soldTickets > 0)
            {
                Console.Clear();
                Console.WriteLine($"Количество пассажиров: {soldTickets}");
                Console.WriteLine("Выберите вагоны для комплектации: ");

                for(int i = 0; i < availableVans.Count; i++)
                {
                    Console.Write(i + 1 + " ");
                    Console.WriteLine($"{availableVans[i].Name}, мест: {availableVans[i].Places}");
                }

                int userInput = Convert.ToInt32(Console.ReadLine());

                if(userInput == availableVans[0].Id)
                {
                    vans.Add(sleeping);
                    soldTickets -= sleeping.Places;
                }
                else if(userInput == availableVans[1].Id)
                {
                    vans.Add(sitting);
                    soldTickets -= sitting.Places;
                }
                else if(userInput == availableVans[2].Id)
                {
                    vans.Add(reserved);
                    soldTickets -= reserved.Places;
                }
                else
                {
                    Console.WriteLine("Введите цифру нужного вагона от 1 до 3");
                    Console.ReadKey();
                }
            }

            return vans;
        }
    }

    class Train
    {
        private List<Van> _vans = new();

        public Train(string direction, int occupiedPlace, List<Van> vans)
        {
            Direction = direction;
            OccupiedPlace = occupiedPlace;
            _vans = vans;
            Vans = _vans.Count;
        }

        public Train()
        {
            Direction = "Владимир-Москва";
            OccupiedPlace = 0;
            _vans = new List<Van>();
        }

        public string Direction { get; private set; }
        public int OccupiedPlace { get; private set; }
        public int MaxPlaces { get; private set; } = 540;
        public int FreePlace => MaxPlaces - OccupiedPlace;
        public int Vans { get; private set; }

        public void ShowVans()
        {
            foreach (Van van in _vans)
            {
                Console.WriteLine($"{van.Name}, количество мест: {van.Places}");
            }
        }
    }

    class User
    {
        public User(string name = "Аноним")
        {
            Name = name;
            ReadName();
        }

        public string? Name { get; private set; }

        private void ReadName()
        {
            Console.WriteLine("Здравствуйте, как я могу к Вам обращаться?: ");
            Name = Console.ReadLine();
            Name ??= "Аноним";
        }
    }

    class Van
    {
        public Van(int id, string name, int places)
        {
            Id = id;
            Name = name;
            Places = places;
        }

        public int Id { get; private set; }
        public int Places { get; private set; }
        public string Name { get; private set; }
    }
}