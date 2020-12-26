using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0,100)]
    public int randomFillPercent;

    int[,] map;

    public Sprite sprite1;
    public Sprite sprite2;

    public GameObject obParent;

    void Start() {
        GenerateMap();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        if(obParent.transform.childCount > 0){
            Destroy(obParent);
            GameObject nob = new GameObject("Objetos");
            obParent = nob;
        }


        map = new int[width,height];
        RandomFillMap();

        for (int i = 0; i < 5; i ++) {
            SmoothMap();
        }
        OnDraw();
    }


    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < height; y ++) {
                if (x == 0 || x == width-1 || y == 0 || y == height -1) {
                    map[x,y] = 1;
                }
                else {
                    map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
                }
            }
        }
    }

    void SmoothMap() {
        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < height; y ++) {
                int neighbourWallTiles = GetSurroundingWallCount(x,y);

                if (neighbourWallTiles > 4)
                    map[x,y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x,y] = 0;

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += map[neighbourX,neighbourY];
                    }
                }
                else {
                    wallCount ++;
                }
            }
        }

        return wallCount;
    }


    void OnDraw() {
        if (map != null) {
            for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
                    GameObject go = new GameObject(x+"x"+y);
                    go.transform.localPosition = new Vector3(x - width/2,y-height/2,0);
                    go.transform.parent = obParent.transform;
                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sprite = map[x,y] == 1 ? sprite1 : sprite2;
                   // Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
                    //Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
                   // Gizmos.DrawCube(pos,Vector3.one);
                }
            }
        }
    }

}