using GoapCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSword : GoapAction
{
    GoapState state;
    List<WorldParamEffect> effects;

    public override GoapState Condition => state;

    public override List<WorldParamEffect> Effects => effects;

    public GetSword()
    {
        //条件
        state = new GoapState();
        state.SetWorldParam(new WorldParamBool() { constraint = WorldParam.ConstraintEnum.Equal, key = "附近有剑", value = true });
        //效果
        effects = new List<WorldParamEffect>();
        effects.Add(new WorldParamEffect() { effect = WorldParamEffect.EffectEnum.Set, param = new WorldParamBool() { key = "拥有剑", value = true } });
        effects.Add(new WorldParamEffect() { effect = WorldParamEffect.EffectEnum.Set, param = new WorldParamBool() { key = "附近有剑", value = false } });
    }
}
