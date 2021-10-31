using System.Collections.Generic;
using GarageManager;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public static readonly int SR_NumOfWheels = 16;
        public static readonly float SR_MaxAirPressure = 26;
        public static readonly float SR_MaxFuelCapacity = 110;
        public static readonly Fuel.eFuelType SR_FuelType = Fuel.eFuelType.Soler;
        private static eVehicleType s_VehicleType;
        private static bool s_DangerousGoods;
        private static float s_CargoCapacity;

        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            List<Wheel> i_Wheels,
            EnergySource i_EnergySource,
            eVehicleType i_VehicleType,
            bool i_DangerousGoods,
            float i_CargoCapacity)
            : base(i_ModelName, i_LicenseNumber, i_Wheels, i_EnergySource, i_VehicleType)
        {
            s_VehicleType = i_VehicleType;
            s_DangerousGoods = i_DangerousGoods;
            s_CargoCapacity = i_CargoCapacity;
        }

        public bool DangerousGoods
        {
            get
            {
                return s_DangerousGoods;
            }

            set
            {
                s_DangerousGoods = value;
            }
        }

        public float CargoCapacity
        {
            get
            {
                return s_CargoCapacity;
            }

            set
            {
                s_CargoCapacity = value;
            }
        }
    }
}
