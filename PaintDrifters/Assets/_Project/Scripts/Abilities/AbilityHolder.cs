using UnityEngine;

public class AbilityHolder : MonoBehaviour
{

    [SerializeField] private Ability ability;

    private void OnTriggerEnter(Collider other)
    {
        ability.ActivateAbility();
        Destroy(gameObject);
    }

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
    }

}
