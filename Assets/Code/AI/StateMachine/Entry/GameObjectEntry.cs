using System;
using UnityEngine;

[Serializable]
public struct GameObjectEntry
{
    [SerializeField] private string entryName;
    public string EntryName { get => entryName; set => entryName = value; }
    
    [SerializeField] private GameObject value;
    public GameObject Value { get => value; set => this.value = value; }
    
    public GameObjectEntry(string entryName, GameObject value)
    {
        this.entryName = entryName;
        this.value = value;
    }
    
}
