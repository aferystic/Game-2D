using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class SandTileWithGrass : Tile
{
    [SerializeField]
    private Sprite[] sandWitkGrassSprite;

    [SerializeField]
    private Sprite preview;


    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if (HasSand(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }


    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (HasSand(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
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

        if (randomVal < 10)
        {
            tileData.sprite = sandWitkGrassSprite[15];
        }
        else if (randomVal >= 10 && randomVal < 20)
        {

            tileData.sprite = sandWitkGrassSprite[13];
        }
        else if (randomVal >= 20 && randomVal < 30)
        {
            tileData.sprite = sandWitkGrassSprite[14];
        }
        else 
        {
            tileData.sprite = sandWitkGrassSprite[6];
        }


        if (composition[0] == '1' && composition[1] == '1' && composition[3] == '1' && composition[4] == '0' && composition[5] == '1' && composition[6] == '1')
        {
            tileData.sprite = sandWitkGrassSprite[1];
        }
        else if (composition[1] == '0' && composition[3] == '1' && composition[4] == '1' && composition[5] == '1' && composition[6] == '1' && composition[7] == '1')
        {
            tileData.sprite = sandWitkGrassSprite[5];
        }
        else if (composition[0] == '1' && composition[1] == '1' && composition[2] == '1' && composition[3] == '1' && composition[4] == '1' && composition[6] == '0')
        {
            tileData.sprite = sandWitkGrassSprite[7];
        }
        else if (composition[1] == '0' && composition[3] == '0' && composition[4] == '1' && composition[6] == '1' && composition[7] == '1')
        {
            tileData.sprite = sandWitkGrassSprite[10];
        }
        else if (composition[1] == '1' && composition[2] == '1' && composition[4] == '1' && composition[3] == '0' && composition[6] == '1' && composition[7] == '1')
        {
            tileData.sprite = sandWitkGrassSprite[11];
        }
        else if (composition[0] == '1' && composition[1] == '1' && composition[3] == '1' && composition[4] == '0' && composition[6] == '0')

        {
            tileData.sprite = sandWitkGrassSprite[2];
        }
        else if (composition[1] == '0' && composition[3] =='1' && composition[4] == '0' && composition[5] == '1' && composition[6] == '1')
        {
            tileData.sprite = sandWitkGrassSprite[0];
        }
        else if (composition[1] == '1' && composition[2] == '1' && composition[3] == '0' && composition[4] == '1' && composition[6] == '0')
        {
            tileData.sprite = sandWitkGrassSprite[12];
        }
        else if (composition == "11111110")
        {
            tileData.sprite = sandWitkGrassSprite[8];
        }
        else if (composition == "11011111")
        {
            tileData.sprite = sandWitkGrassSprite[9];
        }
        else if (composition == "01111111")
        {
            tileData.sprite = sandWitkGrassSprite[4];
        }
       else if (composition == "11111011")
        {
            tileData.sprite = sandWitkGrassSprite[3];
        }
    }

    private bool HasSand(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }





#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/SandTileWithGrass")]
    public static void CreateSandWithGrassTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Sandtile", "New SandTile", "asset", "Save SandTile", "Asstets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SandTileWithGrass>(), path);

    }

#endif
}
