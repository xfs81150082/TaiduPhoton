using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TumoCommon;
using TumoCommon.Model;
using TumoCommon.Tools;
using TumoPhoton.DB.Manager;

namespace TumoPhoton.Handlers
{
    class SkillDBHandler : HandlerBase
    {
        private SkillDBManager skillDbManager;

        public SkillDBHandler()
        {
            skillDbManager = new SkillDBManager();
        }

        public override OperationCode OpCode
        {
            get { return OperationCode.SkillDB; }
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode = ParameterTool.GetSubcode(request.Parameters);
            ParameterTool.AddSubcode(response.Parameters,subCode);
            switch (subCode)
            {
                case SubCode.Add:
                    SkillDB skillDb = ParameterTool.GetParameter<SkillDB>(request.Parameters, ParameterCode.SkillDB);
                    skillDb.Role = peer.LoginRole;
                    skillDbManager.Add(skillDb);
                    skillDb.Role = null;
                    ParameterTool.AddParmeter(request.Parameters,ParameterCode.SkillDB,skillDb);
                    break;
                case SubCode.Update:
                    SkillDB skillDb2 = ParameterTool.GetParameter<SkillDB>(request.Parameters, ParameterCode.SkillDB);
                    skillDb2.Role = peer.LoginRole;
                    skillDbManager.Update(skillDb2);
                    break;
                case SubCode.Get:
                    List<SkillDB> list = skillDbManager.Get(peer.LoginRole);
                    foreach (var temp in list)
                    {
                        temp.Role = null;
                    }
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.SkillDBList,list);
                    break;
                case SubCode.Upgrade:
                    SkillDB skillDb3 = ParameterTool.GetParameter<SkillDB>(request.Parameters, ParameterCode.SkillDB);
                    Role role = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    role.User = peer.LoginUser;
                    skillDb3.Role = role;
                    skillDbManager.Upgrade(skillDb3,role);
                    skillDb3.Role = null;
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.SkillDB,skillDb3);
                    break;
                




            }


        }
    }
}
