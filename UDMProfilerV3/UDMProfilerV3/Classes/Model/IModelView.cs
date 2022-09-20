using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDMProfilerV3
{
    public interface IModelView : IView
    {
        bool IsEditable { get; set; }
    }
}
