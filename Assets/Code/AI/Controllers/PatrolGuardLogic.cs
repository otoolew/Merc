using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/AI/Logic/PatrolGuard")]
public class PatrolGuardLogic : ControllerLogic
{
    public override AIController Controller { get; set; }
    public override string Description { get; set; }
    public override void Init(AIController controller)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateTick()
    {
        throw new System.NotImplementedException();
    }
}
