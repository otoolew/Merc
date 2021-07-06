using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/AI/Conditional/Actions Complete")]
public class ActionsPerformed : Conditional
{
    public override bool Evaluate(AIController controller)
    {
        return controller.CurrentState.ActionsCompleted();
    }
}
