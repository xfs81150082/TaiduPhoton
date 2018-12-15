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
    class BossHandler : HandlerBase
    {
        public override OperationCode OpCode
        {
            get { return OperationCode.Boss; }
        }



        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode = ParameterTool.GetSubcode(request.Parameters);
            switch (subCode)
            {
                case SubCode.SyncBossAnimation:
                    RequestTool.TransmitRequst(peer,request,OpCode);
                    break;
            }
        }
    }
}
