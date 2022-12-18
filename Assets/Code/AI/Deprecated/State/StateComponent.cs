using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu (menuName = "Game/AI/State")]
public class StateComponent : ScriptableObject
{
    [SerializeField] private bool repeatActions;
    public bool RepeatActions { get => repeatActions; set => repeatActions = value; }
    
    [SerializeField] private StateAction[] actions;
    public StateAction[] Actions { get => actions; set => actions = value; }
    
    [SerializeField] private Transition[] transitions;
    public Transition[] Transitions { get => transitions; set => transitions = value; }
    
    [SerializeField] private Color color;
    public Color Color { get => color; set => color = value; }

    private void PerformActions(AIController controller)
    {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].Perform(controller);
        }
    }
    
    // private void CheckTransitions(AIController controller)
    // {
    //     for (int i = 0; i < transitions.Length; i++)
    //     {
    //         bool decisionSucceeded = transitions[i].Conditional.Evaluate(controller);
    //
    //         controller.TransitionToState(decisionSucceeded
    //             ? transitions[i].TrueEvaluationState
    //             : transitions[i].FalseEvaluationState);
    //     }
    // }

    public bool ActionsCompleted()
    {
        return actions.Length <= 0 || Actions.All(t => t.IsComplete);
    }
}
