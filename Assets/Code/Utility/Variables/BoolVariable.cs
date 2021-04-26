using UnityEngine;

[CreateAssetMenu(menuName = "Game/AI/Variable/Bool", fileName = "newBoolVar")]
public class BoolVariable : ScriptableObject, IVariable<bool>
{
    [SerializeField] private string entryName;
    public string VariableName { get => entryName; set => entryName = value; }

    [SerializeField] private bool value;
    public bool Value { get => value; set => this.value = value; }
    public BoolVariable(string entryName, bool value)
    {
        this.entryName = entryName;
        this.value = value;
    }
    public void SetValue(bool value)
    {
        this.value = value;
    }
}
