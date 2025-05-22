using UnityEngine;

public class FilteredPrefabAttribute : PropertyAttribute
{
    public string folderPath;

    public FilteredPrefabAttribute(string folderPath)
    {
        this.folderPath = folderPath;
    }
}
