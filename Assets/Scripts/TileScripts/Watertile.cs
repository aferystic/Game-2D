using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Watertile : Tile
{
    [SerializeField]
    private Sprite[] waterSprite;

    [SerializeField]
    private Sprite preview;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }


    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if(HasWater(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position: position, tilemap: tilemap, tileData: ref tileData);
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (HasWater(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
                    {
                        composition += 'W';
                    }
                    else
                    {
                        composition += 'E';
                    }

                }

                
            }
        }


        int randomVal = Random.Range(0, 100);

        if (randomVal < 15)
        {
            tileData.sprite = waterSprite[12];
        }
        else if (randomVal >= 15 && randomVal < 35)
        {

            tileData.sprite = waterSprite[13];
        }
        else
        {
            tileData.sprite = waterSprite[14];
        }



       if (composition[1] == 'W' && composition[2] == 'W' && composition[4] == 'W' && composition[3] == 'E' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = waterSprite[1];
        }

        else if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[5] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = waterSprite[3];
        }
        else if (composition[0] == 'W' && composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = waterSprite[4];
        }
         else if (composition[0] == 'W' && composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = waterSprite[5];
        }
        else if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'W' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = waterSprite[6];
        }
        else if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = waterSprite[7];
        }
        else if (composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = waterSprite[8];
        }
        else if (composition[0] == 'W' && composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[5] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = waterSprite[9];
        }
        else if (composition == "WWWWWEWW")
        {
            tileData.sprite = waterSprite[0];
        }
        else if (composition == "EWWWWWWW")
        {
            tileData.sprite = waterSprite[2];
        }
        else if (composition == "WWEWWWWW")
        {
            tileData.sprite = waterSprite[10];
        }
        else if (composition == "WWWWWWWE")
        {
            tileData.sprite = waterSprite[11];
        }
    }

    private bool HasWater(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/WaterTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Watertile", "New Watertile", "asset", "Save watertile", "Asstets");
        if(path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Watertile>(), path);

    }

#endif
}
