using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilInRange : CustomYieldInstruction
{
    private readonly Transform origin;
    private readonly Vector3 destination;
    private readonly float range;
    public override bool keepWaiting => !(Vector3.Distance(origin.position, destination) <= range);

    public WaitUntilInRange(Transform origin, Vector3 destination, float range)
    {
        this.origin = origin;
        this.destination = destination;
        this.range = range;
    }
}
