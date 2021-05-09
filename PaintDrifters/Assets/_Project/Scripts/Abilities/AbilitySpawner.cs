using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{

    [SerializeField] private Ability[] abilities;

    [SerializeField] private GameObject abilityBox;

    [SerializeField] private float minSpawnTime;

    [SerializeField] private float maxSpawnTime;

    [SerializeField] private float currentSpawnTime;

    [SerializeField] private int minZ;
    [SerializeField] private int minX;
    [SerializeField] private int maxZ;
    [SerializeField] private int maxX;

    private void Start()
    {
        GenerateNewTime();
    }

    private void SpawnAbility()
    {
        var selectedAbility = abilities[Random.Range(0, abilities.Length)];
        GameObject newAbility = Instantiate(abilityBox);
        newAbility.transform.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        newAbility.GetComponent<AbilityHolder>().SetAbility(selectedAbility);
        GenerateNewTime();
    }

    private void Update()
    {
        currentSpawnTime = Mathf.Clamp(currentSpawnTime -= 1 * Time.deltaTime, 0, maxSpawnTime);

        if (currentSpawnTime == 0)
        {
            SpawnAbility();
        }
    }

    private void GenerateNewTime()
    {
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

}
