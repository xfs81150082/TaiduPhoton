using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace TumoCommon.Tools
{
    public class ParameterTool
    {
        public static T GetParameter<T>(Dictionary<byte, object> parameters, ParameterCode parameterCode, bool isObject = true) 
        {
            object o = null;
            parameters.TryGetValue((byte)parameterCode, out o);
            if (isObject == false) 
            {
                return (T) o;
            }
            return JsonMapper.ToObject<T>(o.ToString());
        }


        public static void AddParmeter<T>(Dictionary<byte, object> parameters, ParameterCode key, T value,
            bool isObject = true)
        {
            if (isObject)
            {
                string json = JsonMapper.ToJson(value);
                parameters.Add((byte) key,json);
            }
            else
            {
                parameters.Add((byte) key,value);
            }
        }

        public static SubCode GetSubcode(Dictionary<byte, object> parameters)
        {
            return GetParameter<SubCode>(parameters, ParameterCode.SubCode, false);
        }

        public static void AddSubcode(Dictionary<byte, object> parameters, SubCode subCode)
        {
            AddParmeter<SubCode>(parameters,ParameterCode.SubCode,subCode,false);

        }

        public static void AddOperationcodeSubcodeRoleId(Dictionary<byte, object> parameters, OperationCode opCode,
             int roleId)
        {
            if (parameters.ContainsKey((byte) ParameterCode.OperationCode))
            {
                parameters.Remove((byte) ParameterCode.OperationCode);
            }
            parameters.Add((byte) ParameterCode.OperationCode,opCode);
            if (parameters.ContainsKey((byte) ParameterCode.RoleId))
            {
                parameters.Remove((byte) ParameterCode.RoleId);
            }
            parameters.Add((byte) ParameterCode.RoleId,roleId);

        }




    }
}
