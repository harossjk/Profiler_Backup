using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    public class CDDEAMdcSymbolList : List<CDDEAMdcSymbol>
    {
        #region Member Variables

        
        #endregion


        #region Initialize

        public CDDEAMdcSymbolList()
        {


        }

        #endregion


        #region Properties


        #endregion


        #region Public Method


        public void AddSymbol(CDDEAMdcSymbol cSymbol)
        {
            if (!this.Contains(cSymbol))
                this.Add(cSymbol);
        }


        public void CreateWordLength(CDDEAMdcSymbol cSymbol)
        {
            //Length가 1이상 일경우 새로운Word를 생성한다 단, 새로 생성된 접점은 Length가 0이다.
            if (cSymbol.DataType == EMDataType.Word && cSymbol.AddressCount > 1)
            {
                for (int i = 1; i < cSymbol.AddressCount; i++)
                {
                    int iAddress = cSymbol.AddressMajor + i;
                    string sAddress = cSymbol.AddressHeader + iAddress.ToString();
                    if (cSymbol.CheckAddressHexa(sAddress))
                    {
                        string sHexa = string.Format("{0:x}", iAddress);
                        sAddress = cSymbol.AddressHeader + sHexa;
                    }

                    CDDEAMdcSymbol cSubSymbol = new CDDEAMdcSymbol(sAddress, false);
                    cSubSymbol.CreateMelsecDDEASymbol(sAddress);
                    cSubSymbol.BaseAddress = sAddress;
                    cSubSymbol.AddressCount = 0;

                    if (this.Contains(cSubSymbol) == false)
                        this.AddSymbol(cSubSymbol);
                    cSymbol.DWordSecondAddress = sAddress;
                }
            }
        }

        public List<CDDEAMdcSymbol> FindEqulBaseAddressSymbol(string sAddress)
        {
            List<CDDEAMdcSymbol> lstResult = this.FindAll(b => b.Address == sAddress);

            return lstResult;
        }
        #endregion
    }
}
