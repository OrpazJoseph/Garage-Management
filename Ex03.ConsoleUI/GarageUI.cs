using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private const int k_Options = 8;

        // Default messages for errors
        private const string k_GeneralErrorMsg = "Unknown error.";
        private static readonly string sr_RangeErrorMsg = $"Input should be from 1 - {k_Options} please try again.";
        private static readonly Garage sr_MyGarage = new Garage();

        public static void UI()
        {
            int userInput;

            while (true)
            {
                showOption(out userInput);
                try
                {
                    switch (userInput)
                    {
                        case 1:
                            addNewVehicleToGarage(sr_MyGarage);
                            break;
                        case 2:
                            showLicenseNumbersByStatus(sr_MyGarage);
                            break;
                        case 3:
                            changeVehicleStatus(sr_MyGarage);
                            break;
                        case 4:
                            inflateVehicleToMax(sr_MyGarage);
                            break;
                        case 5:
                            fuelVehicle(sr_MyGarage);
                            break;
                        case 6:
                            chargeVehicle(sr_MyGarage);
                            break;
                        case 7:
                            showVehicleDataByLicenseNumber(sr_MyGarage);
                            break;
                        case 8:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(sr_RangeErrorMsg);
                            break;
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.InnerException);
                }

                Console.WriteLine("Press 'Enter' to continue.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void showOption(out int io_Choice)
        {
            {
                io_Choice = 0;

                string menuOptions = string.Format(
@"Hello and welcome to Garage Manager System!
Please enter your choice:
1). Enter a new vehicle to the garage.
2). Display all license numbers in the garage.
3). Change vehicle status.
4). Inflate wheels to max.
5). Fuel gas tank.
6). Charge battery.
7). Display full information of vehicle.
8). Exit
");
                Console.WriteLine(menuOptions);
                while (!int.TryParse(Console.ReadLine(), out io_Choice) || io_Choice < 1 || io_Choice > k_Options)
                {
                    Console.WriteLine(sr_RangeErrorMsg);
                }
            }
        }

        private static void addNewVehicleToGarage(Garage i_Garage)
        {
            string ownerNameInput = string.Empty;
            string ownerPhoneNumberInput = string.Empty;

            getOwnerNameFromUser(out ownerNameInput);
            getOwnerPhoneNumberFromUser(out ownerPhoneNumberInput);

            // Exception is being thrown if license already exists in the garage.
            AllVehicleInfo vehicleData = getAllUserData();
            i_Garage.EnterVehicleToGarage(ownerNameInput, ownerPhoneNumberInput, vehicleData);
            Console.WriteLine("Car has been added successfully to the garage" + Environment.NewLine);
        }

        private static void getOwnerNameFromUser(out string o_OwnerName)
        {
            string userName = string.Empty;
            bool validInput = false;

            Console.WriteLine("Enter your name: ");
            while (!validInput)
            {
                try
                {
                    validInput = checkUserName(out userName);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("The name can't be empty");
                }
                catch (FormatException)
                {
                    Console.WriteLine("The name should contain only letters and spaces");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_OwnerName = userName;
        }

        private static bool checkUserName(out string o_UserInput)
        {
            string userInputCheck = string.Empty;

            userInputCheck = Console.ReadLine();
            if (string.IsNullOrEmpty(userInputCheck))
            {
                throw new NullReferenceException();
            }

            // Name can only contain letters and spaces
            foreach (char c in userInputCheck)
            {
                if (!(c == ' ' || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')))
                {
                    throw new FormatException();
                }
            }

            o_UserInput = userInputCheck;
            return true;
        }

        private static void getOwnerPhoneNumberFromUser(out string o_OwnerPhoneNumber)
        {
            string userPhone = string.Empty;
            bool validInput = false;

            Console.WriteLine("Please enter your phone number: ");
            while (!validInput)
            {
                try
                {
                    validInput = getValidPhoneNumberFromUser(out userPhone);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("The phone number can't be empty");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Only digits allowed");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_OwnerPhoneNumber = userPhone;
        }

        private static bool getValidPhoneNumberFromUser(out string o_PhoneNumber)
        {
            string userInputCheck = string.Empty;

            userInputCheck = Console.ReadLine();
            if (string.IsNullOrEmpty(userInputCheck))
            {
                throw new NullReferenceException();
            }

            foreach (char c in userInputCheck)
            {
                if (c < '0' || c > '9')
                {
                    throw new FormatException();
                }
            }

            o_PhoneNumber = userInputCheck;
            return true;
        }

        private static void showLicenseNumbersByStatus(Garage i_Garage)
        {
            VehicleTicket.eCarStatus vehicleStatus = 0;
            List<string> licenseNumbersList;
            StringBuilder buildLicenseNumber = new StringBuilder();
            int userInput;
            bool validInput = false;

            Console.WriteLine("1. Show all license numbers in the garage.\n2. Show all license numbers by status.");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 2);
                    if (userInput == 1)
                    {
                        licenseNumbersList = i_Garage.GetAllLicenseNumber();
                        foreach (string licenseNumber in licenseNumbersList)
                        {
                            buildLicenseNumber.Append(licenseNumber + Environment.NewLine);
                        }
                    }
                    else
                    {
                        getVehicleStatusFromUser(out vehicleStatus);
                        licenseNumbersList = i_Garage.LicenseNumberByStatus(vehicleStatus);
                    }

                    if (licenseNumbersList.Count == 0 && userInput == 2)
                    {
                        Console.WriteLine($"Vehicle with status {vehicleStatus} not found.");
                    }
                    else if (userInput == 2)
                    {
                        Console.WriteLine($"Vehicles with status {vehicleStatus} in garage:");
                        foreach (string licenseNumber in licenseNumbersList)
                        {
                            buildLicenseNumber.Append(licenseNumber + Environment.NewLine);
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            Console.WriteLine(buildLicenseNumber);
        }

        private static void changeVehicleStatus(Garage i_Garage)
        {
            string licenseNumber;
            VehicleTicket.eCarStatus newVehicleStatus;

            try
            {
                getVehicleLicenseNumberFromUser(out licenseNumber);
                getVehicleStatusFromUser(out newVehicleStatus);
                i_Garage.ChangeVehicleStatus(licenseNumber, newVehicleStatus);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Vehicle not found, please try again.");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Input can not be empty, please try again.");
            }
        }

        private static void inflateVehicleToMax(Garage i_Garage)
        {
            string licenseNumber = string.Empty;

            try
            {
                getVehicleLicenseNumberFromUser(out licenseNumber);
                i_Garage.InflateVehicleWheelsToMax(licenseNumber);
                Console.WriteLine("Vehicles wheels inflated to max successfully");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Vehicle not found, please try again.");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Input can not be empty, please try again.");
            }
        }

        private static void fuelVehicle(Garage i_Garage)
        {
            string licenseNumber;
            Fuel.eFuelType fuelType;
            float fuelToAdd;

            try
            {
                getVehicleLicenseNumberFromUser(out licenseNumber);
                getFuelTypeFromUser(out fuelType);
                getFuelToAddFromUser(out fuelToAdd);
                i_Garage.FuelVehicleWithGas(licenseNumber, fuelType, fuelToAdd);
                Console.WriteLine("Vehicle fueled successfully");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Vehicle not found, please try again.");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Input can not be empty, please try again.");
            }
            catch (ArgumentNullException)
            {   // electric vehicle
                Console.WriteLine("The vehicle has no fuel tank");
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("Cannot fuel vehicle over the maximum fuel capacity of " + ex.MaxValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong type of fuel.");
            }
        }

        private static void chargeVehicle(Garage i_Garage)
        {
            string licenseNumber;
            float batteryTimeToAdd;

            try
            {
                getVehicleLicenseNumberFromUser(out licenseNumber);
                getBatteryTimeToAddFromUser(out batteryTimeToAdd);
                i_Garage.ChargeVehicleBattery(licenseNumber, batteryTimeToAdd);
                Console.WriteLine("Vehicle charged successfully");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Vehicle not found, please try again.");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Input can not be empty, please try again.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The vehicle has no Battery");
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("Cannot charge battery over " + ex.MaxValue);
            }
        }

        private static void getBatteryTimeToAddFromUser(out float o_TimeToAdd)
        {
            float timeInput = 0;
            bool validInput = false;

            Console.WriteLine("How much time you want to add to the battery (hours):");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserFloatInput(out timeInput, 0, float.MaxValue);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please enter a floating point number");
                }
                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Error: Please enter a positive number");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error occurred: " + Environment.NewLine + ex.Message + Environment.NewLine);
                }
            }

            o_TimeToAdd = timeInput;
        }

        // First prints all general data of the vehicle
        // Than prints specific data for each vehicle type
        private static void showVehicleDataByLicenseNumber(Garage i_Garage)
        {
            string licenseNumber = string.Empty;

            try
            {
                getVehicleLicenseNumberFromUser(out licenseNumber);
                Console.WriteLine(i_Garage.CarInfoByLicenseNumber(licenseNumber));
                Vehicle vehicleToShow = i_Garage.FindVehicleByLicense(licenseNumber);

                if (vehicleToShow.VehicleType == Vehicle.eVehicleType.Car)
                {
                    string carDetails = string.Format(
                        $"Energy source: {EnergySource.eEnergySource.GasTank}\nCurrent amount of gas: {vehicleToShow.EnergySource.CurrentAmountOfEnergy}%"
                        + $"\nNumber of doors: {((Car)vehicleToShow).NumOfDoors}\nCar color: {((Car)vehicleToShow).Color}");
                    Console.WriteLine(carDetails);
                }

                if (vehicleToShow.VehicleType == Vehicle.eVehicleType.ElectricCar)
                {
                    string electricCarDetails = string.Format(
                        $"Energy source: {EnergySource.eEnergySource.Battery}\nCurrent amount of gas: {vehicleToShow.EnergySource.CurrentAmountOfEnergy}%"
                        + $"\nNumber of doors: {((Car)vehicleToShow).NumOfDoors}\nCar color: {((Car)vehicleToShow).Color}");
                    Console.WriteLine(electricCarDetails);
                }

                if (vehicleToShow.VehicleType == Vehicle.eVehicleType.ElectricMotorcycle)
                {
                    string electricCarDetails = string.Format(
                        $"Energy source: {EnergySource.eEnergySource.Battery}\nCurrent amount of gas: {vehicleToShow.EnergySource.CurrentAmountOfEnergy}%"
                        + $"\nEngine capacity: {((Motorcycle)vehicleToShow).EngineCapacity}\nLicense type: {((Motorcycle)vehicleToShow).LicenseType}");
                    Console.WriteLine(electricCarDetails);
                }

                if (vehicleToShow.VehicleType == Vehicle.eVehicleType.Motorcycle)
                {
                    string carDetails = string.Format(
                        $"Energy source: {EnergySource.eEnergySource.GasTank}\nCurrent amount of gas: {vehicleToShow.EnergySource.CurrentAmountOfEnergy}%"
                        + $"\nEngine capacity: {((Motorcycle)vehicleToShow).EngineCapacity}\nLicense type: {((Motorcycle)vehicleToShow).LicenseType}");
                    Console.WriteLine(carDetails);
                }

                if (vehicleToShow.VehicleType == Vehicle.eVehicleType.Truck)
                {
                    string dangerousGoods = ((Truck)vehicleToShow).DangerousGoods ? "Yes" : "No";

                    string carDetails = string.Format(
                        $"Energy source: {EnergySource.eEnergySource.GasTank}\nCurrent amount of gas: {vehicleToShow.EnergySource.CurrentAmountOfEnergy}%"
                        + $"\nCargo capacity: {((Truck)vehicleToShow).CargoCapacity}\nHas dangerous goods: {dangerousGoods}");
                    Console.WriteLine(carDetails);
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Vehicle not found, please try again.");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Input can not be empty, please try again.");
            }
        }

        private static void getFuelTypeFromUser(out Fuel.eFuelType o_FuelType)
        {
            bool validInput = false;
            int userInput = 0;

            Console.WriteLine("Please choose your vehicle's fuel type:\n1. Octan95\n2. Octan96\n3. Octan98\n4. Soler");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 4);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_FuelType = (Fuel.eFuelType)userInput;
        }

        private static void getFuelToAddFromUser(out float o_FuelToAdd)
        {
            float capacityInput = 0;
            bool validInput = false;

            Console.WriteLine("How much fuel you want to add: ");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserFloatInput(out capacityInput, 0, float.MaxValue);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_FuelToAdd = capacityInput;
        }

        private static void getVehicleStatusFromUser(out VehicleTicket.eCarStatus o_VehicleStatus)
        {
            int userInput = 0;
            bool validInput = false;

            Console.WriteLine("Please choose vehicle status:\n1. InRepair\n2. Repaired\n3. Paid");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 3);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_VehicleStatus = (VehicleTicket.eCarStatus)userInput;
        }

        private static AllVehicleInfo getAllUserData()
        {
            AllVehicleInfo o_VehicleData = new AllVehicleInfo();

            getVehicleLicenseNumberFromUser(out o_VehicleData.m_LicenseNumber);
            getVehicleTypeFromUser(out o_VehicleData.m_VehicleType);
            getVehicleModelNameFromUser(out o_VehicleData.m_ModelName);
            getVehicleProperties(o_VehicleData);
            getWheelsDataFromUser(o_VehicleData, out o_VehicleData.m_WheelsManufacturerName, out o_VehicleData.m_CurrentAirPressure);
            return o_VehicleData;
        }

        private static void getVehicleLicenseNumberFromUser(out string o_LicenseNumber)
        {
            string stringInput = string.Empty;

            Console.WriteLine("Please enter license number: ");
            while (true)
            {
                stringInput = Console.ReadLine();
                if (string.IsNullOrEmpty(stringInput))
                {
                    throw new NullReferenceException("The license number cannot be empty.");
                }

                if (stringInput.Length > 0)
                {
                    o_LicenseNumber = stringInput;
                    break;
                }

                throw new Exception(k_GeneralErrorMsg);
            }
        }

        private static void getVehicleTypeFromUser(out EnterVehicle.eVehicleType o_VehicleType)
        {
            int userInput = 0;
            bool validInput = false;

            Console.WriteLine($"Enter Vehicle type:\n1. Car\n2. Electric car\n3. Motorcycle\n4. Electric Motorcycle\n5. Truck");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 5);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_VehicleType = (EnterVehicle.eVehicleType)userInput;
        }

        private static bool validateUserIntInput(out int o_UserInput, int i_MinValue, int i_MaxValue)
        {
            if (int.TryParse(Console.ReadLine(), out o_UserInput) == false)
            {
                throw new FormatException();
            }

            if (o_UserInput < i_MinValue || o_UserInput > i_MaxValue)
            {
                throw new ValueOutOfRangeException(new Exception(), i_MinValue, i_MaxValue);
            }

            return true;
        }

        private static void getVehicleModelNameFromUser(out string o_ModelName)
        {
            string stringInput = string.Empty;

            Console.WriteLine("Enter vehicle model:");
            while (true)
            {
                stringInput = Console.ReadLine();
                if (string.IsNullOrEmpty(stringInput))
                {
                    throw new NullReferenceException("The license number cannot be empty.");
                }

                if (stringInput.Length > 0)
                {
                    o_ModelName = stringInput;
                    break;
                }

                throw new Exception(k_GeneralErrorMsg);
            }
        }

        private static void getVehicleProperties(AllVehicleInfo o_VehicleData)
        {
            if (o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.Car || o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.ElectricCar)
            {
                getVehicleColorFromUser(out o_VehicleData.m_Color);
                getVehicleNumberOfDoorsFromUser(out o_VehicleData.m_Doors);
                if (o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.Car)
                {
                    getFuelCurrentCapacityFromUser(out o_VehicleData.m_CurrentLitersOfGas, Car.SR_MaxFuelCapacity);
                }
                else
                {
                    getVehicleBatteryDataFromUser(out o_VehicleData.m_RemainingHoursOfBattery, Car.SR_MaxBatteryTime);
                }
            }

            if (o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.Motorcycle || o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.ElectricMotorcycle)
            {
                getVehicleLicenseTypeFromUser(out o_VehicleData.m_LicenseType);
                getVehicleEngineCapacityFromUser(out o_VehicleData.m_EngineCapacity);
                if (o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.Motorcycle)
                {
                    getFuelCurrentCapacityFromUser(out o_VehicleData.m_CurrentLitersOfGas, Motorcycle.SR_MaxFuelCapacity);
                }
                else
                {
                    getVehicleBatteryDataFromUser(out o_VehicleData.m_RemainingHoursOfBattery, Motorcycle.SR_MaxBatteryTime);
                }
            }

            if (o_VehicleData.m_VehicleType == EnterVehicle.eVehicleType.Truck)
            {
                getVehicleDangerousSubstancesDataFromUser(out o_VehicleData.m_DangerousGoods);
                getVehicleCargoVolumeFromUser(out o_VehicleData.m_CargoCapacity);
                getFuelCurrentCapacityFromUser(out o_VehicleData.m_CurrentLitersOfGas, Truck.SR_MaxFuelCapacity);
            }
        }

        private static void getVehicleColorFromUser(out Car.eColor o_VehicleColor)
        {
            int userInput = 0;
            bool validInput = false;
            Console.WriteLine("Enter vehicle color:\n1. Yellow\n2. White\n3. Black\n4. Blue\n");

            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 4);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_VehicleColor = (Car.eColor)userInput;
        }

        private static void getVehicleNumberOfDoorsFromUser(out Car.eNumOfDoors o_NumOfDoors)
        {
            int userInput = 0;
            bool validInput = false;

            Console.WriteLine("Enter number of doors:\n2. 2 Doors\n3. 3 Doors\n4. 4 Doors\n5. 5 doors\n");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 2, 5);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_NumOfDoors = (Car.eNumOfDoors)userInput;
        }

        private static void getFuelCurrentCapacityFromUser(out float o_CurrentFuelCapacity, float i_MaxFuelCapacity)
        {
            float capacityInput = 0;
            bool validInput = false;

            Console.WriteLine("Enter current vehicle's fuel capacity: ");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserFloatInput(out capacityInput, 0, i_MaxFuelCapacity);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between 0 - { ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_CurrentFuelCapacity = capacityInput;
        }

        private static bool validateUserFloatInput(out float o_UserInput, float i_MinValue, float i_MaxValue)
        {
            if (float.TryParse(Console.ReadLine(), out o_UserInput) == false)
            {
                throw new FormatException();
            }

            if (o_UserInput < i_MinValue || o_UserInput > i_MaxValue)
            {
                throw new ValueOutOfRangeException(new Exception(), i_MinValue, i_MaxValue);
            }

            return true;
        }

        private static void getVehicleBatteryDataFromUser(out float o_RemainingBatteryTime, float i_MaxBatteryTime)
        {
            float timeInput = 0;
            bool validInput = false;

            Console.WriteLine("Enter remaining vehicle's battery time (hours): ");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserFloatInput(out timeInput, 0, i_MaxBatteryTime);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between 0 - { ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_RemainingBatteryTime = timeInput;
        }

        private static void getVehicleLicenseTypeFromUser(out Motorcycle.eLicenseType o_LicenseType)
        {
            int userInput = 0;
            bool validInput = false;

            Console.WriteLine("Please choose your vehicle's license type:\n1. A type\n2. A1 type\n3. A2 type\n4. B type");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 4);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_LicenseType = (Motorcycle.eLicenseType)userInput;
        }

        private static void getVehicleEngineCapacityFromUser(out int o_EngineCapacity)
        {
            int capacityInput = 0;
            bool validInput = false;

            Console.WriteLine("Enter vehicle's engine capacity: ");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out capacityInput, 0, int.MaxValue);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_EngineCapacity = capacityInput;
        }

        private static void getVehicleDangerousSubstancesDataFromUser(out bool o_ContainsDangerousSubstances)
        {
            int userInput = 0;
            bool validInput = false;

            Console.WriteLine("Does your vehicle contain dangerous goods:\n1. Yes\n2. No");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserIntInput(out userInput, 1, 2);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please choose 1 for yes and 2 for no.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_ContainsDangerousSubstances = userInput == 1;
        }

        private static void getVehicleCargoVolumeFromUser(out float o_CargoVolume)
        {
            float cargoInput = 0;
            bool validInput = false;

            Console.WriteLine("Enter vehicle's cargo capacity: ");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserFloatInput(out cargoInput, 0, float.MaxValue);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_CargoVolume = cargoInput;
        }

        private static void getWheelsDataFromUser(AllVehicleInfo i_VehicleType, out string o_Manufacturer, out float o_CurrentAirPressure)
        {
            float pressureInput = 0;
            float maxAirPressure = 0;
            string stringInput = string.Empty;
            bool validInput = false;

            if (i_VehicleType.m_VehicleType == EnterVehicle.eVehicleType.Car || i_VehicleType.m_VehicleType == EnterVehicle.eVehicleType.ElectricCar)
            {
                maxAirPressure = Car.SR_MaxAirPressure;
            }

            if (i_VehicleType.m_VehicleType == EnterVehicle.eVehicleType.Motorcycle || i_VehicleType.m_VehicleType == EnterVehicle.eVehicleType.ElectricMotorcycle)
            {
                maxAirPressure = Motorcycle.SR_MaxAirPressure;
            }

            if (i_VehicleType.m_VehicleType == EnterVehicle.eVehicleType.Truck)
            {
                maxAirPressure = Truck.SR_MaxAirPressure;
            }

            Console.WriteLine("Enter wheels manufacturer name: ");
            while (true)
            {
                stringInput = Console.ReadLine();
                if (string.IsNullOrEmpty(stringInput))
                {
                    throw new NullReferenceException("The manufacturer name cannot be empty.");
                }

                if (stringInput.Length > 0)
                {
                    o_Manufacturer = stringInput;
                    break;
                }

                throw new Exception(k_GeneralErrorMsg);
            }

            Console.WriteLine("Enter current wheels pressure: ");
            while (!validInput)
            {
                try
                {
                    validInput = validateUserFloatInput(out pressureInput, 0, maxAirPressure);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be a number.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Input should be between {ex.MinValue} - {ex.MaxValue}");
                }
                catch (Exception)
                {
                    Console.WriteLine(k_GeneralErrorMsg);
                }
            }

            o_CurrentAirPressure = pressureInput;
        }
    }
}