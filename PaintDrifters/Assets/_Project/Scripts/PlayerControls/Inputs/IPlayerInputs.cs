#region
using UnityEngine.Events;
#endregion

public interface IPlayerInputs {

    int InputDirection { get; }
    bool IsDrifting { get; }
    UnityAction OnAbilityUsedAction { get; set; }

}