using System;
using System.Collections.Generic;
using System.Text;

namespace TumoCommon
{
     public enum ParameterCode : byte
    {
        ServerList,
        User,
        RoleList,
        Role,
        TumoRole,
        TumoRoleList,
        GetTumoRole,
        UpdateTumoRole,
        SubCode,
        OperationCode,
        TaskDB,
        TaskDBList,
        InventoryItemDBList,
        InventoryItemDB,
        SkillDBList,
        SkillDB,
        MasterRoleId,
        Position,      //位置
        EulerAngles,    //旋转
        RoleId,          //角色的Id，表示是更新的哪一个客户端   
        IsMove,
        PlayerMoveAnimationModel,
        CreateEnemyModel,
        EnemyPositionModel,
        EnemyAnimationModel,
        PlayerAnimationModel,
        GameSteteModel,
        BossAnimationModel,

    }
}
