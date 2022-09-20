using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDMProfilerV3
{
    /*
     * 
     * yjk, 20.01.15 -
     * Line View의 축 속성 정의
     * 
     */
    public class CTrendLineViewAxisProeprties
    {
        //X Axis
        private DateTime m_dtStartTime = DateTime.MinValue;
        private DateTime m_dtEndTime = DateTime.MinValue;
        private int m_iXScalse = 100;

        //Y Axis
        private double m_dStartValue = 0;
        private double m_dEndValue = 10;
        private double m_dYScale = 0.5;


        public DateTime XAxisStartTime
        {
            get { return m_dtStartTime; }
            set { m_dtStartTime = value; }
        }

        public DateTime XAxisEndTime
        {
            get { return m_dtEndTime; }
            set { m_dtEndTime = value; }
        }

        //단위 : ms
        public int XAxisScale
        {
            get { return m_iXScalse; }
            set { m_iXScalse = value; }
        }

        public double YAxisStartValue
        {
            get { return m_dStartValue; }
            set { m_dStartValue = value; }
        }

        public double YAxisEndValue
        {
            get { return m_dEndValue; }
            set { m_dEndValue = value; }
        }

        public double YAxisScale
        {
            get { return m_dYScale; }
            set { m_dYScale = value; }
        }
    }
}
