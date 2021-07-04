using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(CustomSample), true)]
public class CustomSampleInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement container = new VisualElement();
        SerializedProperty it = serializedObject.GetIterator();
        it.Next(true);
        while (it.NextVisible(false))
        {
            PropertyField prop = new PropertyField(it);
            prop.SetEnabled(it.name != "m_Script");
            container.Add(prop);
        }

        return container;
    }
}
