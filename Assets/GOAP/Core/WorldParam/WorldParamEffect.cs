using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoapCore
{
    public class WorldParamEffect
    {
        public enum EffectEnum
        {
            Set,
            Add,
            Subtract,
        }
        public EffectEnum effect;
        public WorldParam param;

        /// <summary>
        /// 执行影响
        /// </summary>
        /// <param name="originParam"></param>
        public void DoEffect(WorldParam originParam)
        {
            if (effect == EffectEnum.Set)
            {
                originParam.ValueObj = param.ValueObj;
                return;
            }
            if (effect == EffectEnum.Add)
            {
                originParam.ValueObj = (originParam as IAdd).Add(param).ValueObj;
            }
            if (effect == EffectEnum.Subtract)
            {
                originParam.ValueObj = (originParam as ISubtract).Subtract(param).ValueObj;
            }
        }
    }
}

