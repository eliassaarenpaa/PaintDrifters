#region
using UnityEngine;
using UnityEngine.Events;
#endregion

public class WASDInput : MonoBehaviour, IPlayerInputs {

    public int InputDirection { get; set; }
    public bool IsDrifting { get; set; }
    public UnityAction OnAbilityUsedAction { get; set; }

    private void Update() {

        InputDirection = (int) Input.GetAxisRaw( "AD_Horizontal" );
        IsDrifting = Input.GetButton( "S_Button" );
        if ( Input.GetButtonDown( "W_Button" ) ) OnAbilityUsedAction?.Invoke();

    }

}