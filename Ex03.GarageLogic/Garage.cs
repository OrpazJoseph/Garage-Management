using System;
using System.Collections.Generic;
using GarageManager;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<VehicleTicket> r_carTickets;

        public Garage()
        {
            r_carTickets = new List<VehicleTicket>();
        }

        public void EnterVehicleToGarage(string i_OwnerName, string i_OwnerPhone, AllVehicleInfo i_AllVehicleInfo)
        {
            VehicleTicket existTicket;

            try
            {
                existTicket = FindTicketByLicense(i_AllVehicleInfo.m_LicenseNumber);
            }
            catch (KeyNotFoundException)
            {
                existTicket = null;
            }

            if (existTicket == null)
            {
                Vehicle newVehicle = EnterVehicle.EnterNewVehicle(i_AllVehicleInfo);
                VehicleTicket newTicket = new VehicleTicket(i_OwnerName, i_OwnerPhone, VehicleTicket.eCarStatus.InRepair, newVehicle);
                r_carTickets.Add(newTicket);
            }
            else
            {
                existTicket.CarStatus = VehicleTicket.eCarStatus.InRepair;
                throw new Exception(existTicket.Vehicle.LicenseNumber + "already in the garage.");
            }
        }

        public VehicleTicket FindTicketByLicense(string i_LicenseNumber)
        {
            VehicleTicket ticket = r_carTickets.Find(i_Exist => i_Exist.Vehicle.LicenseNumber.Equals(i_LicenseNumber));
            if (ticket == null)
            {
                throw new KeyNotFoundException(new Exception() + "Key not found!");
            }

            return ticket;
        }

        public Vehicle FindVehicleByLicense(string i_LicenseNumber)
        {
            return FindTicketByLicense(i_LicenseNumber).Vehicle;
        }

        public List<string> GetAllLicenseNumber()
        {
            List<string> licenseNumbers = new List<string>();

            foreach (VehicleTicket ticket in r_carTickets)
            {
                licenseNumbers.Add(ticket.Vehicle.LicenseNumber);
            }

            return licenseNumbers;
        }

        public List<string> LicenseNumberByStatus(VehicleTicket.eCarStatus i_CarStatus)
        {
            List<string> licenseNumByStatus = new List<string>();

            foreach (VehicleTicket ticket in r_carTickets)
            {
                if (ticket.CarStatus.Equals(i_CarStatus))
                {
                    licenseNumByStatus.Add(ticket.Vehicle.LicenseNumber);
                }
            }

            return licenseNumByStatus;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleTicket.eCarStatus i_NewCarStatus)
        {
            FindTicketByLicense(i_LicenseNumber).CarStatus = i_NewCarStatus;
        }

        public void InflateVehicleWheelsToMax(string i_LicenseNumber)
        {
            foreach (Wheel wheel in FindTicketByLicense(i_LicenseNumber).Vehicle.Wheels)
            {
                wheel.CurrentAirPressure = wheel.MaxManufacturerAirPressure;
            }
        }

        public void FuelVehicleWithGas(string i_LicenseNumber, Fuel.eFuelType i_FuelType, float i_FuelToAdd)
        {
            Fuel fuel = null;
            Vehicle vehicle = FindVehicleByLicense(i_LicenseNumber);
            fuel = vehicle.EnergySource as Fuel;

            if (fuel == null)
            {
                throw new ArgumentNullException();
            }

            fuel.AddFuel(i_FuelToAdd, i_FuelType);
        }

        public void ChargeVehicleBattery(string i_LicenseNumber, float i_MinutesToAdd)
        {
            Battery battery = null;
            Vehicle vehicle = FindVehicleByLicense(i_LicenseNumber);
            battery = vehicle.EnergySource as Battery;

            if (battery == null)
            {
                throw new ArgumentNullException();
            }

            // Divide by 60 to add hours
            battery.ChargeBattery(i_MinutesToAdd / 60f);
        }

        public string CarInfoByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicle = FindVehicleByLicense(i_LicenseNumber);
            VehicleTicket vehicleTicket = FindTicketByLicense(i_LicenseNumber);

            string result = string.Format(
                $"Car full information:\nLicense number: {i_LicenseNumber}\nModel name: {vehicle.ModelName}\n"
                + $"Owner name: {vehicleTicket.OwnerName}\nCar Status: {vehicleTicket.CarStatus}\n"
                + $"Wheels info: \nAir pressure: {vehicle.Wheels[0].CurrentAirPressure}\n"
                + $"Manufacturer name: {vehicle.Wheels[0].ManufacturerName}");
            return result;
        }
    }
}