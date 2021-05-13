using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerComponent : MonoBehaviour
{    
    [SerializeField] private TimerStatus status;
    public TimerStatus Status { get => status; set => status = value; }
    
    [SerializeField] private float startCount;
    public float StartCount { get => startCount; set => startCount = value; }
    
    [SerializeField,DisableFloat] private float elapsedTime;
    public float ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
    
    [SerializeField] private bool loopTimer;
    public bool LoopTimer { get => loopTimer; set => loopTimer = value; }
    
    public UnityAction OnComplete; 
    
    // Start is called before the first frame update
    public void StartTimer()
    {
        StartCoroutine(StartTimerRoutine());
    }
    
    public void StopTimer()
    {
        status = TimerStatus.STOPPED;
    }
    
    public void ResetTimer()
    {
        elapsedTime = startCount;
    }
    
    IEnumerator StartTimerRoutine()
    {
        if (status == TimerStatus.RUNNING)
        {
            elapsedTime -= Time.deltaTime;
        }
        yield return new WaitUntil(() => elapsedTime <= 0);
        
        if (loopTimer)
        {
            ResetTimer();
            status = TimerStatus.RUNNING;
        }
        else
        {
            status = TimerStatus.COMPLETED;
        }
        while (elapsedTime > 0)
        {
            yield return new WaitUntil(() => elapsedTime <= 0);
        }
    }
    
    private void TimerFinished()
    {
        OnComplete?.Invoke();
    }
    
}
