using System.Collections.Generic;
using GarageManager;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            A1,
            A2,
            B
        }

        public static readonly int SR_NumOfWheels = 2;
        public static readonly float SR_MaxAirPressure = 28;
        public static readonly float SR_MaxFuelCapacity = 5.5f;
        public static readonly float SR_MaxBatteryTime = 1.6f;
        public static readonly Fuel.eFuelType SR_FuelType = Fuel.eFuelType.Octan98;
        private static eVehicleType s_VehicleType;
        private static eLicenseType s_LicenseType;
        private static int s_EngineCapacity;

        public Motorcycle(
            string i_ModelName,
            string i_LicenseNumber,
            List<Wheel> i_Wheels,
            EnergySource i_EnergySource,
            eVehicleType i_VehicleType,
            eLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(i_ModelName, i_LicenseNumber, i_Wheels, i_EnergySource, i_VehicleType)
        {
            s_VehicleType = i_VehicleType;
            s_LicenseType = i_LicenseType;
            s_EngineCapacity = i_EngineCapacity;
        }

        public eLicenseType LicenseType
        {
            get
            {
                return s_LicenseType;
            }

            set
            {
                s_LicenseType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return s_EngineCapacity;
            }

            set
            {
                s_EngineCapacity = value;
            }
        }
    }
}
