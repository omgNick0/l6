using System;

namespace ShipsApp
{
    public abstract class ShipBase
    {
        protected double Length { get; set; }

        protected ShipBase(double length)
        {
            Length = length;
        }

        public double GetLength() => Length;
        
        public abstract void DisplayInfo();
        public abstract void Modify();
    }

    public class Ship : ShipBase
    {
        protected string Name { get; set; }
        protected int Crew { get; set; }

        public Ship(string name, double length, int crew) : base(length)
        {
            Name = name;
            Crew = crew;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Длина: {Length} метров");
            Console.WriteLine($"Экипаж: {Crew} человек");
        }

        public override void Modify()
        {
            // Будет реализовано через форму
        }

        public virtual string GetShipInfo()
        {
            return $"Название: {Name}\nДлина: {Length} метров\nЭкипаж: {Crew} человек";
        }
    }

    public class Steamship : Ship
    {
        public int EnginePower { get; set; }

        public Steamship(string name, double length, int crew, int enginePower) 
            : base(name, length, crew)
        {
            EnginePower = enginePower;
        }

        public override string GetShipInfo()
        {
            return base.GetShipInfo() + $"\nМощность двигателя: {EnginePower} л.с.";
        }
    }

    public class SailingShip : Ship
    {
        public int Sails { get; set; }

        public SailingShip(string name, double length, int crew, int sails) 
            : base(name, length, crew)
        {
            Sails = sails;
        }

        public override string GetShipInfo()
        {
            return base.GetShipInfo() + $"\nКоличество парусов: {Sails}";
        }
    }

    public class Yacht : Ship
    {
        public int Sails { get; set; }
        public int EnginePower { get; set; }
        public string LuxuryLevel { get; set; }

        public Yacht(string name, double length, int crew, int sails, int enginePower, string luxuryLevel) 
            : base(name, length, crew)
        {
            Sails = sails;
            EnginePower = enginePower;
            LuxuryLevel = luxuryLevel;
        }

        public override string GetShipInfo()
        {
            return base.GetShipInfo() + 
                   $"\nКоличество парусов: {Sails}" +
                   $"\nМощность двигателя: {EnginePower} л.с." +
                   $"\nУровень роскоши: {LuxuryLevel}";
        }
    }
}
