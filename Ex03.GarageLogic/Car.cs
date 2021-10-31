using System.Collections.Generic;
using GarageManager;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Yellow = 1,
            White,
            Black,
            Blue
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public static readonly int SR_NumOfWheels = 4;
        public static readonly float SR_MaxAirPressure = 30;
        public static readonly float SR_MaxFuelCapacity = 50;
        public static readonly float SR_MaxBatteryTime = 2.8f;
        public static readonly Fuel.eFuelType SR_FuelType = Fuel.eFuelType.Octan95;

        private static eVehicleType s_VehicleType;
        private static eColor s_Color;
        private static eNumOfDoors s_NumOfDoors;

        public Car(
            string i_ModelName,
            string i_LicenseNumber,
            List<Wheel> i_Wheels,
            EnergySource i_EnergySource,
            eVehicleType i_VehicleType,
            eColor i_Color,
            eNumOfDoors i_NumOfDoors)
            : base(i_ModelName, i_LicenseNumber, i_Wheels, i_EnergySource, i_VehicleType)
        {
            s_VehicleType = i_VehicleType;
            s_Color = i_Color;
            s_NumOfDoors = i_NumOfDoors;
        }

        public eColor Color
        {
            get
            {
                return s_Color;
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return s_NumOfDoors;
            }
        }
    }
}
