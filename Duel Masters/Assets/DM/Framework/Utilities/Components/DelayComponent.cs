using UnityEngine;
using UnityEngine.Events;

public class DelayComponent : MonoBehaviour
{
    [SerializeField]
    public UnityEvent timerStarted;

    [SerializeField]
    public UnityEvent timerFinished;

    [SerializeField]
    public float targetTime;

    private float currentTime;
    private bool running = false;

    public void OnDisable()
    {
        running = false;
        currentTime = 0;
    }

    public void StartTimer()
    {
        running = true;
        currentTime = 0;
        timerStarted.Invoke();
    }

    public void FinishTimer()
    {
        running = false;
        currentTime = 0;
        timerFinished.Invoke();
    }

    private void Run(float deltaTime)
    {
        if(running)
        {
            currentTime += deltaTime;
            if(currentTime >= targetTime)
            {
                FinishTimer();
            }
        }
    }

    private void Update()
    {
        Run(Time.deltaTime);
    }
}
