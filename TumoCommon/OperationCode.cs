 using System;
using System.Collections.Generic;
using System.Text;

namespace TumoCommon
{
    public enum OperationCode:Byte
    {
        Login,
        GetServer,
        Register,
        Role,
        TumoRole,
        TaskDB,
        InventoryItemDB,
        SkillDB,
        Battle,
        Enemy,
        Boss
    }
}
