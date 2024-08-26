using UnityEngine;
using System.Collections;
using UnityEditor;

//PLACE THIS SCRIPT IN EDITOR FOLDER ***

[CustomPropertyDrawer(typeof(Vector3Range), true)]
public class Vector3RangeDrawer : PropertyDrawer
{
    Color neutral;
    Vector3 rangeMin = Vector3.zero;
    Vector3 rangeMax = Vector3.one;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        neutral = GUI.color;

        label = EditorGUI.BeginProperty(position, label, property);

        rangeMin.x = property.FindPropertyRelative("minWindow").floatValue;
        rangeMax.x = property.FindPropertyRelative("maxWindow").floatValue;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(label);
        rangeMin = Vector3.one * EditorGUILayout.DelayedFloatField(rangeMin.x, GUILayout.MaxWidth(40));
        EditorGUILayout.LabelField(" - ", GUILayout.MaxWidth(20));
        rangeMax = Vector3.one * EditorGUILayout.DelayedFloatField(rangeMax.x, GUILayout.MaxWidth(40));
        EditorGUILayout.EndHorizontal();

        property.FindPropertyRelative("minWindow").floatValue = rangeMin.x;
        property.FindPropertyRelative("maxWindow").floatValue = rangeMax.x;

        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        Vector3 minValue = minProp.vector3Value;
        Vector3 maxValue = maxProp.vector3Value;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        //X
        GUIStyle guis = new GUIStyle();
        guis.alignment = TextAnchor.MiddleRight;
        guis.fontStyle = FontStyle.Bold;

        EditorGUILayout.LabelField("X", guis, GUILayout.MaxWidth(30));
        GUI.backgroundColor = Extensions.HexToColor("#fc9494");
        minProp.vector3Value = new Vector3(EditorGUILayout.FloatField(minProp.vector3Value.x, GUILayout.MaxWidth(60)), minProp.vector3Value.y, minProp.vector3Value.z);
        GUI.color = neutral;

        EditorGUI.BeginChangeCheck();
        GUI.color = Extensions.HexToColor("#ff3d3d");
        EditorGUILayout.MinMaxSlider(ref minValue.x, ref maxValue.x, rangeMin.x, rangeMax.x);
        GUI.color = neutral;
        if (EditorGUI.EndChangeCheck())
        {
            minProp.vector3Value = new Vector3(minValue.x, minProp.vector3Value.y, minProp.vector3Value.z);
            maxProp.vector3Value = new Vector3(maxValue.x, maxProp.vector3Value.y, maxProp.vector3Value.z);
        }

        GUI.backgroundColor = Extensions.HexToColor("#fc9494");
        maxProp.vector3Value = new Vector3(EditorGUILayout.FloatField(maxProp.vector3Value.x, GUILayout.MaxWidth(60)), maxProp.vector3Value.y, maxProp.vector3Value.z);
        GUI.color = neutral;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        //Y
        EditorGUILayout.LabelField("Y", guis, GUILayout.MaxWidth(30));
        GUI.backgroundColor = Extensions.HexToColor("#dcff8e");
        minProp.vector3Value = new Vector3(minProp.vector3Value.x, EditorGUILayout.FloatField(minProp.vector3Value.y, GUILayout.MaxWidth(60)), minProp.vector3Value.z);
        GUI.color = neutral;

        EditorGUI.BeginChangeCheck();
        GUI.color = Extensions.HexToColor("#b0ff00");
        EditorGUILayout.MinMaxSlider(ref minValue.y, ref maxValue.y, rangeMin.y, rangeMax.y);
        GUI.color = neutral;
        if (EditorGUI.EndChangeCheck())
        {
            minProp.vector3Value = new Vector3(minProp.vector3Value.x, minValue.y, minProp.vector3Value.z);
            maxProp.vector3Value = new Vector3(maxProp.vector3Value.x, maxValue.y, maxProp.vector3Value.z);
        }

        GUI.backgroundColor = Extensions.HexToColor("#dcff8e");
        maxProp.vector3Value = new Vector3(maxProp.vector3Value.x, EditorGUILayout.FloatField(maxProp.vector3Value.y, GUILayout.MaxWidth(60)), maxProp.vector3Value.z);
        GUI.color = neutral;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        //Z
        EditorGUILayout.LabelField("Z", guis, GUILayout.MaxWidth(30));
        GUI.backgroundColor = Extensions.HexToColor("#7a97ff");
        minProp.vector3Value = new Vector3(minProp.vector3Value.x, minProp.vector3Value.y, EditorGUILayout.FloatField(minProp.vector3Value.z, GUILayout.MaxWidth(60)));
        GUI.color = neutral;

        EditorGUI.BeginChangeCheck();
        GUI.color = Extensions.HexToColor("#3d67ff");
        EditorGUILayout.MinMaxSlider(ref minValue.z, ref maxValue.z, rangeMin.z, rangeMax.z);
        GUI.color = neutral;
        if (EditorGUI.EndChangeCheck())
        {
            minProp.vector3Value = new Vector3(minProp.vector3Value.x, minProp.vector3Value.y, minValue.z);
            maxProp.vector3Value = new Vector3(maxProp.vector3Value.x, maxProp.vector3Value.y, maxValue.z);
        }

        GUI.backgroundColor = Extensions.HexToColor("#7a97ff");
        maxProp.vector3Value = new Vector3(maxProp.vector3Value.x, maxProp.vector3Value.y, EditorGUILayout.FloatField(maxProp.vector3Value.z, GUILayout.MaxWidth(60)));
        GUI.color = neutral;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        EditorGUI.EndProperty();
    }
}

//MISC, DON'T CARE ABOUT
public class Vector3RangeMisc : MonoBehaviour
{
    Vector3Range v3r;
}

[CustomEditor(typeof(Vector3Range))]
public class Vector3RangeEditorMisc : Editor
{
}
