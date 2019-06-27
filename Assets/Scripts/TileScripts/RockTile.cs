using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class RockTile : Tile
{
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (go != null)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = -position.y * 2;
            return base.StartUp(position, tilemap, go);
        }

        return true;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/RockTile")]
    public static void CreateRockTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save RockTile", "New RockTile", "asset", "Save RockTile", "Asstets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RockTile>(), path);

    }

#endif
}
