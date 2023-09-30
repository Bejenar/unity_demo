using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public GameObject[] seedPrefabs;
    public Transform[] slots;
    private GameObject[] seedsInSlots;

    private void Start()
    {
        seedsInSlots = new GameObject[slots.Length];
        SpawnInitialSeeds();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            MoveSeeds();
            SpawnRandomSeedInFirstSlot();
        }
    }

    private void SpawnInitialSeeds()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            SpawnRandomSeedInSlot(i);
        }
    }

    private void SpawnRandomSeedInFirstSlot()
    {
        int randomIndex = Random.Range(0, seedPrefabs.Length);
        SpawnSeedInSlot(randomIndex, 0);
    }

    private void SpawnRandomSeedInSlot(int slotIndex)
    {
        int randomIndex = Random.Range(0, seedPrefabs.Length);
        SpawnSeedInSlot(randomIndex, slotIndex);
    }

    private void SpawnSeedInSlot(int prefabIndex, int slotIndex)
    {
        if (seedsInSlots[slotIndex] == null)
        {
            GameObject newSeed = Instantiate(seedPrefabs[prefabIndex], slots[slotIndex].position, Quaternion.identity);
            newSeed.transform.SetParent(slots[slotIndex]);
            seedsInSlots[slotIndex] = newSeed;
        }
    }

    private void MoveSeeds()
    {
        for (int i = seedsInSlots.Length - 1; i > 0; i--)
        {
            if (seedsInSlots[i - 1] != null)
            {
                Destroy(seedsInSlots[i]);
                seedsInSlots[i] = seedsInSlots[i - 1];
                seedsInSlots[i - 1] = null;
                seedsInSlots[i].transform.SetParent(slots[i]);
                seedsInSlots[i].transform.position = slots[i].position;
            }
        }
    }
}
