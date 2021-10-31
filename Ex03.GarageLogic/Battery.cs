using System;

namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        private readonly float r_MaxHoursOfBattery;
        private float m_RemainingHoursOfBattery;

        public Battery(float i_RemainingHoursOfBattery, float i_MaxHoursOfBattery)
        {
            m_RemainingHoursOfBattery = i_RemainingHoursOfBattery;
            r_MaxHoursOfBattery = i_MaxHoursOfBattery;
            ChargeBatteryPercentage();
        }

        public void ChargeBattery(float i_HoursToAdd)
        {
            if (m_RemainingHoursOfBattery + i_HoursToAdd > r_MaxHoursOfBattery)
            {
                throw new ValueOutOfRangeException(new Exception(), 0, r_MaxHoursOfBattery);
            }

            m_RemainingHoursOfBattery += i_HoursToAdd;
            ChargeBatteryPercentage();
        }

        // Display current energy in percent
        public void ChargeBatteryPercentage()
        {
            m_CurrentAmountOfEnergy = (float)Math.Round(m_RemainingHoursOfBattery * 100.0f / r_MaxHoursOfBattery);
        }
    }
}
