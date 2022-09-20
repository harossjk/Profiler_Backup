// Decompiled with JetBrains decompiler
// Type: UDMDDEA.FrmAddAddress
// Assembly: UDMDDEA, Version=3.18.5.24, Culture=neutral, PublicKeyToken=null
// MVID: 9255FCB2-6F38-4411-AFDC-A0E5CCCB3BA6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMDDEA.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UDMDDEA
{
    public partial class FrmAddAddress : Form
    {
        private Dictionary<string, int> m_dicAddress = new Dictionary<string, int>();
        private Dictionary<string, string> m_dicCycle = new Dictionary<string, string>();
        private List<string> m_lstRecipe = new List<string>();
        private List<string> m_lstModel = new List<string>();
        private List<string> m_lstGlassID = new List<string>();
        private List<string> m_lstLot = new List<string>();

        public FrmAddAddress(Dictionary<string, int> dicAddress)
        {
            this.InitializeComponent();
            this.m_dicAddress = dicAddress;
        }

        public Dictionary<string, string> CycleAddressList
        {
            get
            {
                return this.m_dicCycle;
            }
        }

        public List<string> RecipeList
        {
            get
            {
                return this.m_lstRecipe;
            }
        }

        public List<string> ModelList
        {
            get
            {
                return this.m_lstModel;
            }
        }

        public List<string> GlassIDList
        {
            get
            {
                return this.m_lstGlassID;
            }
        }

        public List<string> LotList
        {
            get
            {
                return this.m_lstLot;
            }
        }

        public Dictionary<string, int> AddressList
        {
            get
            {
                return this.m_dicAddress;
            }
        }

        protected List<string> ExtractAddress(string sSource)
        {
            List<string> stringList = new List<string>();
            string[] strArray = sSource.Replace("\r", "").Split('\n');
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (!(strArray[index] == "") && !strArray[index].Contains(","))
                    stringList.Add(strArray[index]);
            }
            return stringList;
        }

        protected Dictionary<string, string> ExtractCycle(string sSource)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] strArray1 = sSource.Replace("\r", "").Split('\n');
            for (int index = 0; index < strArray1.Length; ++index)
            {
                if (!(strArray1[index] == "") && strArray1[index].Contains(","))
                {
                    string[] strArray2 = strArray1[index].Split(',');
                    dictionary.Add(strArray2[0], strArray2[1]);
                }
            }
            return dictionary;
        }

        private void FrmAddAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.m_dicAddress.Clear();
            string[] strArray1 = this.txtCollectAddress.Text.Replace("\r", "").Split('\n');
            try
            {
                for (int index = 0; index < strArray1.Length; ++index)
                {
                    if (!(strArray1[index] == ""))
                    {
                        if (strArray1[index].Contains(","))
                        {
                            string[] strArray2 = strArray1[index].Split(',');
                            int int32 = Convert.ToInt32(strArray2[1]);
                            if (!this.m_dicAddress.ContainsKey(strArray2[0]))
                                this.m_dicAddress.Add(strArray2[0], int32);
                        }
                        else
                            this.m_dicAddress.Add(strArray1[index], 1);
                    }
                }
                this.m_dicCycle = this.ExtractCycle(this.txtCycle.Text);
                this.m_lstRecipe = this.ExtractAddress(this.txtRecipe.Text);
                this.m_lstModel = this.ExtractAddress(this.txtModel.Text);
                this.m_lstGlassID = this.ExtractAddress(this.txtGlassID.Text);
                this.m_lstLot = this.ExtractAddress(this.txtLot.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Address Change Fail : " + ex.Message);
            }
        }

        private void FrmAddAddress_Load(object sender, EventArgs e)
        {
            if (this.m_dicAddress.Count <= 0)
                return;
            foreach (KeyValuePair<string, int> keyValuePair in this.m_dicAddress)
                this.txtCollectAddress.AppendText(string.Format("{0},{1}\r\n", (object)keyValuePair.Key, (object)keyValuePair.Value));
        }
    }
}
