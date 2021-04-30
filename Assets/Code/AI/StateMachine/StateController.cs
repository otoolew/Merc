using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private UnityDictionary<string, StateVariable> variableDictionary;
    public UnityDictionary<string, StateVariable> VariableDictionary { get => variableDictionary; set => variableDictionary = value; }
    
    [SerializeField] private UnityDictionary<string, StateNode> stateDictionary;
    public UnityDictionary<string, StateNode> StateDictionary { get => stateDictionary; set => stateDictionary = value; }
    
    [SerializeField] private List<GameObjectEntry> entryDictionary;
    public List<GameObjectEntry> EntryDictionary { get => entryDictionary; set => entryDictionary = value; }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
