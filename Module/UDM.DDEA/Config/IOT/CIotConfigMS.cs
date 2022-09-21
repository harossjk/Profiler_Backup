using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.Common;
using UDM.Monitor;

namespace UDM.DDEA
{
    //jjk, 20.02.11 - opc,mode 통신 설정 정보 serializeble 파일
    [Serializable]
    public class CIotConfigMS : IDisposable, ICloneable
    {
        #region Member Variables

        private CIotTypeConverter m_cIotTypeConverter = null;
        private CDevice m_cDevice = null;
        private CTag m_cTag = null;
        private CTagS m_cTagS = null;

        #endregion

        #region Initialize/Dispose
        public CIotConfigMS()
        {
            m_cIotTypeConverter = new CIotTypeConverter();
            m_cDevice = new CDevice();
            m_cTagS = new CTagS();
            m_cTag = new CTag();
        }

        public void Dispose()
        {

        }

        public object Clone()
        {
            CIotConfigMS config = new CIotConfigMS();
            config.TypeConverter = (CIotTypeConverter)TypeConverter.Clone();
            config.Device = (CDevice)Device.Clone();
            config.Tag = (CTag)Tag.Clone();
            config.TagS = (CTagS)TagS.Clone();

            return config;
        }

        #endregion

        #region Public Properties

        public CDevice Device
        {
            get { return m_cDevice; }
            set { m_cDevice = value; }
        }

        public CTag Tag
        {
            get { return m_cTag; }
            set { m_cTag = value; }
        }

        public CTagS TagS
        {
            get { return m_cTagS; }
            set { m_cTagS = value; }
        }

        public CIotTypeConverter TypeConverter
        {
            get { return m_cIotTypeConverter; }
            set { m_cIotTypeConverter = value; }
        }

        #endregion

        #region Private Methods



        #endregion

        #region Public Methods

        public bool AddItemS(List<CTag> lstTag)
        {
            
            if (lstTag == null || lstTag.Count == 0)
                return false;


            for (int index = 0; index < lstTag.Count; index++)
            {
                if (!m_cTagS.ContainsKey(lstTag[index].Key))
                {
                    m_cTagS.Add(lstTag[index].Key, lstTag[index]);
                }
            }

            return true;
        }

        public void RemoveItemS(string sConnectType)
        {
            //jjk, 20.02.25 - 만든곳이 connect 인 tag 지우기

            foreach (CTag ctag in m_cTagS.Values.ToList())
            {
                if (ctag.Creator == sConnectType)
                    m_cTagS.Remove(ctag.Key);
            }

        }

        #endregion

        #region Event Methods
        #region Event Source


        #endregion

        #region Event Sink


        #endregion
        #endregion
    }
}
