using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Speed Up")]
public class SpeedUp : Ability
{

    [SerializeField] private float speed;
    [SerializeField] private float abilityTime;

    public override void ActivateAbility(CarController controller)
    {

        GameManager.Instance.StartChildCoroutine(StartAbility(controller));

    }

    private IEnumerator StartAbility(CarController controller)
    {
        controller.SetSpeed(speed);
        yield return new WaitForSeconds(abilityTime);
        controller.ResetSpeed();
    }

}
