using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.DDEACommon;
using UDM.Monitor;


namespace UDM.DDEA
{
    //jjk, 20.02.10 - OPC, Modbus Converter 클래스 
    //통신 설정에서 OPC와 Modbus 정보를 가져오기위하여 생성
    public class CIotTypeConverter :ICloneable
    {
        #region Member Variables

        private EMCommunicationCategory m_sCategory = EMCommunicationCategory.None;
        private int m_iSeriesSeletedIndex = -1;
        private COpcCategoryDescriptor m_cOpcCategory = null;
        private CModbusCategoryDescriptor m_cModBusCategory = null;
       
        private string m_sMaker = string.Empty;
        private string m_sSeries = string.Empty;
        private string m_sDevice = string.Empty;
        private string m_sChannel = string.Empty;
        private string m_sName = string.Empty;
        private string m_sDescription = string.Empty;
        private string m_sOpcSever = string.Empty;
        private string m_sOpcChannel = string.Empty;


        #endregion

        #region Initialize/Dispose

        public CIotTypeConverter()
        {
            m_cOpcCategory = new COpcCategoryDescriptor();
            m_cModBusCategory = new CModbusCategoryDescriptor();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Public Properties
        public EMCommunicationCategory Category
        {
            get { return m_sCategory; }
            set { m_sCategory = value; }
        }

        public string Maker
        {
            get { return m_sMaker; }
            set { m_sMaker = value; }
        }

        public string Series
        {
            get { return m_sSeries; }
            set { m_sSeries = value; }
        }

        public string Device
        {
            get { return m_sDevice; }
            set { m_sDevice = value; }
        }

        public string Channel
        {
            get { return m_sChannel; }
            set { m_sChannel = value; }
        }

        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }

        public string OpcServer
        {
            get { return m_sOpcSever; }
            set { m_sOpcSever = value; }
        }

        public string OpcClient
        {
            get { return m_sOpcChannel; }
            set { m_sOpcChannel = value; }
        }

        #endregion

        #region Private Methods


        #endregion

        #region Public Methods

        #region opc

        public List<IMakerDescriptor> GetOpcMakerList()
        {
            List<IMakerDescriptor> lstMakerS = new List<IMakerDescriptor>();
            IMakerDescriptor[] arrMakerItem = m_cOpcCategory.GetMakerDescriptors();

            for (int index = 0; index < arrMakerItem.Length; index++)
            {
                lstMakerS.Add(arrMakerItem[index]);
            }

            return lstMakerS;
        }

        public List<ISeriesDescriptor> GetOpcSeiresList(int iSeleted)
        {
            if (iSeleted == -1)
                return null;
            List<ISeriesDescriptor> lstSeriesS = new List<ISeriesDescriptor>();
            IMakerDescriptor[] arrMakerItem = m_cOpcCategory.GetMakerDescriptors();
            ISeriesDescriptor[] arrSeriesItem = arrMakerItem[iSeleted].GetSeriesDescriptors();

            for (int index = 0; index < arrSeriesItem.Length; index++)
            {
                lstSeriesS.Add(arrSeriesItem[index]);
            }

            m_iSeriesSeletedIndex = iSeleted;

            return lstSeriesS;
        }

        public List<IDevice> GetOpcDeviceList(int iSeleted)
        {
            if (iSeleted == -1)
                return null;

            List<IDevice> lstDevicesS = new List<IDevice>();

            IMakerDescriptor[] arrMakerItem = m_cOpcCategory.GetMakerDescriptors();
            ISeriesDescriptor[] arrSeriesItem = arrMakerItem[m_iSeriesSeletedIndex].GetSeriesDescriptors();
            IDevice[] arrDeviceItem = arrSeriesItem[iSeleted].GetDevices();
            for (int index = 0; index < arrSeriesItem.Length; index++)
            {
                lstDevicesS.Add(arrDeviceItem[index]);
            }

            return lstDevicesS;
        }

        public List<IChannel> GetChannelList(int iSeleted)
        {
            if (iSeleted == -1)
                return null;

            List<IChannel> lstChannelS = new List<IChannel>();

            IMakerDescriptor[] arrMakerItem = m_cOpcCategory.GetMakerDescriptors();
            ISeriesDescriptor[] arrSeriesItem = arrMakerItem[m_iSeriesSeletedIndex].GetSeriesDescriptors();
            IChannel[] arrChannelItem = arrSeriesItem[iSeleted].GetChannels();

            for (int index = 0; index < arrChannelItem.Length; index++)
            {
                lstChannelS.Add(arrChannelItem[index]);
            }

            return lstChannelS;
        }

        public List<string> GetOpcServerList()
        {
            COpcBrowser cBrowser = new COpcBrowser();
            List<string> lstServer = cBrowser.GetServerList();
            cBrowser.Dispose();
            cBrowser = null;

            return lstServer;
        }

        public List<string> GetOpcChannelList(string sOpcServer)
        {
            List<string> lstOpcChannel = new List<string>();

            if (sOpcServer == null || sOpcServer == string.Empty)
                return null;

            COpcBrowser cBrowser = new COpcBrowser();
            bool bOK = cBrowser.Connect(sOpcServer);
            if (bOK)
            {
                cBrowser.ShowBranches();
                int iChannelCount = cBrowser.NodeCount;

                string sChannel;
                string sDevice;
                for (int i = 0; i < iChannelCount; i++)
                {
                    sChannel = cBrowser.GetNodeName(i);
                    cBrowser.MoveDown(sChannel);
                    bOK = cBrowser.ShowBranches();
                    int iDeviceCount = cBrowser.NodeCount;

                    if (sOpcServer == "Takebishi.Dxp.5")
                    {
                        lstOpcChannel.Add(sChannel);
                    }
                    else
                    {
                        for (int j = 0; j < iDeviceCount; j++)
                        {
                            sDevice = cBrowser.GetNodeName(j);
                            lstOpcChannel.Add(sChannel + "." + sDevice);
                        }
                    }

                    cBrowser.MoveUp();
                    cBrowser.ShowBranches();
                }

                cBrowser.Disconnect();
            }

            cBrowser.Dispose();
            cBrowser = null;

            return lstOpcChannel;
        }
        #endregion

        #region ModBus

        public List<IMakerDescriptor> GetModBusMakerList()
        {
            List<IMakerDescriptor> lstMakerS = new List<IMakerDescriptor>();
            IMakerDescriptor[] arrMakerItem = m_cModBusCategory.GetMakerDescriptors();

            for (int index = 0; index < arrMakerItem.Length; index++)
            {
                lstMakerS.Add(arrMakerItem[index]);
            }

            return lstMakerS;
        }

        public List<ISeriesDescriptor> GetModBusSeiresList(int iSeleted)
        {
            if (iSeleted == -1)
                return null;
            List<ISeriesDescriptor> lstSeriesS = new List<ISeriesDescriptor>();
            IMakerDescriptor[] arrMakerItem = m_cModBusCategory.GetMakerDescriptors();
            ISeriesDescriptor[] arrSeriesItem = arrMakerItem[iSeleted].GetSeriesDescriptors();

            for (int index = 0; index < arrSeriesItem.Length; index++)
            {
                lstSeriesS.Add(arrSeriesItem[index]);
            }

            m_iSeriesSeletedIndex = iSeleted;

            return lstSeriesS;
        }

        public List<IDevice> GetModBusDeviceList(int iSeleted)
        {
            if (iSeleted == -1)
                return null;

            List<IDevice> lstDevicesS = new List<IDevice>();

            IMakerDescriptor[] arrMakerItem = m_cModBusCategory.GetMakerDescriptors();
            ISeriesDescriptor[] arrSeriesItem = arrMakerItem[m_iSeriesSeletedIndex].GetSeriesDescriptors();
            IDevice[] arrDeviceItem = arrSeriesItem[iSeleted].GetDevices();
            for (int index = 0; index < arrSeriesItem.Length; index++)
            {
                lstDevicesS.Add(arrDeviceItem[index]);
            }

            return lstDevicesS;
        }

        public List<IChannel> GetModBusChannelList(int iSeleted)
        {
            if (iSeleted == -1)
                return null;

            List<IChannel> lstChannelS = new List<IChannel>();

            IMakerDescriptor[] arrMakerItem = m_cModBusCategory.GetMakerDescriptors();
            ISeriesDescriptor[] arrSeriesItem = arrMakerItem[m_iSeriesSeletedIndex].GetSeriesDescriptors();
            IChannel[] arrChannelItem = arrSeriesItem[iSeleted].GetChannels();

            for (int index = 0; index < arrChannelItem.Length; index++)
            {
                lstChannelS.Add(arrChannelItem[index]);
            }

            return lstChannelS;
        }

        #endregion

        #endregion

        #region Event Methods

        #region Event Source


        #endregion

        #region Event Sink


        #endregion

        #endregion
    }
}
