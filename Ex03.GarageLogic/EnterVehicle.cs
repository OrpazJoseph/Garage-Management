using System.Collections.Generic;
using GarageManager;

namespace Ex03.GarageLogic
{
    public class EnterVehicle
    {
        public enum eVehicleType
        {
            Car = 1,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static Vehicle EnterNewVehicle(AllVehicleInfo i_AllVehicleInfo)
        {
            Vehicle vehicle = null;
            List<Wheel> wheels = setNumOfWheels(i_AllVehicleInfo);
            EnergySource energySource = setEnergySource(i_AllVehicleInfo);
            eVehicleType carType = i_AllVehicleInfo.m_VehicleType;

            if (carType == eVehicleType.Car || carType == eVehicleType.ElectricCar)
            {
                vehicle = new Car(
                    i_AllVehicleInfo.m_ModelName,
                    i_AllVehicleInfo.m_LicenseNumber,
                    wheels,
                    energySource,
                    (Vehicle.eVehicleType)carType,
                    i_AllVehicleInfo.m_Color,
                    i_AllVehicleInfo.m_Doors);
            }
            else if (carType == eVehicleType.Motorcycle || carType == eVehicleType.ElectricMotorcycle)
            {
                vehicle = new Motorcycle(
                    i_AllVehicleInfo.m_ModelName,
                    i_AllVehicleInfo.m_LicenseNumber,
                    wheels,
                    energySource,
                    (Vehicle.eVehicleType)carType,
                    i_AllVehicleInfo.m_LicenseType,
                    i_AllVehicleInfo.m_EngineCapacity);
            }
            else if (carType == eVehicleType.Truck)
            {
                vehicle = new Truck(
                    i_AllVehicleInfo.m_ModelName,
                    i_AllVehicleInfo.m_LicenseNumber,
                    wheels,
                    energySource,
                    (Vehicle.eVehicleType)carType,
                    i_AllVehicleInfo.m_DangerousGoods,
                    i_AllVehicleInfo.m_CargoCapacity);
            }

            return vehicle;
        }

        private static List<Wheel> setNumOfWheels(AllVehicleInfo i_AllVehicleInfo)
        {
            List<Wheel> wheelsList = new List<Wheel>();
            int numberOfWheels = 0;
            float airPressure = 0;

            eVehicleType carType = i_AllVehicleInfo.m_VehicleType;

            if (carType == eVehicleType.Car || carType == eVehicleType.ElectricCar)
            {
                numberOfWheels = Car.SR_NumOfWheels;
                airPressure = Car.SR_MaxAirPressure;
            }

            if (carType == eVehicleType.Motorcycle || carType == eVehicleType.ElectricMotorcycle)
            {
                numberOfWheels = Motorcycle.SR_NumOfWheels;
                airPressure = Motorcycle.SR_MaxAirPressure;
            }

            if (carType == eVehicleType.Truck)
            {
                numberOfWheels = Truck.SR_NumOfWheels;
                airPressure = Truck.SR_MaxAirPressure;
            }

            for (int i = 0; i < numberOfWheels; i++)
            {
                Wheel wheel = new Wheel(
                    i_AllVehicleInfo.m_WheelsManufacturerName,
                    i_AllVehicleInfo.m_CurrentAirPressure,
                    airPressure);
                wheelsList.Add(wheel);
            }

            return wheelsList;
        }

        private static EnergySource setEnergySource(AllVehicleInfo i_AllVehicleInfo)
        {
            float maxAmountOfFuel = 0;
            float maxHoursOfBattery = 0;
            EnergySource energySource = null;
            Fuel.eFuelType fuelType = 0;

            eVehicleType carType = i_AllVehicleInfo.m_VehicleType;
            if (carType == eVehicleType.Car || carType == eVehicleType.Motorcycle || carType == eVehicleType.Truck)
            {
                if (carType == eVehicleType.Car)
                {
                    fuelType = Car.SR_FuelType;
                    maxAmountOfFuel = Car.SR_MaxFuelCapacity;
                }

                if (carType == eVehicleType.Motorcycle)
                {
                    fuelType = Motorcycle.SR_FuelType;
                    maxAmountOfFuel = Motorcycle.SR_MaxFuelCapacity;
                }

                if (carType == eVehicleType.Truck)
                {
                    fuelType = Truck.SR_FuelType;
                    maxAmountOfFuel = Truck.SR_MaxFuelCapacity;
                }

                energySource = new Fuel(i_AllVehicleInfo.m_CurrentLitersOfGas, maxAmountOfFuel, fuelType);
            }
            else if (carType == eVehicleType.ElectricCar || carType == eVehicleType.ElectricMotorcycle)
            {
                if (carType == eVehicleType.ElectricCar)
                {
                    maxHoursOfBattery = Car.SR_MaxBatteryTime;
                }

                if (carType == eVehicleType.ElectricMotorcycle)
                {
                    maxHoursOfBattery = Motorcycle.SR_MaxBatteryTime;
                }

                energySource = new Battery(i_AllVehicleInfo.m_RemainingHoursOfBattery, maxHoursOfBattery);
            }

            return energySource;
        }
    }
}
