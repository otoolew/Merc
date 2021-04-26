using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStackController : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private StateNode defaultState;
    public StateNode DefaultState { get => defaultState; set => defaultState = value; }
    
    private Stack<StateNode> stateStack;

    [SerializeField] private List<StateNode> stateList;
    public List<StateNode> StateList => stateList;

    private void OnEnable()
    {
        Debug.Log("StateNode Stack Enabled");
        stateStack = new Stack<StateNode>();
        stateList = new List<StateNode>();
    }

    private void Start()
    {
        Push(Instantiate(defaultState));
    }

    #region Stack Operations
    
    public int Count => stateStack?.Count ?? 0;

    public StateNode Peek()
    {
        return stateStack.Peek();
    }
    
    public StateNode Pop()
    {
        return stateStack.Pop();
    }
    
    public void Push(StateNode item)
    {
        stateStack.Push(item);
    }
    
    public StateNode[] ToArray()
    {
        return stateStack.ToArray();
    }
    
    public void Clear()
    {
        stateStack.Clear();
    }
    
    public bool Contains(StateNode item)
    {
        return stateStack.Contains(item);
    }
    
    public Stack<StateNode>.Enumerator GetEnumerator()
    {
        return stateStack.GetEnumerator();
    }
    
    public void TrimExcess()
    {
        stateStack.TrimExcess();
    }
    
    //public void CopyTo(T[] array, int arrayIndex);
    #endregion
    
    #region Inspector
    public void OnBeforeSerialize()
    {
        StateList.Clear();

        foreach (var kvp in stateStack)
        {
            StateList.Add(kvp);
        }
    }
    public void OnAfterDeserialize()
    {
        stateStack = new Stack<StateNode>();

        for (int i = StateList.Count; i >= 0; i--)
            stateStack.Push(StateList[i]);
    }
    #endregion
}
