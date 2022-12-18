using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    [field: SerializeReference] public List<IVariable> VariableList { get; set; }
    public void EnterState(AIController controller)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(AIController controller)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState(AIController controller)
    {
        throw new System.NotImplementedException();
    }
}
