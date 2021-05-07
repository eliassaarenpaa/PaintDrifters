using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] private UnityEvent onGameEnd;

    public void EndGame()
    {
        onGameEnd?.Invoke();
    }

}
