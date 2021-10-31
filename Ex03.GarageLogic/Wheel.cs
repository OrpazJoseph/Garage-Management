namespace GarageManager
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxManufacturerAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxManufacturerAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxManufacturerAirPressure = i_MaxManufacturerAirPressure;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxManufacturerAirPressure
        {
            get
            {
                return m_MaxManufacturerAirPressure;
            }

            set
            {
                m_MaxManufacturerAirPressure = value;
            }
        }
    }
}
