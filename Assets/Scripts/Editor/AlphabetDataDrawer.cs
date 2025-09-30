using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(AlphabetData))]
[CanEditMultipleObjects]
[System.Serializable]
public class AlphabetDataDrawer : Editor
{
    private ReorderableList AlphabelNormalList;
    private ReorderableList AlphabelFalselList;
    private ReorderableList AlphabelTrueList;

    private void OnEnable()
    {
        IntializeReodableList(ref AlphabelNormalList, "AlphabetNormal", "Alphabet Normal");
        IntializeReodableList(ref AlphabelFalselList, "AlphabetFalse", "Alphabet False");
        IntializeReodableList(ref AlphabelTrueList, "AlphabetTrue", "Alphabet True");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AlphabelNormalList.DoLayoutList();
        AlphabelFalselList.DoLayoutList();
        AlphabelTrueList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    private void IntializeReodableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
        true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };

        var l = list;

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("letter"), GUIContent.none);

            EditorGUI.PropertyField(
                new Rect(rect.x + 70, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("image"), GUIContent.none);
        };
    }
}
