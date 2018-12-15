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
    public class TaskDBHandler : HandlerBase
    {
        private TaskDBManager taskDbManager;

        public TaskDBHandler()
        {
            taskDbManager = new TaskDBManager();
        }

        public override OperationCode OpCode
        {
            get { return OperationCode.TaskDB; }
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode = ParameterTool.GetParameter<SubCode>(request.Parameters, ParameterCode.SubCode, false);
            response.Parameters.Add((byte) ParameterCode.SubCode,subCode);
            switch (subCode)
            {
                case SubCode.AddTaskDB:
                    TaskDB taskDB = ParameterTool.GetParameter<TaskDB>(request.Parameters, ParameterCode.TaskDB);
                    taskDB.Role = peer.LoginRole;
                    taskDbManager.AddTaskDB(taskDB);
                    taskDB.Role = null;
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.TaskDB,taskDB);
                    response.ReturnCode = (short)ReturnCode.Success;
                    break;
                case SubCode.GetTaskDB:
                    List<TaskDB> list = taskDbManager.GeTaskDbList(peer.LoginRole);
                    foreach (var taskDb in list)
                    {
                        taskDb.Role = null;
                    }
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.TaskDBList,list);
                    response.ReturnCode = (short)ReturnCode.Success;
                    break;
                case SubCode.UpdateTaskDB:
                    TaskDB taskDB2 = ParameterTool.GetParameter<TaskDB>(request.Parameters, ParameterCode.TaskDB);
                    taskDB2.Role = peer.LoginRole;
                    taskDbManager.UpdateTaskDB(taskDB2);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;


            }


        }
    }
}
