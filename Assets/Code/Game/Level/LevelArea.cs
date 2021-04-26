using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    [SerializeField] private AICharacter characterPrefab;
    public AICharacter CharacterPrefab { get => characterPrefab; set => characterPrefab = value; }
    
    [SerializeField] private Transform spawnPoint;
    public Transform SpawnPoint { get => spawnPoint; set => spawnPoint = value; }
    
    [SerializeField] private PatrolCircuit patrolCircuit;
    public PatrolCircuit PatrolCircuit { get => patrolCircuit; set => patrolCircuit = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCharacter()
    {
        AICharacter enemy = Instantiate(characterPrefab, null);
        enemy.gameObject.transform.position = spawnPoint.position;
        enemy.MovementComp.PatrolCircuit = patrolCircuit;
        enemy.MovementComp.ContinueToPatrolPoint();
        Debug.Log("Spawned Character");
    }
}
