// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CAsyncTcpClient
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UDM.DDEACommon;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CAsyncTcpClient
    {
        private string m_sHost = "172.25.28.136";
        private int m_iPort = 6000;
        private bool m_bConnect = false;
        private EMConnectAppType m_emApp = EMConnectAppType.None;
        private const int MAXSIZE = 1024;
        private byte[] nRecvBuffer;
        private Socket m_ClientSock;
        private Socket m_cbSock;

        public event UEventHandlerMessage UEventMessage;

        public event UEventHandlerReceiveMessage UEventReceiveMessage;

        public event UEventHandlerClientMessageAnalyze UEventServerMessage;

        public CAsyncTcpClient(string sIP, int iPort, EMConnectAppType emApp)
        {
            this.m_sHost = sIP;
            this.m_iPort = iPort;
            this.nRecvBuffer = new byte[1024];
            this.m_emApp = emApp;
            this.DoInit();
        }

        public bool IsConnected
        {
            get
            {
                return this.m_ClientSock.Connected;
            }
        }

        public void BeginConnect()
        {
            try
            {
                if (this.m_ClientSock == null)
                    this.DoInit();
                this.m_ClientSock.BeginConnect(this.m_sHost, this.m_iPort, new AsyncCallback(this.ConnectCallBack), (object)this.m_ClientSock);
            }
            catch (SocketException ex)
            {
                this.SendMessage(ResDDEA.CAsyncTcpClient_BeginConnect_Msg + ex.Message);
                this.DoInit();
            }
        }

        public void Disconnect()
        {
            if (this.m_cbSock != null)
            {
                this.m_cbSock.Shutdown(SocketShutdown.Both);
                Thread.Sleep(1000);
                this.m_cbSock.Close();
                this.m_cbSock = (Socket)null;
            }
            if (this.m_ClientSock == null)
                return;
            this.m_ClientSock.Close();
            this.m_ClientSock = (Socket)null;
        }

        public void BeginSend(string message)
        {
            try
            {
                if (!this.m_ClientSock.Connected)
                    return;
                byte[] bytes = new UTF8Encoding().GetBytes(message);
                this.m_ClientSock.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(this.SendCallBack), (object)message);
            }
            catch (SocketException ex)
            {
                this.SendMessage(ResDDEA.CAsyncTcpClient_BeginSend_Msg + ex.Message);
            }
        }

        public void Receive()
        {
            this.m_cbSock.BeginReceive(this.nRecvBuffer, 0, this.nRecvBuffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceiveCallBack), (object)this.m_cbSock);
        }

        protected void DoInit()
        {
            this.m_ClientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected void ConnectCallBack(IAsyncResult IAR)
        {
            try
            {
                Socket asyncState = (Socket)IAR.AsyncState;

                //yjk, 19.04.15 - 소켓 접속 상태 예외 처리
                if (!asyncState.Connected)
                    return;

                this.SendMessage(ResDDEA.CAsyncTcpClient_ConnectCallBack_Msg1 + (object)((IPEndPoint)asyncState.RemoteEndPoint).Address);
                asyncState.EndConnect(IAR);
                this.m_cbSock = asyncState;
                this.m_bConnect = true;
                this.m_cbSock.BeginReceive(this.nRecvBuffer, 0, this.nRecvBuffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceiveCallBack), (object)this.m_cbSock);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.NotConnected)
                    return;

                this.SendMessage(ResDDEA.CAsyncTcpClient_ConnectCallBack_Msg2 + ex.Message);
                this.m_bConnect = false;
                if (this.m_emApp == EMConnectAppType.Profiler)
                    this.BeginConnect();
            }
        }

        protected void SendCallBack(IAsyncResult IAR)
        {
            string asyncState = (string)IAR.AsyncState;
        }

        protected void OnReceiveCallBack(IAsyncResult IAR)
        {
            try
            {
                int count = ((Socket)IAR.AsyncState).EndReceive(IAR);
                if (count != 0)
                {
                    string str = new UTF8Encoding().GetString(this.nRecvBuffer, 0, count);
                    if (str != "")
                    {
                        if (this.m_emApp == EMConnectAppType.Manager)
                        {
                            CServerMessageAnalyze cAnalyze = new CServerMessageAnalyze();
                            cAnalyze.ReceiveMessage = str;
                            if (cAnalyze.MessageCommand == EMSendMessageType.SCHT)
                                this.SendMessage("Receive : " + str);
                            else if (cAnalyze.MessageCommand == EMSendMessageType.SCHC)
                                this.SendMessage("Receive : " + str);
                            else if (cAnalyze.MessageCommand != EMSendMessageType.NONE)
                                this.UEventReceiveMessage((object)this, cAnalyze);
                            else
                                this.SendMessage(ResDDEA.CAsyncTcpClient_OnReceiveCallBack_Msg1 + str);
                        }
                        else if (this.m_emApp == EMConnectAppType.Profiler)
                        {
                            this.SendMessage(str);
                            this.AnalyzeProfilerMessage(str);
                        }
                    }
                }
                if (this.m_ClientSock == null)
                    return;
                this.Receive();
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                {
                    this.SendMessage(ResDDEA.CAsyncTcpClient_OnReceiveCallBack_Msg2);
                    this.m_bConnect = false;
                    if (this.m_emApp == EMConnectAppType.Profiler)
                        this.BeginConnect();
                }
            }
        }

        protected void SendServerEventMessage(EMTcpDDEAMessageType emType, string sMessage)
        {
            if (this.UEventServerMessage == null)
                return;
            this.UEventServerMessage((object)this, emType, sMessage);
        }

        protected void SendMessage(string sMsg)
        {
            if (this.UEventMessage == null)
                return;
            this.UEventMessage((object)this, "TCPClient", sMsg);
        }

        protected void AnalyzeProfilerMessage(string sMessage)
        {
            if (!sMessage.Contains("^"))
                return;
            string[] strArray1 = sMessage.Split('#');
            if (strArray1.Length <= 0)
                return;
            for (int index1 = 0; index1 < strArray1.Length; ++index1)
            {
                string[] strArray2 = strArray1[index1].Split('^');
                if (strArray2.Length >= 2)
                {
                    //jjk, 20.11.12 - R series 통신 추가
                    if (strArray2[0] == EMTcpDDEAMessageType.RENetConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.RENetConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.RUsbConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.RUsbConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.UsbConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.UsbConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.MNetConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.MNetConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.ENetConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.ENetConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.GXSimConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.GXSimConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.GOTConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.GOTConfig, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.UpmPath.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.UpmPath, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.Start.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.Start, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.Stop.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.Stop, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.Close.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.Close, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.CollectMode.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.CollectMode, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.LogSavePath.ToString())
                    {
                        string sMessage1 = strArray2[1];
                        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
                        {
                            string source = strArray1[index2];
                            if (!source.Contains<char>('^'))
                                sMessage1 = sMessage1 + "#" + source;
                            else
                                break;
                        }
                        this.SendServerEventMessage(EMTcpDDEAMessageType.LogSavePath, sMessage1);
                    }
                    else if (strArray2[0] == EMTcpDDEAMessageType.SaveTime.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.SaveTime, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.FormShowChange.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.FormShowChange, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.GXSim2Config.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.GXSim2Config, strArray2[1]);
                    else if (strArray2[0] == EMTcpDDEAMessageType.LSConfig.ToString())
                        this.SendServerEventMessage(EMTcpDDEAMessageType.LSConfig, strArray2[1]);
                    else
                        this.SendMessage(ResDDEA.CAsyncTcpClient_AnalyzeProfilerMessage_Msg + sMessage);
                }
            }
        }
    }
}
