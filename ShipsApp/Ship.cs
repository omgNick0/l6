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
        public int Crew { get; set; }

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
            // Реализовано через форму EditShipForm
        }

        public override string GetVesselInfo()
        {
            return $"Название: {Name}\nДлина: {Length} метров\nЭкипаж: {Crew} человек";
        }
    }

    public class Steamship : Ship, IPowered
    {
        public int EnginePower { get; set; }

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
        public int Sails { get; set; }

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
        public int EnginePower { get; set; }
        public string LuxuryLevel { get; set; }

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
