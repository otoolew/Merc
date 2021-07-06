using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Game/AI/Action/Wait Action")]
public class WaitAction : StateAction
{
    [SerializeField] private float waitTime;
    public float WaitTime { get => waitTime; set => waitTime = value; }
    
    [SerializeField] private bool isComplete;
    public override bool IsComplete { get => isComplete; set => isComplete = value; }
    public override void Perform(AIController controller)
    {
        controller.StopAllCoroutines();
        controller.StartCoroutine(WaitRoutine());
    }
    public override UnityAction OnActionComplete { get; set; }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(waitTime);
        isComplete = true;
        OnActionComplete?.Invoke();
    }
}
