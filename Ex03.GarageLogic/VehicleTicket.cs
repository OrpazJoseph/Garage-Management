namespace Ex03.GarageLogic
{
    public class VehicleTicket
    {
        public enum eCarStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        private string m_OwnerName;
        private string m_OwnerPhone;
        private eCarStatus m_CarStatus;
        private Vehicle m_Vehicle;

        public VehicleTicket(string i_OwnerName, string i_OwnerPhone, eCarStatus i_CarStatus, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_CarStatus = i_CarStatus;
            m_Vehicle = i_Vehicle;
        }

        public eCarStatus CarStatus
        {
            get
            {
                return m_CarStatus;
            }

            set
            {
                m_CarStatus = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }
    }
}
