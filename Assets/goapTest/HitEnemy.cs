using GoapCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : GoapAction
{
    GoapState state;
    List<WorldParamEffect> effects;

    public override GoapState Condition => state;

    public override List<WorldParamEffect> Effects => effects;

    public HitEnemy()
    {
        //条件
        state = new GoapState();
        state.SetWorldParam(new WorldParamBool() { constraint = WorldParam.ConstraintEnum.Equal, key = "拥有剑", value = true });
        //效果
        effects = new List<WorldParamEffect>();
        effects.Add(new WorldParamEffect() { effect = WorldParamEffect.EffectEnum.Subtract, param = new WorldParamInt() { key = "敌人生命值", value = 30 } });
    }
}
