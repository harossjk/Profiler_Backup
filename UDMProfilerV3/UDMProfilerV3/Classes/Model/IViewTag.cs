using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UDM.Common;

namespace UDMProfilerV3
{
    public interface IViewTag : IDisposable
    {

        #region Public Properties

        string Key { get; set; }

        string Address { get; set; }

        string Description { get; set; }

        int Size { get; set; }

        EMDataType DataType { get; set; }

        string Program { get; set; }

        string LinkAddress { get; set; }

        string Creator { get; set; }

        CTag Tag { get; set; }

        #endregion


        #region Public Methods

        void Apply();

        #endregion
    }
}
