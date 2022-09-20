using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UDM.Common;

namespace UDMProfilerV3
{
    public class CUserTagS :List<CUserTag>, IDisposable
    {

        #region Member Variables


        #endregion


        #region Initialize/Dispose

        public CUserTagS()
        {

        }

        public void Dispose()
        {
            Clear();
        }

        #endregion


        #region Public Properties


        #endregion


        #region Public Methods


        #endregion


        #region Private Methods


        #endregion
    }
}
