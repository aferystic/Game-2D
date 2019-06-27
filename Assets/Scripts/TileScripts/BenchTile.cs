using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class BenchTile : Tile
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
    [MenuItem("Assets/Create/Tiles/BenchTile")]
    public static void CreateTreeTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save BenchTile", "New BenchTile", "asset", "Save BenchTile", "Asstets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BenchTile>(), path);

    }

#endif
}
