using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;

    public void UpdateTimerText(int minutes, int seconds)
    {
        if (seconds < 10)
        {
            timerText.text = $"{minutes}:0{seconds}";
        }
        else
        {
            timerText.text = $"{minutes}:{seconds}";
        }
    }

}
