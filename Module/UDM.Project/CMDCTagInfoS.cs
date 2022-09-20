using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;

namespace UDM.Project
{
    [Serializable]
    public class CMDCTagInfoS : Dictionary<string, CMDCTagInfo>
    {

        #region Member Varialbes

        #endregion


        #region Inialize/Dispose


        #endregion


        #region Public Properties

        #endregion


        #region Public Methods

        public void Add(CMDCSubItemInfo cSubItem)
        {
            if (this.ContainsKey(cSubItem.Key))
            {
                CMDCTagInfo cMDCTag = this[cSubItem.Key];

                bool bNew = true;
                for (int i = 0; i < cMDCTag.CodeList.Count; i++)
                {
                    if (cMDCTag.ParentList[i] == cSubItem.Parent && cMDCTag.CodeList[i] == cSubItem.Code)
                    {
                        bNew = false;
                        break;
                    }
                }

                if (bNew)
                {
                    cMDCTag.ParentList.Add(cSubItem.Parent);
                    cMDCTag.CodeList.Add(cSubItem.Code);
                }
            }
            else
            {
                CMDCTagInfo cMDCTag = new CMDCTagInfo();
                cMDCTag.Key = cSubItem.Key;
                cMDCTag.Address = cSubItem.Address;
                cMDCTag.Size = cSubItem.Size;

                cMDCTag.ParentList.Add(cSubItem.Parent);
                cMDCTag.CodeList.Add(cSubItem.Code);

                this.Add(cSubItem.Key, cMDCTag);
            }
        }

        #endregion
    }
}
