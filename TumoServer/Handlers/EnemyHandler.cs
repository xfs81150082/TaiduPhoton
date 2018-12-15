using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TumoCommon;
using TumoCommon.Tools;

namespace TumoPhoton.Handlers
{
    class EnemyHandler : HandlerBase
    {
        public override OperationCode OpCode
        {
            get { return OperationCode.Enemy; }
        }



        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode = ParameterTool.GetSubcode(request.Parameters);
            switch (subCode)
            {
                case SubCode.CreateEnemy:
                    TransmitRequst(peer, request);
                    break;
                case SubCode.SyncPositionAndRotation:
                    TransmitRequst(peer, request);
                    break;
                case SubCode.SyncAnimation:
                    TransmitRequst(peer,request);
                    break;

            }
        }

        //这个方法用来转发请求
        void TransmitRequst(ClientPeer peer,OperationRequest request)
        {
            foreach (ClientPeer temp in peer.Team.clientPeers)
            {
                if (temp != peer)
                {
                    EventData data = new EventData();
                    data.Parameters = request.Parameters;
                    ParameterTool.AddOperationcodeSubcodeRoleId(data.Parameters, OpCode, peer.LoginRole.Id);
                    temp.SendEvent(data, new SendParameters());
                }
            }
        }






    }
}
