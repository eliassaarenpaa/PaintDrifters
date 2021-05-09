using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI winnerText;

    private Points _points;

    private void Awake()
    {
        _points = GetComponent<Points>();
    }

    public void AnnounceTheWinner()
    {

        var winner = _points.GetWinner();

        winnerText.text = $"Aaand the winner iiiis player {winner}!";

    }

}
