using UnityEditor;
using UnityEngine;

using MatchTile.Tile;

[CustomEditor(typeof(BaseTile))]
public class TileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BaseTile tile = (BaseTile)target;

        if (tile.parents != null)
        {
            EditorGUILayout.LabelField("Parents");
            for (int i = 0; i < tile.parents.Count; i++)
            {
                tile.parents[i] = (IBaseTile)EditorGUILayout.ObjectField(tile.parents[i] as UnityEngine.Object, typeof(IBaseTile), allowSceneObjects: true);
            }
        }

        if (tile.children != null)
        {
            EditorGUILayout.LabelField("Children");
            for (int i = 0; i < tile.children.Count; i++)
            {
                tile.children[i] = (IBaseTile)EditorGUILayout.ObjectField(tile.children[i] as UnityEngine.Object, typeof(IBaseTile), allowSceneObjects: true);
            }
        }
    }

}
