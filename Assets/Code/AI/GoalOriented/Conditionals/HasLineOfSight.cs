using UnityEngine;

public class HasLineOfSight : Conditional
{
    public override bool ConditionMet(AIController controller)
    {
        return controller.AssignedCharacter.VisionPerception.HasLineOfSight();
    }
}
