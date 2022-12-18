using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void EnterState(AIController controller);
    void UpdateState(AIController controller);
    void ExitState(AIController controller);
}
