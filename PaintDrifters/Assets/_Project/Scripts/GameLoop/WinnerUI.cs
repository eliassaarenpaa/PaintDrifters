using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{

    [SerializeField] private GameObject neither;
    [SerializeField] private GameObject redTextObj;
    [SerializeField] private GameObject blueTextObj;
    //[SerializeField] private TextMeshProUGUI winnerText;

    private Points _points;

    private void Awake()
    {
        _points = GetComponent<Points>();
    }

    public void AnnounceTheWinner()
    {
        var winner = _points.GetWinner();

        if ( winner <= 0 ) {
            neither.SetActive( true );
            redTextObj.SetActive( false );
            blueTextObj.SetActive( false );
            return;
        }
        
        var isPlayer1 = winner == 1;
        
        redTextObj.SetActive( isPlayer1 );
        blueTextObj.SetActive( !isPlayer1 );
        
        // winnerText.text = $"Aaand the winner iiiis player {winner}!";

    }

}
