using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private AICharacter characterPrefab;
    public AICharacter CharacterPrefab { get => characterPrefab; set => characterPrefab = value; }
    
    [SerializeField] private Transform spawnPoint;
    public Transform SpawnPoint { get => spawnPoint; set => spawnPoint = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        AICharacter enemy = Instantiate(characterPrefab, null);
        enemy.gameObject.transform.position = spawnPoint.position;
    }
}
