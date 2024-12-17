using System;

namespace ShipsApp
{
    public interface IVessel
    {
        double GetLength();
        string GetVesselInfo();
        void DisplayInfo();
    }

    public interface ISailable : IVessel
    {
        int GetSailsCount();
    }

    public interface IPowered : IVessel
    {
        int GetEnginePower();
    }

    public abstract class ShipBase : IVessel
    {
        protected double Length { get; set; }

        protected ShipBase(double length)
        {
            Length = length;
        }

        public double GetLength() => Length;
        
        public abstract void DisplayInfo();
        public abstract void Modify();
        public virtual string GetVesselInfo() => $"Длина: {Length} метров";
    }

    public class Ship : ShipBase
    {
        public string Name { get; set; }
        private int crew;
        public int Crew 
        { 
            get => crew;
            set
            {
                if (value >= Length)
                {
                    throw new ArgumentException($"Количество экипажа ({value} человек) не может быть больше или равно длине корабля ({Length} метров)");
                }
                int maxCrew = GetMaxCrewSize();
                if (value > maxCrew)
                {
                    throw new ArgumentException($"Превышено максимальное количество экипажа ({maxCrew} человек) для корабля длиной {Length} метров");
                }
                crew = value;
            }
        }

        protected virtual int GetMaxCrewSize()
        {
            int maxCrew;
            if (Length < 50) maxCrew = 30;
            else if (Length < 150) maxCrew = 100;
            else maxCrew = 200;  // Максимальное значение для любого корабля

            return maxCrew;
        }

        public Ship(string name, double length, int crew) : base(length)
        {
            Name = name;
            Crew = crew;  // Используем свойство для проверки ограничений
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Длина: {Length} метров");
            Console.WriteLine($"Экипаж: {Crew} человек");
        }

        public override void Modify()
        {
            // Реализовано через форму EditShipForm
        }

        public override string GetVesselInfo()
        {
            return $"Название: {Name}\nДлина: {Length} метров\nЭкипаж: {Crew} человек";
        }
    }

    public class Steamship : Ship, IPowered
    {
        private int enginePower;
        private const int MaxEnginePower = 100000;
        public int EnginePower 
        { 
            get => enginePower;
            set
            {
                int minPower = GetMinRequiredPower();
                if (value < minPower)
                {
                    throw new ArgumentException($"Мощность двигателя должна быть не менее {minPower} л.с. (Формула: длина * экипаж = {Length} * {Crew})");
                }
                if (value > MaxEnginePower)
                {
                    throw new ArgumentException($"Мощность двигателя не может превышать {MaxEnginePower} л.с.");
                }
                enginePower = value;
            }
        }

        protected virtual int GetMinRequiredPower()
        {
            return (int)(Length * Crew);
        }

        public Steamship(string name, double length, int crew, int enginePower) 
            : base(name, length, crew)
        {
            EnginePower = enginePower;
        }

        public int GetEnginePower() => EnginePower;

        public override string GetVesselInfo()
        {
            return base.GetVesselInfo() + $"\nМощность двигателя: {EnginePower} л.с.";
        }
    }

    public class SailingShip : Ship, ISailable
    {
        private int sails;
        public int Sails 
        { 
            get => sails;
            set
            {
                // Проверяем условие: количество_парусов^2 > количество_экипажа
                if (value * value <= Crew)
                {
                    throw new ArgumentException($"Количество парусов в квадрате ({value}^2 = {value * value}) должно быть больше количества экипажа ({Crew})");
                }
                sails = value;
            }
        }

        public SailingShip(string name, double length, int crew, int sails) 
            : base(name, length, crew)
        {
            Sails = sails;
        }

        public int GetSailsCount() => Sails;

        public override string GetVesselInfo()
        {
            return base.GetVesselInfo() + $"\nКоличество парусов: {Sails}";
        }
    }

    public class Yacht : Ship, ISailable, IPowered
    {
        public int Sails { get; set; }
        private int enginePower;
        private string luxuryLevel;
        private const int MaxEnginePower = 100000;

        public int EnginePower 
        { 
            get => enginePower;
            set
            {
                int minPower = GetMinRequiredPower();
                if (value < minPower)
                {
                    throw new ArgumentException($"Мощность двигателя должна быть не менее {minPower} л.с. (Формула: длина * экипаж + паруса = {Length} * {Crew} + {Sails})");
                }
                if (value > MaxEnginePower)
                {
                    throw new ArgumentException($"Мощность двигателя не может превышать {MaxEnginePower} л.с.");
                }
                enginePower = value;
            }
        }

        public string LuxuryLevel 
        { 
            get => luxuryLevel;
            set
            {
                if (!double.TryParse(value, out double level) || level < 1)
                {
                    throw new ArgumentException("Уровень роскоши должен быть числом не менее 1");
                }
                luxuryLevel = value;
            }
        }

        protected virtual int GetMinRequiredPower()
        {
            return (int)(Length * Crew + Sails);
        }

        public Yacht(string name, double length, int crew, int sails, int enginePower, string luxuryLevel) 
            : base(name, length, crew)
        {
            Sails = sails;
            EnginePower = enginePower;
            LuxuryLevel = luxuryLevel;
        }

        public int GetSailsCount() => Sails;
        public int GetEnginePower() => EnginePower;

        public override string GetVesselInfo()
        {
            return base.GetVesselInfo() + 
                   $"\nКоличество парусов: {Sails}" +
                   $"\nМощность двигателя: {EnginePower} л.с." +
                   $"\nУровень роскоши: {LuxuryLevel}";
        }
    }
}
