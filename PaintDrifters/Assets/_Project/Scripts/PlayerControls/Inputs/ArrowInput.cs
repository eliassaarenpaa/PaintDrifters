#region
using UnityEngine;
using UnityEngine.Events;
#endregion

public class ArrowInput : MonoBehaviour, IPlayerInputs {

    public int InputDirection { get; set; }
    public bool IsDrifting { get; set; }
    public UnityAction OnAbilityUsedAction { get; set; }

    private void Update() {

        InputDirection = (int) Input.GetAxisRaw( "ArrowHorizontal" );
        IsDrifting = Input.GetButton( "DownArrow" );
        if ( Input.GetButtonDown( "UpArrow" ) ) OnAbilityUsedAction?.Invoke();

    }

}