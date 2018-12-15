 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using Photon.SocketServer;
using TumoCommon;
using TumoPhoton.DB.Manager;
using TumoCommon.Model;
using ExitGames.Logging;

namespace TumoPhoton.Handlers
{
    class ServerHandler : HandlerBase
    {
        private ServerPropertyManager manager;       
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public ServerHandler()
        {
            manager =new ServerPropertyManager();
            
        }

        //当接受到客户端请求时，调用来处理服务器列表
        public override void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer, SendParameters sendParameters)
        {
            List<ServerProperty> list = manager.GetServerList();
            log.Debug(list.Count);
            string json = JsonMapper.ToJson(list);
            Dictionary<byte, object> parameters = response.Parameters;
            parameters.Add((byte) ParameterCode.ServerList,json);           
            response.OperationCode = request.OperationCode;
        }

        public override OperationCode OpCode { get { return OperationCode.GetServer; } }
    }
}
