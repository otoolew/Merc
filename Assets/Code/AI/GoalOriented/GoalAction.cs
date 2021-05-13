using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An action is something that the agent does.
/// </summary>
public abstract class GoalAction : MonoBehaviour
{
    /// <summary>
    /// The cost of an action. The lower cost has priority
    /// </summary>
    public abstract int Cost { get; set; }

    public abstract bool PreconditionsMet();
    // public abstract HashSet< KeyValuePair<string,object> > Preconditions;
    //
    // HashSet< KeyValuePair<string,object> > effects;
}
