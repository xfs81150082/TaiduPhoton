using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TumoCommon;
using TumoCommon.Model;
using TumoCommon.Tools;

namespace TumoPhoton.Handlers
{
    class BattleHandler : HandlerBase
    {
        public override OperationCode OpCode
        {
            get { return OperationCode.Battle; }
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode = ParameterTool.GetSubcode(request.Parameters);
            ParameterTool.AddSubcode(response.Parameters,subCode);
            switch (subCode)
            {
                case SubCode.SendTeam:
                    if (TumoApplication.Instance.clientPeerListFotTeam.Count>=2)
                    {
                        //取得list中的前二个peer 跟当前的peer进行组队  toto
                        ClientPeer peer1 = TumoApplication.Instance.clientPeerListFotTeam[0];
                        ClientPeer peer2 = TumoApplication.Instance.clientPeerListFotTeam[1];
                        Team t = new Team(peer1,peer2,peer);
                        TumoApplication.Instance.clientPeerListFotTeam.RemoveRange(0,2);
                        List< Role> roleList= new List<Role>();
                        foreach (var clientPeer in t.clientPeers)
                        {
                            roleList.Add(clientPeer.LoginRole);
                        }
                        ParameterTool.AddParmeter(response.Parameters,ParameterCode.RoleList,roleList);
                        ParameterTool.AddParmeter(response.Parameters, ParameterCode.MasterRoleId, t.masterRoleId,
                            false);
                        response.ReturnCode = (short) ReturnCode.GetTeam;

                        SendEventByPeer(peer1, (OperationCode) response.OperationCode, SubCode.GetTeam,roleList,t.masterRoleId); //不知道对不对？
                        SendEventByPeer(peer2, (OperationCode) response.OperationCode, SubCode.GetTeam,roleList,t.masterRoleId); //不知道对不对？
                    }
                    else
                    {
                        //当当前的服器可供组队的客户端不足的时候，把自身加到集合中等 等组队
                        TumoApplication.Instance.clientPeerListFotTeam.Add(peer);
                        response.ReturnCode = (short) ReturnCode.WartingTeam;
                    }
                    break;
                case SubCode.CancelTeam:
                    TumoApplication.Instance.clientPeerListFotTeam.Remove(peer);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
                case SubCode.SyncPositionAndRotation:
                    object posObj = null;
                    request.Parameters.TryGetValue((byte) ParameterCode.Position, out posObj);
                    object eulerAnglesObj = null;
                    request.Parameters.TryGetValue((byte) ParameterCode.EulerAngles, out eulerAnglesObj);
                    foreach (ClientPeer temp in peer.Team.clientPeers)
                    {
                        if (temp != peer)
                        {
                            SendEventByPeer(temp, OpCode, SubCode.SyncPositionAndRotation, peer.LoginRole.Id, posObj,
                                eulerAnglesObj);
                        }
                    }
                    break;
                case SubCode.SyncMoveAnimation:
                    foreach (ClientPeer temp in peer.Team.clientPeers)
                    {
                        if (temp != peer)
                        {
                            SendMoveAnimationEvent(temp,OpCode,SubCode.SyncMoveAnimation,peer.LoginRole.Id,request.Parameters);
                        }
                    }
                    break;
                case SubCode.SyncAnimation:
                    request.Parameters.Add((byte) ParameterCode.RoleId,peer.LoginRole.Id);
                    RequestTool.TransmitRequst(peer,request,OpCode);
                    break;
                case SubCode.SendGameState:
                    RequestTool.TransmitRequst(peer,request,OpCode);
                    peer.Team.Dismiss();  //解散队伍
                    break;

            }
        }

        //
        void SendMoveAnimationEvent(ClientPeer peer, OperationCode opCode, SubCode subCode, int roleid, Dictionary<byte,object> parameters)
        {
            EventData data = new EventData();
            data.Parameters = parameters;
            ParameterTool.AddOperationcodeSubcodeRoleId(parameters,opCode,roleid);
            peer.SendEvent(data, new SendParameters());

        }

        //向客户端发送 位置和旋转的数据 进行同步
        void SendEventByPeer(ClientPeer peer,OperationCode opCode,SubCode subCode,int roleid,object posObj,object eulerAmglesObj)
        {
            EventData data= new EventData();
            data.Parameters = new Dictionary<byte, object>();
            ParameterTool.AddParmeter(data.Parameters,ParameterCode.OperationCode,opCode,false);
            ParameterTool.AddParmeter(data.Parameters,ParameterCode.SubCode,subCode,false);
            data.Parameters.Add((byte) ParameterCode.RoleId,roleid);
            data.Parameters.Add((byte) ParameterCode.Position,posObj.ToString());
            data.Parameters.Add((byte) ParameterCode.EulerAngles,eulerAmglesObj);
            peer.SendEvent(data, new SendParameters());
        }

        void SendEventByPeer(ClientPeer peer,OperationCode opCode,SubCode subCode ,List<Role> rolelList,int masterRoleId)
        {
            //OperationResponse response=new OperationResponse();
            //response.Parameters=new Dictionary<byte, object>();
            //ParameterTool.AddSubcode(response.Parameters,subCode);
            //ParameterTool.AddParmeter(response.Parameters,ParameterCode.RoleList,rolelList);
            //response.ReturnCode = (short) ReturnCode.GetTeam;
            //peer.SendOperationResponse(response, sendParameters);

            EventData eventData = new EventData();
            eventData.Parameters = new Dictionary<byte, object>();
            ParameterTool.AddParmeter(eventData.Parameters, ParameterCode.OperationCode, opCode, false);
            ParameterTool.AddSubcode(eventData.Parameters, subCode);
            ParameterTool.AddParmeter(eventData.Parameters, ParameterCode.RoleList, rolelList);
            ParameterTool.AddParmeter(eventData.Parameters, ParameterCode.MasterRoleId, masterRoleId, false);

            peer.SendEvent(eventData, new SendParameters());

        }

    }
}
