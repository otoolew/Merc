using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : GoalAction
{
    public override bool PrePerform()
    {
        Debug.Log("PrePerform");
        return true;
    }

    public override bool PostPerform() 
    {
        // Remove a patient from the world
        // GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        // if (target) {
        //
        //     target.GetComponent<GAgent>().inventory.AddItem(resource);
        // }
        Debug.Log("PrePerform");
        return true;
    }
}
