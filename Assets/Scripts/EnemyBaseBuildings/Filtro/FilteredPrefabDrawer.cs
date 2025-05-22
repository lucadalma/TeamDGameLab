#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(FilteredPrefabAttribute))]
public class FilteredPrefabDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attribute = (FilteredPrefabAttribute)base.attribute;

        // Draw popup with prefabs from folder
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { attribute.folderPath });
        GameObject[] options = guids.Select(g => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(g)))
                                    .ToArray();

        string[] optionNames = options.Select(o => o.name).ToArray();
        int currentIndex = System.Array.IndexOf(options, property.objectReferenceValue);

        int selected = EditorGUI.Popup(position, label.text, currentIndex, optionNames);

        if (selected >= 0 && selected < options.Length)
        {
            property.objectReferenceValue = options[selected];
        }
    }
}
#endif
