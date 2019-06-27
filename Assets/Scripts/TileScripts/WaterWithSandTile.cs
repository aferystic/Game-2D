using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;


public class WaterWithSandTile : Tile
{
    [SerializeField]
    private Sprite[] waterWithSandSprite;

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

                if (HasWater(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }


    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (HasWater(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
                    {
                        composition += '1';
                    }
                    else
                    {
                        composition += '0';
                    }

                }


            }
        }


        int randomVal = Random.Range(0, 100);

        if (randomVal < 15)
        {
            tileData.sprite = waterWithSandSprite[12];
        }
        else if (randomVal >= 15 && randomVal < 30)
        {

            tileData.sprite = waterWithSandSprite[13];
        }
        else
        {
            tileData.sprite = waterWithSandSprite[14];
        }


        if (composition[1] == '1' && composition[2] == '1' && composition[4] == '1' && composition[3] == '0' && composition[6] == '1' && composition[7] == '1')
        {
            tileData.sprite = waterWithSandSprite[1];
        }
        else if (composition[0] == '1' && composition[1] == '1' && composition[2] == '1' && composition[3] == '1' && composition[4] == '1' && composition[6] == '0')
        {
            tileData.sprite = waterWithSandSprite[5];
        }
        else if (composition[1] == '0' && composition[3] == '0' && composition[4] == '1' && composition[6] == '1' && composition[7] == '1')
        {
            tileData.sprite = waterWithSandSprite[7];
        }
        else if (composition[0] == '1' && composition[1] == '1' && composition[3] == '1' && composition[4] == '0' && composition[5] == '1' && composition[6] == '1')
        {
            tileData.sprite = waterWithSandSprite[10];
        }
        else if (composition[1] == '1' && composition[2] == '1' && composition[3] == '0' && composition[4] == '1' && composition[6] == '0')
        {
            tileData.sprite = waterWithSandSprite[8];
        }
        else if (composition[0] == '1' && composition[1] == '1' && composition[3] == '1' && composition[4] == '0' && composition[6] == '0')
        {
            tileData.sprite = waterWithSandSprite[4];
        }
        else if (composition[1] == '0' && composition[3] == '1' && composition[4] == '0' && composition[5] == '1' && composition[6] == '1')
        {
            tileData.sprite = waterWithSandSprite[3];
        }
        else if (composition[1] == '0' && composition[3] == '1' && composition[4] == '1' && composition[5] == '1' && composition[6] == '1' && composition[7] == '1')
        {
            tileData.sprite = waterWithSandSprite[6];
        }
        else if (composition == "11011111")
        {
            tileData.sprite = waterWithSandSprite[11];
        }
        else if (composition == "01111111")
        {
            tileData.sprite = waterWithSandSprite[2];
        }
        else if (composition == "11111011")
        {
            tileData.sprite = waterWithSandSprite[0];
        }
        else if (composition == "11111110")
        {
            tileData.sprite = waterWithSandSprite[9];
        }
    }

    private bool HasWater(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }







#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/WaterWithSandTile")]
    public static void CreateWaterWithSandTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save WaterWithSandTile", "New WaterWithSandTile", "asset", "Save WaterWithSandTile", "Asstets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WaterWithSandTile>(), path);

    }

#endif
}
