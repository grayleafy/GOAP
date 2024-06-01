using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AStarCore
{
    public interface IAStarEdge
    {
        float GetCost();

        IAStarNode GetNextNode(IAStarNode currentNode);
    }
}

