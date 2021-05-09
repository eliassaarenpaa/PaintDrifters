using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float seconds;

    [SerializeField] private float currentSeconds;

    [SerializeField] private int currentMinutes;

    [SerializeField] private UnityEvent onTimeEnd;

    private TimerUI _timerUI;

    private bool _isActive;

    private void Awake()
    {
        _timerUI = GetComponent<TimerUI>();
    }

    private void Start()
    {
        currentMinutes = (int)(seconds / 60);
        currentSeconds = seconds % 60;
        _isActive = true;
    }

    private void Update()
    {
        currentSeconds = Mathf.Clamp(currentSeconds -= 1 * Time.deltaTime, 0, 60);
        if (currentSeconds == 0 && currentMinutes > 0)
        {
            currentSeconds = 60;
            currentMinutes--;
        }
        _timerUI.UpdateTimerText(currentMinutes, (int)currentSeconds);
        if (currentSeconds == 0 && currentMinutes == 0 && _isActive)
        {
            onTimeEnd?.Invoke();
            _isActive = false;
        }
    }
}
