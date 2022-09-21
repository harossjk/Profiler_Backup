// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CAsyncTcpServer
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDM.DDEACommon;
using UDM.LS;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CAsyncTcpServer
    {
        protected EMConnectAppType m_emApp = EMConnectAppType.None;
        protected int m_iPort = 0;
        protected CClientMessageAnalyze m_cAnalyze = new CClientMessageAnalyze();
        protected Socket m_ServerSocket;
        protected List<Socket> m_ClientSocket;
        protected byte[] szData;

        public event UEventHandlerMessage UEventMessage;

        public event UEventHandlerClientMessageAnalyze UEventClientMessage;

        public CAsyncTcpServer(EMConnectAppType emApp, int iPort)
        {
            m_emApp = emApp;
            m_iPort = iPort;
            m_ClientSocket = new List<Socket>();
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_ServerSocket.Bind((EndPoint)new IPEndPoint(IPAddress.Any, m_iPort));
            m_ServerSocket.Listen(200);
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            m_ServerSocket.AcceptAsync(e);
        }

        public CAsyncTcpServer()
        {
        }

        public Socket DDEAClient
        {
            get
            {
                if (m_ClientSocket.Count > 0)
                    return m_ClientSocket[0];
                return (Socket)null;
            }
        }

        public bool IsClinetConnect
        {
            get
            {
                return m_ClientSocket.Count == 1;
            }
        }

        public bool Run(EMConnectAppType emApp, int iPort)
        {
            bool flag = false;
            try
            {
                m_emApp = emApp;
                m_iPort = iPort;
                m_ClientSocket = new List<Socket>();
                m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_ServerSocket.Bind((EndPoint)new IPEndPoint(IPAddress.Any, m_iPort));
                m_ServerSocket.Listen(200);
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
                m_ServerSocket.AcceptAsync(e);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return flag;
        }

        public void Stop()
        {
            Close();
            if (m_ServerSocket == null)
                return;
            if (m_ServerSocket.Connected)
            {
                m_ServerSocket.Shutdown(SocketShutdown.Both);
                m_ServerSocket.Close();
            }
            else if (m_ServerSocket.IsBound)
                m_ServerSocket.Close();
            m_ServerSocket = (Socket)null;
        }

        public bool SendClientMessage(string sMessage)
        {
            try
            {
                if (m_ClientSocket.Count > 0 && m_ClientSocket[0].Connected)
                {
                    if (m_ClientSocket.Count == 0)
                        return false;
                    sMessage += "#";
                    m_ClientSocket[0].Send(new UTF8Encoding().GetBytes(sMessage));
                }
            }
            catch (SocketException ex)
            {
                SendMainMessage(ResDDEA.CAsyncTcpServer_SendClientMessage_Msg + ex.Message);
            }
            return true;
        }

        public void Close()
        {
            foreach (Socket socket in m_ClientSocket)
            {
                if (socket.Connected)
                    socket.Disconnect(false);
            }
            m_ClientSocket.Clear();
        }

        public void SendMessageStartDDEA()
        {
            SendClientMessage(string.Format("Start^"));
        }

        public void SendMessageStopDDEA()
        {
            SendClientMessage(string.Format("Stop^"));
        }

        public void SendMessageCloseDDEA()
        {
            SendClientMessage(string.Format("Close^"));
        }

        public void SendUpmPath(string sPath)
        {
            SendClientMessage(string.Format("UpmPath^{0}", sPath));
        }

        public void SendConfig(CDDEAConfigMS_V4 cConfig)
        {
            //jjk, 2020.11.11 - R series 분기 추가 모드 추가 
            if (cConfig == null)
                return;

            if (cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_Normal)
            {
                if (cConfig.SelectedItem == EMConnectTypeMS.USB)
                {
                    SendClientMessage(string.Format("UsbConfig^{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}#",
                                                     cConfig.USB.CPUType.ToString(),
                                                     cConfig.USB.StationType.ToString(),
                                                     cConfig.USB.NetworkNumber,
                                                     cConfig.USB.StationNumber,
                                                     cConfig.USB.DestinationIONumber,
                                                     cConfig.USB.IONumber,
                                                     cConfig.USB.MultiDropChannelNumber,
                                                     cConfig.USB.ThroughNetworkType,
                                                     cConfig.USB.TimeOut,
                                                     cConfig.TimerReadType.ToString(),
                                                     cConfig.USB.UnitNumber
                                                     ));
                }
                else if (cConfig.SelectedItem == EMConnectTypeMS.MNetG || cConfig.SelectedItem == EMConnectTypeMS.MNetH)
                {
                    SendClientMessage(string.Format("MNetConfig^{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}#",
                                                      cConfig.MNet.CPUType.ToString(),
                                                      cConfig.MNet.StationType.ToString(),
                                                      cConfig.MNet.NetworkNumber,
                                                      cConfig.MNet.StationNumber,
                                                      cConfig.MNet.DestinationIONumber,
                                                      cConfig.MNet.IONumber,
                                                      cConfig.MNet.MultiDropChannelNumber,
                                                      cConfig.MNet.ThroughNetworkType,
                                                      cConfig.MNet.PortNumber,
                                                      cConfig.TimerReadType.ToString(),
                                                      cConfig.MNet.UnitNumber,
                                                      cConfig.SelectedItem.ToString()));
                }
                else if (cConfig.SelectedItem == EMConnectTypeMS.Ethernet)
                {
                    if (!cConfig.GetType().IsAssignableFrom(typeof(CDDEAConfigMS_V4)))
                        return;

                    CENet_V2 enetV2 = ((CDDEAConfigMS_V4)cConfig).ENet_V2;

                    string format = "ENetConfig^{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}#";
                    object[] arrValues = new object[21]
                    {
                      enetV2.CPUType.ToString(),
                      enetV2.StationType.ToString(),
                      enetV2.NetworkNumber.ToString(),
                      enetV2.ConnectionUnitNumber.ToString(),
                      enetV2.IPAddress,
                      enetV2.ModuleType.ToString(),
                      enetV2.PacketType.ToString(),
                      enetV2.PC_StationNumber.ToString(),
                      enetV2.PLC_StationNumber.ToString(),
                      enetV2.PortNumber.ToString(),
                      enetV2.ProtocolType.ToString(),
                      enetV2.TimeOut.ToString(),
                      cConfig.TimerReadType.ToString(),
                      enetV2.ActStationNumber.ToString(),
                      enetV2.ActNetworkNumber.ToString(),
                      enetV2.SourceStationNumber.ToString(),
                      enetV2.SourceNetworkNumber.ToString(),
                      enetV2.IONumber.ToString(),
                      enetV2.PacketTypeInt.ToString(),
                      enetV2.CPUTimeOut.ToString(),
                      enetV2.PLCPortNO.ToString()
                    };

                    SendClientMessage(string.Format(format, arrValues));
                }
                else if (cConfig.SelectedItem == EMConnectTypeMS.GXSim)
                {
                    SendClientMessage(string.Format("GXSimConfig^{0},{1},{2},{3},{4},{5}#",
                                                        cConfig.GxSim.CPUType.ToString(),
                                                        cConfig.GxSim.StationType.ToString(),
                                                        cConfig.GxSim.NetworkNumber,
                                                        cConfig.GxSim.StationNumber,
                                                        cConfig.GxSim.TimeOut,
                                                        cConfig.TimerReadType.ToString()));
                }
                else if (cConfig.SelectedItem == EMConnectTypeMS.GOT)
                {
                    SendClientMessage(string.Format("GOTConfig^{0},{1},{2},{3},{4},{5},{6},{7},{8}#",
                                                        cConfig.GOT.CPUType.ToString(),
                                                        cConfig.GOT.StationType.ToString(),
                                                        cConfig.GOT.GotTransparentPcif,
                                                        cConfig.GOT.GotTransparentPlcif,
                                                        cConfig.GOT.IONumber,
                                                        cConfig.GOT.NetworkNumber,
                                                        cConfig.GOT.StationNumber,
                                                        cConfig.GOT.TimeOut,
                                                        cConfig.TimerReadType.ToString()));
                }
                else
                {
                    if (cConfig.SelectedItem != EMConnectTypeMS.GXSim2)
                        return;

                    SendClientMessage(string.Format("GXSim2Config^{0},{1}#",
                                                        ((CDDEAConfigMS_V4)cConfig).GxSim2.SimulatorType.ToString(),
                                                        ((CDDEAConfigMS_V4)cConfig).GxSim2.CPUSeriesType.ToString()));
                }
            }
            else if(cConfig.MelsecSeriesType == EMMelsecSeriesType.Melsec_RSeries)
            {
                if (cConfig.RProtocolSelectedItem == EMMelsecProtocolTypeV4.USB)
                {
                    SendClientMessage(string.Format("RUsbConfig^{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}#",
                                                     cConfig.RSeriesConfig.RCpuType.ToString(),
                                                     cConfig.USB.CpuNumber,
                                                     cConfig.USB.IONumber,
                                                     cConfig.RSeriesConfig.ActProtocolTypeNumber,
                                                     cConfig.RSeriesConfig.OtherStationNumber,
                                                     cConfig.RSeriesConfig.ActBaudRate,
                                                     cConfig.RSeriesConfig.EthernetIP,
                                                     cConfig.RSeriesConfig.ActControl,
                                                     cConfig.RSeriesConfig.ActDataBits,
                                                     cConfig.RSeriesConfig.ActParity,
                                                     cConfig.USB.ThroughNetworkType,
                                                     cConfig.USB.TimeOut,
                                                     cConfig.USB.UnitNumber,
                                                     cConfig.RSeriesConfig.ActUnitTypeNumber
                                                     ));
                }
                else if (cConfig.RProtocolSelectedItem == EMMelsecProtocolTypeV4.EtherNet)
                {
                    SendClientMessage(string.Format("RENetConfig^{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}#",
                                                     cConfig.RSeriesConfig.ProtocolType.ToString(),
                                                     cConfig.RSeriesConfig.ActDestinationPortNumber,
                                                     cConfig.RSeriesConfig.ActProtocolTypeNumber,
                                                     cConfig.RSeriesConfig.RCpuType.ToString(),
                                                     cConfig.ENet_V2.CpuNumber,
                                                     cConfig.RSeriesConfig.UnitType,
                                                     cConfig.RSeriesConfig.ActUnitTypeNumber,
                                                     cConfig.RSeriesConfig.Password,
                                                     cConfig.ENet_V2.TimeOut,
                                                     cConfig.ENet_V2.SourceNetworkNumber,
                                                     cConfig.ENet_V2.SourceStationNumber,
                                                     cConfig.ENet_V2.IPAddress,
                                                     cConfig.ENet_V2.PLCPortNO,
                                                     cConfig.ENet_V2.PortNumber,
                                                     cConfig.ENet_V2.IONumber,
                                                     cConfig.ENet_V2.CPUTimeOut,
                                                     cConfig.ENet_V2.ConnectionUnitNumber,
                                                     cConfig.ENet_V2.ActNetworkNumber,
                                                     cConfig.ENet_V2.ActStationNumber
                                                    ));
                }

            }
        }

        public void SendConfig(CLsConfig cConfig)
        {
            string empty = string.Empty;
            SendClientMessage(cConfig.InterfaceType != EMLsInterfaceType.Ethernet ? string.Format("LSConfig^{0}#", cConfig.InterfaceType.ToString()) : string.Format("LSConfig^{0},{1},{2}#", cConfig.InterfaceType.ToString(), cConfig.IP, cConfig.Port));
        }

        private void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket acceptSocket = e.AcceptSocket;
                m_ClientSocket.Add(acceptSocket);
                if (m_ClientSocket != null)
                {
                    SocketAsyncEventArgs e1 = new SocketAsyncEventArgs();
                    szData = new byte[1024];
                    e1.SetBuffer(szData, 0, 1024);
                    e1.UserToken = m_ClientSocket;
                    e1.Completed += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
                    acceptSocket.ReceiveAsync(e1);
                }
                e.AcceptSocket = (Socket)null;
                if (acceptSocket == null || !acceptSocket.IsBound && !acceptSocket.Connected || m_ServerSocket == null || !m_ServerSocket.Connected && !m_ServerSocket.IsBound)
                    return;
                m_ServerSocket.AcceptAsync(e);
            }
            catch (SocketException ex)
            {
                ex.Data.Clear();
            }
        }

        private void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket socket = (Socket)sender;
                if (socket.Connected && e.BytesTransferred > 0)
                {
                    byte[] buffer = e.Buffer;
                    AnalyzeClientMessage(Encoding.UTF8.GetString(buffer).Replace("\0", "").Trim());
                    for (int index = 0; index < buffer.Length; ++index)
                        buffer[index] = (byte)0;
                    e.SetBuffer(buffer, 0, 1024);
                    socket.ReceiveAsync(e);
                }
                else
                {
                    socket.Disconnect(false);
                    m_ClientSocket.Remove(socket);
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.ConnectionReset)
                    return;
                SendMainMessage(ResDDEA.CAsyncTcpServer_Receive_Msg);
            }
        }

        protected void SendMainMessage(string sMsg)
        {
            if (UEventMessage == null)
                return;
            UEventMessage(this, "TCPServer", sMsg);
        }

        protected void SendClientMessage(EMTcpDDEAMessageType emCommand, string sMessage)
        {
            if (UEventClientMessage == null)
                return;
            UEventClientMessage(this, emCommand, sMessage);
        }

        protected bool AnalyzeClientMessage(string sMessage)
        {
            //그냥 명령이나 잘못들어온 데이터는 분석필요가 없음.
            if (sMessage.Contains("^") == false)
                return false;

            if (m_ClientSocket[0].Connected == false)
                return false;

            //yjk, 19.05.09 - 프로젝트 명에 '#'이 들어가는 경우 잘 못 Split되어 로직 수정
            // 여러 유형의 Command가 있는데 다른 Command는 내부적으로 만들어지는 것이고 Message Command의 Message는 사용자가
            // 만들어내는 메시지이며 여기서 기존 구분자 였던 '#'을 Mesasge에 담을 수도 있기 때문에 단계를 거쳐 String 분리
            string[] saMsg = sMessage.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            if (saMsg.Length <= 0)
                return false;

            //"A^B" 메시지 규칙으로 다시 만든 리스트
            List<string> lstCommand = new List<string>();

            for (int i = 0; i < saMsg.Length; i++)
            {
                string[] splt = saMsg[i].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (splt.Length == 0)
                    continue;

                //sptl[0]이 Command 인지 체크
                if (Enum.IsDefined(typeof(EMTcpDDEAMessageType), splt[0]))
                {
                    if (splt.Length == 1)
                    {
                        lstCommand.Add(splt[0] + "&*");
                    }
                    else
                    {
                        string sVal = string.Empty;

                        //Command String을 제외한 나머지 String은 Message이기 때문에 합침
                        for (int j = 1; j < splt.Length; j++)
                            sVal += splt[j];

                        string ss = sVal;

                        lstCommand.Add(splt[0] + "&*" + ss);
                    }
                }
                else
                {
                    if (splt.Length == 1)
                    {
                        if (lstCommand.Count == 0)
                        {
                            lstCommand.Add(splt[0]);
                        }
                        else
                        {
                            string ss = lstCommand[lstCommand.Count - 1];
                            ss += "#" + splt[0];

                            //마지막 값에 이어 붙여줌
                            lstCommand[lstCommand.Count - 1] = ss;
                        }
                    }
                    else
                    {
                        string sVal = string.Empty;

                        //Command String을 제외한 나머지 String은 Message이기 때문에 합침
                        for (int j = 1; j < splt.Length; j++)
                            sVal += splt[j];

                        string ss = lstCommand[lstCommand.Count - 1];
                        ss += "#" + splt[0] + sVal;

                        if (lstCommand.Count == 0)
                        {
                            lstCommand.Add(ss);
                        }
                        else
                        {
                            //마지막 값에 이어붙여줌
                            lstCommand[lstCommand.Count - 1] = ss;
                        }
                    }
                }
            }

            for (int i = 0; i < lstCommand.Count; i++)
            {
                //yjk, 19.05.10 - 위에서 Message 재구성 시 구분자를 "&*"로 변경
                string[] sSplit = lstCommand[i].Split(new string[] { "&*" }, StringSplitOptions.None);

                if (sSplit.Length == 0)
                    continue;

                if (sSplit[0] == EMTcpDDEAMessageType.Message.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.Message, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.SavedLogPath.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.SavedLogPath, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.CollectComp.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.CollectComp, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.CollectSpeed.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.CollectSpeed, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.CompTime.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.CompTime, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.CycleCount.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.CycleCount, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.CycleNumber.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.CycleNumber, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.CycleState.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.CycleState, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.ErrorSymbol.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.ErrorSymbol, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.PacketCount.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.PacketCount, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.PacketNumber.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.PacketNumber, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.State.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.State, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.StartTime.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.StartTime, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.TcpState.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.TcpState, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.UpmOpenSuccess.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.UpmOpenSuccess, sSplit[1]);
                else if (sSplit[0] == EMTcpDDEAMessageType.SymbolErrorChecked.ToString())
                    SendClientMessage(EMTcpDDEAMessageType.SymbolErrorChecked, sSplit[1]);
            }

            return true;
        }
    }
}
