using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StackCollection<T> : ISerializationCallbackReceiver
{
    public StackCollection()
    {
        stateStack = new Stack<T>();
        StoredValues = new List<T>();
    }
    
    [SerializeField] private Stack<T> stateStack;
    public Stack<T> StateStack { get => stateStack; set => stateStack = value; }
    
    [SerializeField] private List<T> storedValues;
    public List<T> StoredValues { get => storedValues; set => storedValues = value; }

    #region Stack Operations
    public int Count { get { if(StateStack != null){ return StateStack.Count; } return 0; } }
    public T Peek()
    {
        return StateStack.Peek();
    }
    public T Pop()
    {
        return StateStack.Pop();
    }
    public void Push(T item)
    {
        StateStack.Push(item);
    }
    public T[] ToArray()
    {
        return StateStack.ToArray();
    }
    public void Clear()
    {
        StateStack.Clear();
    }
    public bool Contains(T item)
    {
        return StateStack.Contains(item);
    }
    public Stack<T>.Enumerator GetEnumerator()
    {
        return StateStack.GetEnumerator();
    }
    public void TrimExcess()
    {
        StateStack.TrimExcess();
    }
    //public void CopyTo(T[] array, int arrayIndex);
    #endregion


    #region Inspector
    public void OnBeforeSerialize()
    {
        StoredValues.Clear();

        foreach (var kvp in StateStack)
        {
            StoredValues.Add(kvp);
        }
    }
    public void OnAfterDeserialize()
    {
        for (int i = StoredValues.Count; i >= 0; i--)
            StateStack.Push(StoredValues[i]);
    }
    #endregion
}
