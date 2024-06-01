using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoapCore
{
    public interface IWorldParamOperator
    {

    }
    /// <summary>
    /// 两个参数的差距，用于计算启发式cost, 值为 {自己 - param}
    /// </summary>
    public interface IDifference : IWorldParamOperator
    {
        float Difference(WorldParam param);
    }

    public interface IAdd : IWorldParamOperator
    {
        WorldParam Add(WorldParam param);
    }

    public interface ISubtract : IWorldParamOperator
    {
        WorldParam Subtract(WorldParam param);
    }

}

