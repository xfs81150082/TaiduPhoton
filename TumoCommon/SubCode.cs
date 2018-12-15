using System;
using System.Collections.Generic;
using System.Text;

namespace TumoCommon
{
    public enum SubCode
    {
        GetRole,
        GetTumoRole,
        UpdateTumoRole,
        AddRole,
        SelectRole,
        UpdateRole,
        AddTaskDB,
        UpdateTaskDB,
        GetTaskDB,
        GetInventoryItemDB,
        AddInventoryItemDB,
        UpdateInventoryItemDB,
        UpdateInventoryItemDBList,
        UpgradeEquip,
        Add,
        Update,
        Get,
        Upgrade,
        SendTeam,    //组队请求
        CancelTeam,  //取消组队
        GetTeam,      //组队成功
        SyncPositionAndRotation,  //同步位置和旋转
        SyncMoveAnimation,  //同步移动动画
        CreateEnemy,        //创建敌人产生   
        SyncAnimation,      //同步敌人动画
        SendGameState,
        SyncBossAnimation,

    }
}
