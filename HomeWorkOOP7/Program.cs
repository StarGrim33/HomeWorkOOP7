﻿namespace HomeWorkOOP7
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

                string userInput = Console.ReadLine();

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
            string? direction = SetDirection();
            int soldTickets = SellTickets();
            List<Van> vans = AddVagons(soldTickets);
            Train train = new(direction, soldTickets, vans);
            _trains.Add(train);
            Console.Clear();
            ShowDepot();
            train.ShowVans();
        }

        private static string SetDirection()
        {
            Console.WriteLine("Введите город отправления: ");
            string? arrive = Console.ReadLine();

            Console.WriteLine("Введите город назначения: ");
            string? departure = Console.ReadLine();

            string? direction = arrive + "-" + departure;
            return direction;
        }

        private static int SellTickets()
        {
            Random random = new();
            int minNumber = 300;
            int maxNumber = 540;
            int randomNumber = random.Next(minNumber, maxNumber);
            return randomNumber;
        }

        private static List<Van> AddVagons(int soldTickets)
        {
            const string CommandSleppingVagon = "1";
            const string CommandSittingVagon = "2";
            const string CommandReservedVagon = "3";

            List<Van> vans = new();
            Van sleeping = new("Спальный вагон", 18);
            Van sitting = new("Купе", 36);
            Van reserved = new("Плацкарт", 52);

            while(soldTickets > 0)
            {
                Console.Clear();
                Console.WriteLine($"Количество пассажиров: {soldTickets}");
                Console.WriteLine("Выберите вагоны для комплектации: ");
                Console.WriteLine($"{CommandSleppingVagon}-{sleeping.Name}, мест: {sleeping.Places}");
                Console.WriteLine($"{CommandSittingVagon}-{sitting.Name}, мест:{sitting.Places}");
                Console.WriteLine($"{CommandReservedVagon}-{reserved.Name} мест:{reserved.Places}");

                string? userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSleppingVagon:
                        vans.Add(sleeping);
                        soldTickets -= sleeping.Places;
                        break;

                    case CommandSittingVagon:
                        vans.Add(sitting);
                        soldTickets -= sitting.Places;
                        break;

                    case CommandReservedVagon:
                        vans.Add(reserved);
                        soldTickets -= reserved.Places;
                        break;
                }
            }

            return vans;
        }
    }

    class Train
    {
        public Train(string direction, int occupiedPlace, List<Van> vans)
        {
            Direction = direction;
            OccupiedPlace = occupiedPlace;
            _vans = vans;
            Vans = _vans.Count;
        }

        public string Direction { get; private set; }
        public int OccupiedPlace { get; private set; }
        public int MaxPlaces { get; private set; } = 540;
        public int FreePlace { get { return FreePlace = MaxPlaces - OccupiedPlace; } private set { } }
        public int Vans { get; private set; }

        private List<Van> _vans = new();

        public Train()
        {
            Direction = "Владимир-Москва";
            OccupiedPlace = 0;
            _vans = new List<Van>();
        }

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
            SetName();
        }

        public string? Name { get; private set; }

        public void SetName()
        {
            Console.WriteLine("Здравствуйте, как я могу к Вам обращаться?: ");
            Name = Console.ReadLine();

            Name ??= "Аноним";
        }
    }

    class Van
    {
        public Van(string name, int places)
        {
            Name = name;
            Places = places;
        }

        public int Places { get; private set; }
        public string Name { get; private set; }
    }
}