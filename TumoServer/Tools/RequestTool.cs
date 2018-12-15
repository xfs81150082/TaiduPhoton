using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Photon.SocketServer;
using TumoPhoton;

namespace TumoCommon.Tools
{
    public class RequestTool
    {
        //这个方法用来转发请求
        public static void TransmitRequst(ClientPeer peer, OperationRequest request, OperationCode opCode)
        {
            foreach (ClientPeer temp in peer.Team.clientPeers)
            {
                if (temp != peer)
                {
                    EventData data = new EventData();
                    data.Parameters = request.Parameters;
                    ParameterTool.AddOperationcodeSubcodeRoleId(data.Parameters, opCode, peer.LoginRole.Id);
                    temp.SendEvent(data, new SendParameters());
                }
            }
        }




    }
}
