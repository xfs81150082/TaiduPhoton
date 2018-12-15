using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using Photon.SocketServer;
using TumoCommon;
using TumoCommon.Model;
using TumoCommon.Tools;
using TumoPhoton.DB.Manager;

namespace TumoPhoton.Handlers
{
     public class RoleHandler : HandlerBase
     {
        private RoleManager roleManager = null;
        private UserManager userManager = null;
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();


        public RoleHandler()
        {
            roleManager = new RoleManager();
            userManager = new UserManager();
        }

        public override void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer, SendParameters sendParameters)
        {
            //先得到子操作代码，根据子操作代码，分别进行不同的处理
            SubCode subCode = ParameterTool.GetParameter<SubCode>(request.Parameters,ParameterCode.SubCode,false);

            Dictionary<byte, object> parameters = response.Parameters;
            parameters.Add((byte) ParameterCode.SubCode,subCode);
            response.OperationCode = request.OperationCode;
            switch (subCode)
            {
                case SubCode.GetRole:
                    List<Role> rolelList = roleManager.GetRoleListByUser(peer.LoginUser);
                    foreach (var role1 in rolelList)
                    {
                        role1.User = null;
                    }
                    ParameterTool.AddParmeter(parameters,ParameterCode.RoleList,rolelList);
                    break;                    
                case SubCode.AddRole:
                    Role role = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role); //request ?? response 互换实验看看 ？？request正确
                    role.User = peer.LoginUser;
                    roleManager.AddRole(role);
                    role.User = null;
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.Role,role);
                    break;
                case SubCode.SelectRole:
                    peer.LoginRole = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    break;
                case SubCode.UpdateRole:
                    Role role2 = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    role2.User = peer.LoginUser;
                    roleManager.UpdateRole(role2);
                    role2.User = null;
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
                case SubCode.GetTumoRole:
                    User user = ParameterTool.GetParameter<User>(request.Parameters, ParameterCode.User);
                    log.Debug("Role63" + user.Username);
                    User user1 = userManager.GetUserByUsernmae(user.Username);
                    List<Role> tumoRolelList = roleManager.GetRoleListByUser(user1);
                    foreach (var role1 in tumoRolelList)
                    {
                        role1.User = null;
                    }
                    ParameterTool.AddParmeter(parameters, ParameterCode.TumoRoleList, tumoRolelList);
                    log.Debug("Role70" + tumoRolelList[0].Name);
                    break;

            }
           
        }

         public override OperationCode OpCode
         {
             get  {  return OperationCode.Role;  }
         }
     }
}
