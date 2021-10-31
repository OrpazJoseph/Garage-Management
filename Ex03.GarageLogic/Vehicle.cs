using System.Collections.Generic;
using GarageManager;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        public enum eVehicleType
        {
            Car = 1,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }

        protected eVehicleType m_VehicleType;
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected List<Wheel> m_Wheels;
        protected EnergySource m_EnergySource;

        public Vehicle(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheels, EnergySource i_EnergySource, eVehicleType i_VehicleType)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_Wheels = i_Wheels;
            m_EnergySource = i_EnergySource;
            m_VehicleType = i_VehicleType;
        }

        public eVehicleType VehicleType
        {
            get
            {
                return m_VehicleType;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }
    }
}
