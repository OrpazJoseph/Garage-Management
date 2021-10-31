using System;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        private eFuelType m_FuelType;
        private float m_CurrentLitersOfGas;
        private float m_MaxLitersOfGas;

        public Fuel(float i_CurrentLitersOfGas, float i_MaxLitersOfGas, eFuelType i_FuelType)
        {
            m_CurrentLitersOfGas = i_CurrentLitersOfGas;
            m_MaxLitersOfGas = i_MaxLitersOfGas;
            m_FuelType = i_FuelType;
            AddFuelPercentage();
        }

        public void AddFuel(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType.Equals(m_FuelType))
            {
                if (m_CurrentLitersOfGas + i_AmountToAdd > m_MaxLitersOfGas)
                {
                    throw new ValueOutOfRangeException(new Exception(), 0, m_MaxLitersOfGas);
                }

                m_CurrentLitersOfGas += i_AmountToAdd;
                AddFuelPercentage();
            }
            else
            {
                throw new Exception();
            }
        }

        // Display current fuel amount in percent
        public void AddFuelPercentage()
        {
            m_CurrentAmountOfEnergy = (float)Math.Round(m_CurrentLitersOfGas * 100.0f / m_MaxLitersOfGas);
        }
    }
}
