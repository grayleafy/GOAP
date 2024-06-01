using AStarCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace GoapCore
{


    /// <summary>
    /// 状态
    /// </summary>
    public class GoapState : IAStarNode
    {
        //代理
        public GoapAgent agent;
        //世界状态
        Dictionary<string, WorldParam> states = new();

        public WorldParam GetWorldParam(string key)
        {
            if (states.ContainsKey(key))
            {
                return states[key];
            }
            return null;
        }
        public void SetWorldParam(WorldParam value)
        {
            states[value.key] = value;
        }

        /// <summary>
        /// 另一个状态是否满足自己的条件
        /// </summary>
        /// <param name="goapState"></param>
        /// <returns></returns>
        public bool IsFullfill(GoapState goapState)
        {
            foreach (WorldParam worldParam in states.Values)
            {
                WorldParam p = goapState.GetWorldParam(worldParam.key);
                //自己有但是对方没有这个参数，不满足
                if (p == null)
                {
                    return false;
                }
                if (worldParam.IsFulfill(p) == false)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 克隆自己
        /// </summary>
        /// <returns></returns>
        public GoapState Clone()
        {
            GoapState newState = new GoapState();
            newState.agent = agent;
            foreach (var pair in states)
            {
                newState.states.Add(pair.Key, pair.Value.Clone());
            }
            return newState;
        }

        #region AStar
        public List<IAStarEdge> GetEdges()
        {
            List<IAStarEdge> actions = new List<IAStarEdge>();
            foreach (var goapAction in agent.goapActions)
            {
                if (goapAction.Condition.IsFullfill(this))
                {
                    actions.Add(goapAction);
                }
            }
            return actions;
        }

        public float GetHeuristicCost(IAStarNode start, IAStarNode goal)
        {
            float cost = 0;
            GoapState goalState = goal as GoapState;
            foreach (WorldParam param in states.Values)
            {
                var goalParam = goalState.GetWorldParam(param.key);
                if (goalParam == null) continue;
                cost += Mathf.Abs(param.Difference(goalParam));
            }
            return cost;
        }

        public bool IsFulfill(IAStarNode node)
        {
            return IsFullfill(node as GoapState);
        }
        #endregion


    }

}
