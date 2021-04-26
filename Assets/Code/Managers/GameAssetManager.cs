﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetManager : Singleton<GameAssetManager>
{
    [SerializeField] private PoolComponent<PopUpText> popUpPool;
    public PoolComponent<PopUpText> PopUpPool { get => popUpPool; set => popUpPool = value; }

    [SerializeField] private ProjectileResourcePool projectileResourcePool;
    public ProjectileResourcePool ProjectileResourcePool { get => projectileResourcePool; set => projectileResourcePool = value; }

    [SerializeField] private AICharacter enemyPrefab;
    public AICharacter EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }

    public void Start()
    {
        
    }

}
