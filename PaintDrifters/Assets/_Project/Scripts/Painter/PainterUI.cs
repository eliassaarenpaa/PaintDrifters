#region
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class PainterUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image paintAmountImg;

    public void UpdateNameUI( string name ) {
        playerName.text = name;
    }

    public void UpdatePercentageUI( float percentage ) {
        paintAmountImg.fillAmount = Mathf.Clamp01( percentage );
    }

}