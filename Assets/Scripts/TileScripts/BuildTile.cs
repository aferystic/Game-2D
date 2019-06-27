using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class BuildTile : Tile
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
    [MenuItem("Assets/Create/Tiles/BuildTile")]
    public static void CreateBuildTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save BuildTile", "New BuildTile", "asset", "Save BuildTile", "Asstets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildTile>(), path);

    }

#endif
}
