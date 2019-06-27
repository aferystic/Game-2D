using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class WallTile : Tile
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
    [MenuItem("Assets/Create/Tiles/WallTile")]
    public static void CreateWallTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save WallTile", "New WallTile", "asset", "Save WallTile", "Asstets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);

    }

#endif
}
