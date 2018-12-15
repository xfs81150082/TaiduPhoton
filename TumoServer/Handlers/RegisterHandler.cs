using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TumoCommon.Model;
using TumoCommon.Tools;
using TumoCommon;
using TumoPhoton.DB.Manager;
using TumoPhoton.Tools;

namespace TumoPhoton.Handlers
{
     class RegisterHandler : HandlerBase
    {
        private UserManager manager;

        public RegisterHandler()
        {
            manager = new UserManager();
        }

        public override void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer, SendParameters sendParameters)
        {
            User user = ParameterTool.GetParameter<User>(request.Parameters, ParameterCode.User);
            User userDB = manager.GetUserByUsernmae(user.Username);
            if (userDB != null)
            {
                response.ReturnCode = (short) ReturnCode.Fail;
                response.DebugMessage = "用户名重复";
            }
            else
            {
                user.Password = MD5Tool.GetMD5(user.Password);
                manager.AddUser(user);
                response.ReturnCode = (short) ReturnCode.Success;
            }
           

        }

        public override OperationCode OpCode { get { return OperationCode.Register; } }
    }
}
