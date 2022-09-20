// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CLogicHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UDM.Common;
using UDM.Converter;
using UDM.General;
using UDM.General.Csv;
using UDM.Project;
using UDM.UDLImport;

namespace UDMProfilerV3
{
    public static class CLogicHelper
    {

        #region Variables

        private static CStepS m_cLSStepS = null;
        private static CTagS m_cLSTagS = null;
        private static bool m_bIsLS = false;
        private static bool m_bIsTestMode = false;
        private static int m_iCommentIndex = -1;

        //jjk, 22.02.18 - USB 판매방식 모드
        public static bool USBKeyLock = false;

        #endregion


        #region Properties

        public static int CommentIndex
        {
            set { m_iCommentIndex = value; }
        }

        public static bool IsTestMode
        {
            set { m_bIsTestMode = value; }
        }

        public static bool IsLS
        {
            get { return m_bIsLS; }
            set { m_bIsLS = value; }
        }

        #endregion


        #region Public Method

        ////jjk, 22.04.06 - LG 전자 문제은 책임 요청 사항으로 LS PLC (S)접점에 대한 Step Convert
        //public static List<CStep> ConvertLsGroupByStep(CProfilerProject cProject, List<CStep> lstSteps)
        //{
        //    if (((CProfilerProject_V8)cProject).PLCMaker == UDM.DDEACommon.EMPlcMaker.LS)
        //    {
        //        var lstGroupByStepS = lstSteps.GroupBy(item => item.Address, (key, group) => new { Address = key, StepS = group.ToList() });

        //        //GroupBy 수량으로 이미 Convert가 되었는지 확인하고 이미 convert 작업이 진행이 되어있다면 그대로 리턴
        //        if (lstGroupByStepS.Count() == lstSteps.Count())
        //            return new List<CStep>();

        //        CStepS tempStempS = new CStepS();

        //        foreach (var item in lstGroupByStepS)
        //        {
        //            string sAddress = item.Address;
        //            if (GetAddressHeader(sAddress) != "S")
        //                continue;

        //            if (item.StepS.Count < 0 && item.StepS[0].ContactS.Count < 0 && item.StepS[0].ContactS[0].ContentS.Count < 0)
        //                continue;

        //            //컨텍트 가져오기 
        //            var contacts = item.StepS.Select(x => x.ContactS).ToList();

        //            CContactS tmepContacts = new CContactS();
        //            CContentS tempContents = new CContentS();
        //            foreach (var value in contacts)
        //            {
        //                CContact contact = value[0];
        //                if (contact == null)
        //                    continue;
        //                tmepContacts.Add(contact);
        //                foreach (CContent content in contact.ContentS.ToList())
        //                {
        //                    if (content == null)
        //                        continue;
        //                    tempContents.Add(content);
        //                }
        //            }


        //            foreach (CContact temp in tmepContacts)
        //                temp.ContentS = tempContents;

        //            List<CCoil> lstCoilS = item.StepS.Select(x => x.CoilS[0]).ToList();
        //            CCoilS tmepCoilS = new CCoilS();
        //            foreach (CCoil coil in lstCoilS)
        //                tmepCoilS.Add(coil);

        //            CStep oriCStep = item.StepS.First().Clone() as CStep;
        //            List<CTag> getTagS = cProject.GetTagList(oriCStep);

        //            //list ctag 에서 s접점을 찾음 
        //            string findKey = getTagS.Find(x => x.Address == sAddress).Key;

        //            if (!string.IsNullOrEmpty(findKey))
        //            {
        //                if (oriCStep != null)
        //                {
        //                    oriCStep.ContactS = tmepContacts;
        //                    oriCStep.CoilS = tmepCoilS;
        //                    oriCStep.Key = findKey;

        //                    string sKey = item.StepS.First().Key;
        //                    tempStempS.Add(sKey, oriCStep);
        //                }
        //            }
        //        }

        //        //yjk, 22.04.15 - 원본데이터를 왜...건드리지.. 주석처리
        //        //cProject.StepS = tempStempS;
        //        //lstSteps = cProject.StepS
        //        //                 .Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(x => x.Value))
        //        //                 .Where(x => x.DataType != EMDataType.None)
        //        //                 .ToList<CStep>();

        //        lstSteps = tempStempS.Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(x => x.Value))
        //                             .Where(x => x.DataType != EMDataType.None)
        //                             .ToList<CStep>();
        //        return lstSteps;
        //    }
        //    else
        //        return new List<CStep>();
        //}

        public static void ConvertAddressFilterFromat(CProfilerProject cProject, CUDLImport udlAuto)
        {
            string sAddress = cProject.TagS.Values.ToList()[0].Address;
            string sAddressHeader = string.Empty;
            string sAddressIndex = string.Empty;
            string sTemp = string.Empty;

            if (CMelsecPlc.IsMelsecHeadOne(sAddress))
            {
                sAddressHeader = sAddress.Substring(0, 1);
                sAddressIndex = sAddress.Remove(0, 1);
            }
            else if (CMelsecPlc.IsMelsecHeadTwo(sAddressIndex))
            {
                sAddressHeader = sAddress.Substring(0, 2);
                sAddressIndex = sAddress.Remove(0, 2);
            }

            if (sAddressIndex.Contains("."))
                sTemp = sAddressIndex.Split('.')[0];
            else
                sTemp = sAddressIndex;

            foreach (CTag cTag in udlAuto.AutoTypeTagS)
            {
                string[] sAddrssArr = cTag.Address.Split('/');
                string sKey = cTag.Key.Substring(cTag.Key.Length - 3, 3);
                if (sTemp.Length == 4)
                {
                    cTag.Address = sAddrssArr[0];
                    cTag.Key = string.Format($"{ cTag.Channel}{ cTag.Address}{ sKey}");
                }
                else
                {
                    cTag.Address = sAddrssArr[1];
                    cTag.Key = string.Format($"{ cTag.Channel}{ cTag.Address}{ sKey}");
                }
            }
        }


        public static List<CStep> CreateCoilStep(CStepS cStepS)
        {
            List<CStep> lstCoilStep = new List<CStep>();
            //yjk, 21.03.17 - DataType = None 인 것은 제외(K0와 같은 접점이 아닌 값인 경우인 것으로 우선적으로 판단됨)
            List<CStep> lstSteps = cStepS
                                   .Select<KeyValuePair<string, CStep>, CStep>((Func<KeyValuePair<string, CStep>, CStep>)(x => x.Value))
                                   .Where(x => x.DataType != EMDataType.None)
                                   .ToList<CStep>();

            //yjk, 21.03.21 - 기존은 Step 기준으로 나와서 모든 Coil이 표현이 되지 않았으나 모든 Coil을 표현하기 위한 작업
            List<CStep> lstDatasource = new List<CStep>();
            if (lstSteps != null)
            {
                foreach (CStep step in lstSteps)
                {
                    for (int i = 0; i < step.CoilS.Count; i++)
                    {
                        //기준 Coil로 Step 복사하여 idx 변경
                        CStep clone = (CStep)step.Clone();
                        CCoil coil1 = (CCoil)step.CoilS[i].Clone();
                        CCoil coil2 = (CCoil)step.CoilS[0].Clone();

                        clone.CoilS[0] = coil1;
                        clone.CoilS[i] = coil2;
                        //yjk, 21.03.29
                        clone.StepIndex = coil1.StepIndex;

                        lstCoilStep.Add(clone);
                    }
                }
            }
            return lstCoilStep;
        }

        //jjk, 21.05.03 - Convert Coil Step Contact
        public static CStep ConvertCoilStepToContact(CStep oriStep)
        {
            //CContactS tempContactS = new CContactS();
            if (oriStep == null)
                return null;

            CCoil cCoil = oriStep.CoilS[0];
            if (cCoil == null || cCoil.Relation.PrevContactS.Count == 0)
                return null;

            Dictionary<int, List<int>> dictContactFlow = new Dictionary<int, List<int>>();
            GetContactSIndex(oriStep, cCoil.Relation.PrevContactS, dictContactFlow, -1, true);

            List<int> lstElement = new List<int>();
            foreach (int key in dictContactFlow.Keys)
                foreach (int refStepIndex in dictContactFlow[key])
                    lstElement.Add(refStepIndex);

            if (lstElement.Count == 0)
                return null;

            //선택한 출력 접점의 Coil만 남기고 나머지는 필요 없기 때문에 지우기
            foreach (CCoil removeCoil in oriStep.CoilS.ToList())
            {
                if (removeCoil != cCoil)
                    oriStep.CoilS.Remove(removeCoil);
            }

            //출력 접점과 연관된 하위점접 
            CContactS tempContacS = new CContactS();
            foreach (int StepIndex in lstElement)
            {
                CContact contact = oriStep.ContactS.Find(x => x.StepIndex == StepIndex);
                if (!tempContacS.Contains(contact))
                    tempContacS.Add(contact);
            }

            oriStep.ContactS = tempContacS;

            return oriStep;
        }

        //yjk, 21.03.19 - Get Contacts Index List
        //yjk, 21.03.29 - Coil 까지 오기의 Flow들을 저장
        public static void GetContactSIndex(CStep step, List<int> lstPrevContact, Dictionary<int, List<int>> dictContactFlow, int iKey, bool bFirst)
        {
            for (int i = 0; i < lstPrevContact.Count; i++)
            {
                int idx = lstPrevContact[i];
                int iNewKey = dictContactFlow.Count;

                //첫 함수 진입인 경우
                if (bFirst)
                {
                    CContact contact = step.ContactS.Find(x => x.StepIndex.Equals(idx));
                    if (contact != null)
                    {
                        List<int> lstContact = new List<int>();
                        lstContact.Add(idx);

                        dictContactFlow.Add(iNewKey, lstContact);

                        //재귀
                        if (contact.Relation.PrevContactS.Count > 0)
                            GetContactSIndex(step, contact.Relation.PrevContactS, dictContactFlow, iNewKey, false);
                    }
                }
                else
                {
                    CContact contact = step.ContactS.Find(x => x.StepIndex.Equals(idx));
                    if (contact != null)
                    {
                        //그대로 이어서 Step Index Flow 써줌
                        if (i == 0)
                        {
                            if (lstPrevContact.Count > 1)
                            {
                                //이전 Flow 복사
                                List<int> lstNew = new List<int>();
                                foreach (int val in dictContactFlow[iKey])
                                    lstNew.Add(val);

                                dictContactFlow.Add(iNewKey, lstNew);
                            }

                            dictContactFlow[iKey].Add(idx);

                            if (contact.Relation.PrevContactS.Count > 0)
                                GetContactSIndex(step, contact.Relation.PrevContactS, dictContactFlow, iKey, false);
                        }
                        //새로운 Step Index Flow 추가
                        else
                        {
                            dictContactFlow[iNewKey - 1].Add(idx);

                            if (contact.Relation.PrevContactS.Count > 0)
                                GetContactSIndex(step, contact.Relation.PrevContactS, dictContactFlow, iNewKey - 1, false);
                        }
                    }
                }
            }
        }

        public static CProfilerProject Convert(CViewLogicFileInfoS cFileS, BackgroundWorker bgWorker)
        {
            CILConvert cILConvert = CLogicHelper.Import(cFileS, bgWorker);
            if (cILConvert == null)
                return null;

            bgWorker.ReportProgress(99, "Convert");

            CLCConvet clcConvet = new CLCConvet(cILConvert);
            CProfilerProject cprofilerProject = new CProfilerProject();
            cprofilerProject.StepS = clcConvet.StepS;
            cprofilerProject.TagS = clcConvet.TagS;
            cprofilerProject.Compose();

            return cprofilerProject;
        }

        public static CProfilerProject Convert(Dictionary<string, CViewLogicFileInfo> dicLogicFileInfo, BackgroundWorker bgWorker)
        {
            CILConvert cILConvert = CLogicHelper.Import(dicLogicFileInfo, bgWorker);
            bgWorker.ReportProgress(95, "Convert");
            CProfilerProject cprofilerProject = new CProfilerProject();
            if (IsLS)
            {
                cprofilerProject.StepS = CLogicHelper.m_cLSStepS;
                cprofilerProject.TagS = CLogicHelper.m_cLSTagS;
            }
            else
            {
                if (cILConvert == null)
                    return null;

                CLCConvet clcConvet = new CLCConvet(cILConvert);
                cprofilerProject.StepS = clcConvet.StepS;
                cprofilerProject.TagS = clcConvet.TagS;
            }

            cprofilerProject.Compose();

            return cprofilerProject;
        }

        public static List<string> GetErrorFileList(string[] saFiles, int iFilterIndex)
        {
            List<string> stringList = new List<string>();
            foreach (string saFile in saFiles)
            {
                int iHeaderRowIndex = 0;
                CCsvReader ccsvReader = new CCsvReader();

                if (iFilterIndex == 2)
                {
                    ccsvReader.CsvType = EMCsvType.Tab;
                    iHeaderRowIndex = 2;
                }

                if (!ccsvReader.Open(saFile, true, iHeaderRowIndex) || ccsvReader.Header.Count < 2)
                    stringList.Add(saFile);

                ccsvReader.Close();
                ccsvReader.Dispose();
            }
            return stringList;
        }

        public static string GetTagKey(string sAddress)
        {
            if (sAddress == "")
                return "";

            string str = "[CH.DV]";
            ////yjk, 19.07.18 - test 주석
            //if (m_bIsLS)
            //    str = "[CH.DV]";

            //yjk, 19.05.24 - D100.0 같은 경우 Key는 [CH_DV]D100_1[1]임
            sAddress = sAddress.Replace('.', '_');

            return str + sAddress.Trim() + "[1]";
        }

        public static string GetTagKey(string sAddress, EMDataType emDataType)
        {
            string str1 = "[CH.DV]";
            ////yjk, 19.07.18 - test 주석
            //if (m_bIsLS)
            //    str1 = "[CH.DV]";
            string str2 = str1 + sAddress.Trim();
            return emDataType != EMDataType.DWord && emDataType != EMDataType.Block ? str2 + "[1]" : str2 + "[2]";
        }

        public static string GetTagKey(string sAddress, int iSize)
        {
            string str = "[CH.DV]";
            ////yjk, 19.07.18 - test 주석
            //if (m_bIsLS)
            //    str = "[CH.DV]";
            return str + sAddress.Trim() + "[" + iSize.ToString() + "]";
        }

        public static bool IsBit(string sAddress)
        {
            return CPlcMelsec.IsBit(sAddress);
        }

        public static bool IsWord(string sAddress)
        {
            return CPlcMelsec.IsWord(sAddress);
        }

        public static bool IsHexa(string sAddress)
        {
            return CPlcMelsec.IsHexa(sAddress);
        }

        //yjk, 19.05.23 - Address Header 받기
        public static string GetAddressHeader(string sTagAddress)
        {
            string sTmp = string.Empty;
            int iRemoveCnt = 0;

            //Address가 K로 시작한다면 비트값을 Word 처럼 읽기 위한 것이므로 뒤에 나오는 접점 주소가 실제 주소임
            if (sTagAddress.StartsWith("K"))
            {
                sTmp = sTagAddress;

                for (int i = 0; i < sTmp.Length; i++)
                {
                    //첫번째 문자는 "K"이기 때문에 넘김
                    if (i == 0)
                    {
                        iRemoveCnt++;
                        continue;
                    }

                    if (char.IsNumber(sTmp[i]))
                    {
                        iRemoveCnt++;
                    }
                    else
                        break;
                }

                sTagAddress = sTmp.Remove(0, iRemoveCnt);
            }

            string sHeader = string.Empty;
            for (int i = 0; i < sTagAddress.Length; i++)
            {
                if (!char.IsNumber(sTagAddress[i]))
                {
                    sHeader += sTagAddress[i];
                }
                else
                {
                    break;
                }
            }

            return sHeader;
        }

        //yjk, 20.02.10 - 주소의 숫자 부분 반환
        public static string GetNumeric(string sAddress)
        {
            string sNumeric = string.Empty;
            int idx = -1;

            if (string.IsNullOrEmpty(sAddress))
                return sNumeric;

            for (int i = sAddress.Length - 1; i >= 0; i--)
            {
                //Address 끝부터 체크하여 문자열이 시작되는 Index를 저장
                if (!char.IsDigit(sAddress[i]))
                {
                    idx = i;
                    break;
                }
            }
            string s = sAddress.Remove(0, idx + 1);
            return s;
        }

        //yjk, 19.07.23 - LS Monitoring을 위한 Address 설정
        public static string SetLSAddress(string sAddress, EMDataType emDataType)
        {
            string sHeader = Utils.GetAddressHeader(sAddress);
            string sNumber = sAddress.Remove(0, sHeader.Length);
            string[] splt = sNumber.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string sRemovedDot = string.Empty;

            //점을 뺀 숫자만 
            for (int i = 0; i < splt.Length; i++)
            {
                sRemovedDot += splt[i];
            }

            //Monitoring Address 숫자의 맞춰야 할 자릿수
            int iTargetCnt = 0;
            if (emDataType == EMDataType.Bool)
            {
                iTargetCnt = 6;
            }
            else
            {
                iTargetCnt = 5;
            }

            for (int i = 0; i < 5; i++)
            {
                if (sRemovedDot.Length < iTargetCnt)
                    sRemovedDot = sRemovedDot.Insert(0, "0");
                else
                    break;
            }

            return sHeader + sRemovedDot;
        }



        #endregion


        #region Private Method

        private static DataSet Load(CViewLogicFileInfoS cFileS, bool bTabSplitter)
        {
            DataSet dataSet = new DataSet();
            for (int index = 0; index < cFileS.Count; ++index)
            {
                dataSet.Tables.Add(cFileS[index].Name);
                CCsvReader ccsvReader = new CCsvReader();
                if (bTabSplitter)
                    ccsvReader.CsvType = EMCsvType.Tab;
                bool flag = false;
                string text = cFileS[index].GetText();
                flag = !dataSet.Tables[index].TableName.Contains("COMMENT") ? (!bTabSplitter ? ccsvReader.Fill(text, false, -1, dataSet.Tables[index]) : ccsvReader.Fill(text, true, 2, dataSet.Tables[index])) : ccsvReader.Fill(text, "Device", dataSet.Tables[index]);
                ccsvReader.Dispose();
            }
            return dataSet;
        }

        //jjk, 20.11.23 - Comment combo Box index 추가 
        private static CILSymbolS ImportSymbol(DataSet DS, bool bTabSplitter)
        {
            CILSymbolS cilSymbolS = new CILSymbolS();
            foreach (DataTable table in (InternalDataCollectionBase)DS.Tables)
            {
                //jjk, 20.11.20 - 들어온 File Name에 따라서 다른 동작을 해야함.
                string sFileExtension = table.TableName.Substring(table.TableName.LastIndexOf('.'));
                string sCommentUpper = table.TableName.Replace(sFileExtension, string.Empty).ToUpper();
                if (sCommentUpper == "COMMENT")
                {
                    int index1 = 0;
                    int index2 = 2;
                    if (bTabSplitter)
                    {
                        if (m_iCommentIndex == -1)
                            index2 = 1;
                        else
                            index2 = m_iCommentIndex;
                    }

                    for (int index3 = 0; index3 < table.Rows.Count; ++index3)
                    {
                        string upper2 = table.Rows[index3].ItemArray[index1].ToString().ToUpper();
                        CILSymbol cilSymbol = new CILSymbol(table.Rows[index3].ItemArray[index2].ToString(), upper2, sCommentUpper);
                        cilSymbolS.Add(cilSymbol.Key, cilSymbol);
                    }
                }
            }
            return cilSymbolS;
        }

        private static CILConvert Import(CViewLogicFileInfoS cFileS, BackgroundWorker backgroundWorker)
        {
            bool bTabSplitter = false;
            if (cFileS[0].Format == "Works2")
                bTabSplitter = true;
            CILConvert cilConvert = new CILConvert();
            CILImport cilImport = new CILImport();
            DataSet DS = new DataSet();
            try
            {
                DS = CLogicHelper.Load(cFileS, bTabSplitter);
                cilImport.ImportIL(DS);
                cilConvert.ConvertIL(cilImport.DicILLINE, backgroundWorker);
                cilConvert.SymbolS = CLogicHelper.ImportSymbol(DS, bTabSplitter);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                cilConvert = (CILConvert)null;
            }
            finally
            {
                if (DS != null)
                {
                    DS.Clear();
                    DS.Dispose();
                }
            }
            return cilConvert;
        }

        private static CILConvert Import(Dictionary<string, CViewLogicFileInfo> dictLogicFileInfo, BackgroundWorker backgroundWorker)
        {
            bool bTabSplitter = false;
            CILConvert cilConvert = new CILConvert();
            CILImport cilImport = new CILImport();
            DataSet dataSet = new DataSet();
            List<string> stringList = new List<string>();
            try
            {
                foreach (string key in dictLogicFileInfo.Keys)
                {
                    if (dictLogicFileInfo[key].Format == "Works2")
                        bTabSplitter = true;
                    if (dictLogicFileInfo[key].Format == "LS")
                        m_bIsLS = true;
                    stringList.Add(key);
                }

                CFileOpen cFile = null;
                if (m_bIsLS)
                    cFile = new CFileOpen(EMPLCMaker.LS, stringList.ToArray());
                else
                    cFile = new CFileOpen(EMPLCMaker.Mitsubishi, stringList.ToArray());

                CUDLImport cUDL = new CUDLImport(cFile);
                if (m_bIsLS)
                {
                    cUDL.LsDDEAConnect = false;
                    bool bOK = cUDL.UDLGenerate();

                    if (bOK)
                    {
                        CLogicHelper.m_cLSTagS = cUDL.GlobalTags;
                        CLogicHelper.m_cLSStepS = cUDL.StepS;
                    }
                }
                else
                {
                    cilImport.ImportIL(cUDL);
                    cilConvert.ConvertIL(cilImport.DicILLINE, backgroundWorker);
                    cilConvert.SymbolS = CLogicHelper.ImportSymbol(cUDL.FileOpen.dbMelsecCSV, bTabSplitter);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                cilConvert = (CILConvert)null;
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Clear();
                    dataSet.Dispose();
                }
            }
            return cilConvert;
        }

        private static DataSet Load(Dictionary<string, CViewLogicFileInfo> dictLogicFileInfo, bool bWorks2, bool bLS)
        {
            DataSet dataSet = new DataSet();
            EMPLCMaker plcMaker = EMPLCMaker.Mitsubishi;
            if (bLS)
                plcMaker = EMPLCMaker.LS;
            foreach (string key in dictLogicFileInfo.Keys)
            {
                string text = dictLogicFileInfo[key].GetText();
                DataTable table = (DataTable)null;
                string extension = Path.GetExtension(key);
                if (extension.ToUpper().Equals(".CSV"))
                    table = CLogicHelper.OpenCSVFile(key, plcMaker, bWorks2, text);
                else if (extension.ToUpper().Equals(".IL"))
                    table = CLogicHelper.OpenILFile(key);
                if (table != null)
                    dataSet.Tables.Add(table);
            }
            return dataSet;
        }

        private static DataTable OpenCSVFile(string sPath, EMPLCMaker plcMaker, bool bWorks2, string sText)
        {
            DataTable dbTable = new DataTable(Path.GetFileName(sPath));
            CCsvReader ccsvReader = new CCsvReader();
            bool flag = false;
            if (plcMaker == EMPLCMaker.LS)
            {
                if (ccsvReader.Open(sPath, true, 7))
                    flag = ccsvReader.Fill(dbTable);
            }
            else
            {
                if (bWorks2)
                    ccsvReader.CsvType = EMCsvType.Tab;
                if (dbTable.TableName.Contains("COMMENT"))
                {
                    if (ccsvReader.Open(sPath, true, 1))
                        flag = ccsvReader.Fill(sText, "Device", dbTable);
                }
                else if (bWorks2)
                {
                    if (ccsvReader.Open(sPath, true, 2))
                        flag = ccsvReader.Fill(sText, true, 2, dbTable);
                }
                else if (ccsvReader.Open(sPath, true))
                    flag = ccsvReader.Fill(sText, false, -1, dbTable);
            }
            ccsvReader.Dispose();
            return dbTable;
        }

        private static DataTable OpenILFile(string sPath)
        {
            if (!File.Exists(sPath))
                return (DataTable)null;
            string fileName = Path.GetFileName(sPath);
            DataTable dataTable = new DataTable(fileName);
            try
            {
                StreamReader streamReader = new StreamReader(sPath, Encoding.Default);
                while (!streamReader.EndOfStream)
                {
                    string str = streamReader.ReadLine();
                    if (!(str == string.Empty))
                    {
                        if (!str.Contains("\t") && str.Contains("[") && str.Contains("]"))
                        {
                            dataTable.TableName = string.Format("{0}_{1}", (object)fileName, (object)str);
                        }
                        else
                        {
                            string[] strArray = str.Split('\t');
                            if (strArray.Length != 1)
                            {
                                if (dataTable.Columns.Count == 0)
                                {
                                    for (int index = 0; index < 3; ++index)
                                        dataTable.Columns.Add(index.ToString());
                                }
                                DataRow row = dataTable.NewRow();
                                if (strArray.Length == 4)
                                {
                                    strArray[1] = string.Format("{0}.{1}", (object)strArray[1], (object)strArray[2]);
                                    strArray[2] = strArray[3];
                                    for (int index = 0; index < 3; ++index)
                                        row[index] = (object)strArray[index];
                                }
                                else
                                {
                                    for (int index = 0; index < strArray.Length; ++index)
                                        row[index] = (object)strArray[index];
                                }
                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }
                streamReader.Close();
                streamReader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0} [{1}]", (object)ex.Message, (object)MethodBase.GetCurrentMethod().Name);
                ex.Data.Clear();
                return (DataTable)null;
            }
            return dataTable;
        }


        #endregion

    }
}
