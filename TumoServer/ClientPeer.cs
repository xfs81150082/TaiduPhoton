using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotonHostRuntimeInterfaces;
using TumoPhoton.Handlers;
using ExitGames.Logging;
using TumoCommon.Model;
using TumoPhoton.DB.Manager;

namespace TumoPhoton
{
    //用来和客户进行通信
    public class ClientPeer : PeerBase
    {
        public User LoginUser { get; set; }  //存储当前登录的user帐号
        public Role LoginRole { get; set; }  //存储当前登录的role
        public Team Team { get; set; }       //存储当前登录role所在的队伍

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        

        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {

        }

        //与服务器断开时调用
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            if (TumoApplication.Instance.clientPeerListFotTeam.Contains(this))
            {
                TumoApplication.Instance.clientPeerListFotTeam.Remove(this);
            }
            log.Debug("A client is disconnect.");
        }

        //当客户端发起请求时调用
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //这个方法用来给客户端响应
            HandlerBase handler;
            TumoApplication.Instance.handlers.TryGetValue(operationRequest.OperationCode, out handler);
            OperationResponse response = new OperationResponse();
            response.OperationCode = operationRequest.OperationCode;
            response.Parameters = new Dictionary<byte, object>();
            if (handler != null)
            {
                handler.OnHandlerMessage(operationRequest,response,this,sendParameters);
                SendOperationResponse(response, sendParameters);
            }
            else
            {
               log.Debug("Can't find handler from operation code." +operationRequest.OperationCode );
            }
        }
    }
}
