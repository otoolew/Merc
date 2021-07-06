using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/AI/Conditional/Target In Sight")]
public class TargetInSight : Conditional
{
    public override bool Evaluate(AIController controller)
    {
        return controller.AssignedCharacter.VisionPerception.HasTarget;
    }
}
