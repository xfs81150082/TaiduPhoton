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
    public class InventoryItemDBHandler : HandlerBase
    {
        private InventoryItemDBManager inventoryItemDbManager=new InventoryItemDBManager();

        public override OperationCode OpCode
        {
            get { return OperationCode.InventoryItemDB; }
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode = ParameterTool.GetParameter<SubCode>(request.Parameters, ParameterCode.SubCode, false);
            ParameterTool.AddParmeter(response.Parameters,ParameterCode.SubCode,subCode,false);
            switch (subCode)
            {
                case SubCode.GetInventoryItemDB:
                    List<InventoryItemDB> list = inventoryItemDbManager.GetInventoryItemDB(peer.LoginRole);
                    foreach (var item in list)
                    {
                        item.Role = null;
                    }
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.InventoryItemDBList,list);
                    break;
                case SubCode.AddInventoryItemDB:
                    InventoryItemDB itemDb =
                        ParameterTool.GetParameter<InventoryItemDB>(request.Parameters, ParameterCode.InventoryItemDB);
                    itemDb.Role = peer.LoginRole;
                    inventoryItemDbManager.AddInventoryItemDB(itemDb);
                    itemDb.Role = null;
                    ParameterTool.AddParmeter(response.Parameters,ParameterCode.InventoryItemDB,itemDb);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
                case SubCode.UpdateInventoryItemDB:
                    InventoryItemDB itemDb2 =
                        ParameterTool.GetParameter<InventoryItemDB>(request.Parameters, ParameterCode.InventoryItemDB);
                    itemDb2.Role = peer.LoginRole;
                    inventoryItemDbManager.UpdateInventoryItemDB(itemDb2);
                    break;
                case SubCode.UpdateInventoryItemDBList:
                    List<InventoryItemDB> list2 =
                        ParameterTool.GetParameter<List<InventoryItemDB>>(request.Parameters,
                            ParameterCode.InventoryItemDBList);
                    foreach (var itemDB3 in list2)
                    {
                        itemDB3.Role = peer.LoginRole;
                    }
                    inventoryItemDbManager.UpdateInventoryItemDBList(list2);
                    break;
                case SubCode.UpgradeEquip:
                    InventoryItemDB itemDb4 =
                        ParameterTool.GetParameter<InventoryItemDB>(request.Parameters, ParameterCode.InventoryItemDB);
                    Role role = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    peer.LoginRole = role;
                    role.User = peer.LoginUser;
                    itemDb4.Role = role;
                    inventoryItemDbManager.UpgradeEquip(itemDb4, role);
                    break;

            }



        }
    }
}
