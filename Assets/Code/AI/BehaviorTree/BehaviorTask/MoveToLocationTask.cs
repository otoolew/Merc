using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/AI/MoveTo", fileName = "newMoveToLocationTask")]
public class MoveToLocationTask : BehaviorTask
{
    public override bool PreConditionsMet()
    {
        return true;
    }

    public override bool Perform()
    {
        return true;
    }

    public override bool Finish()
    {
        return true;
    }
    
}
