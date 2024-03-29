using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandomPosition : ActionNode
{
    public Vector2 min = Vector2.one * -10;
    public Vector2 max = Vector2.one * 10;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() 
    {
        blackboard.SetVector3KeyValue("MovePosition",new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y)));

        return State.Success;
    }
}
