namespace Ex03.GarageLogic
{
    public class EnergySource
    {
        public enum eEnergySource
        {
            GasTank = 1,
            Battery,
        }

        protected float m_CurrentAmountOfEnergy;

        public float CurrentAmountOfEnergy
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

            set
            {
                m_CurrentAmountOfEnergy = value;
            }
        }
    }
}
