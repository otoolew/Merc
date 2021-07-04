using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
[CustomPropertyDrawer(typeof(DisableFloatAttribute))]
public class DisableFloatDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property) // Needed
    {
        //return base.CreatePropertyGUI(property);
        FloatField floatField = new FloatField(property.displayName)
        {
            value = property.floatValue,
        };
        floatField.SetEnabled(true);
        return floatField;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) // Needed
    {
        //base.OnGUI(position, property, label);
        GUI.enabled = false;
        EditorGUI.FloatField(position, label, property.floatValue);
        GUI.enabled = true;
    }
}
