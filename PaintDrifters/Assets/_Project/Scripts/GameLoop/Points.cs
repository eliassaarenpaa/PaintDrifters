using UnityEngine;

public class Points : MonoBehaviour
{

    [SerializeField] private int playerOnePoints;
    [SerializeField] private int playerTwoPoints;

    public void AddPoints(int playerTag, int points)
    {
        if (playerTag.Equals(1))
        {
            playerOnePoints += points;
        }
        else if (playerTag.Equals(2))
        {
            playerTwoPoints += points;
        }
    }

    public int GetWinner()
    {
        if (playerOnePoints > playerTwoPoints)
        {
            return 1;
        }
        return 2;
    }

}
