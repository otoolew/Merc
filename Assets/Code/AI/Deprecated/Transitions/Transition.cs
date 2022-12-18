using System;
using UnityEngine;
[Serializable]
public class Transition
{
    [SerializeField] private Conditional conditional;
    public Conditional Conditional { get => conditional; set => conditional = value; }

    [SerializeField] private StateComponent trueEvaluationState;
    public StateComponent TrueEvaluationState { get => trueEvaluationState; set => trueEvaluationState = value; }
    
    [SerializeField] private StateComponent falseEvaluationState;
    public StateComponent FalseEvaluationState { get => falseEvaluationState; set => falseEvaluationState = value; }
}
