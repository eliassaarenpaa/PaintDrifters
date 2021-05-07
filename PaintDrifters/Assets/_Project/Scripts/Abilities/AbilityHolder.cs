using UnityEngine;

public class AbilityHolder : MonoBehaviour
{

    [SerializeField] private Ability ability;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
    }

}
