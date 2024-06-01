using AStarCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoapCore
{
    public abstract class GoapAction : IAStarEdge
    {
        /// <summary>
        /// 条件
        /// </summary>
        public abstract GoapState Condition { get; }
        /// <summary>
        /// 对世界参数的影响
        /// </summary>
        public abstract List<WorldParamEffect> Effects { get; }
        public float cost;

        /// <summary>
        /// 消耗
        /// </summary>
        /// <returns></returns>
        public float GetCost()
        {
            return cost;
        }

        /// <summary>
        /// 得到AStar的下一个节点
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IAStarNode GetNextNode(IAStarNode currentNode)
        {
            return GetNextState(currentNode as GoapState);
        }
        /// <summary>
        /// 得到执行动作后的下一个节点
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        private GoapState GetNextState(GoapState currentState)
        {
            GoapState nextState = currentState.Clone();
            foreach (var effect in Effects)
            {
                WorldParam param = nextState.GetWorldParam(effect.param.key);
                if (param != null)
                {
                    effect.DoEffect(param);
                }
                else
                {
                    Debug.LogError($"行动执行会影响一个不存在的世界参数: {this.GetType().Name}");
                }
            }
            return nextState;
        }

    }


}
