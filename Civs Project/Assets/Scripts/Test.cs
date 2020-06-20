using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    // Tilemap
    public Tilemap tilemap;

    // Tiles
    public Tile grass;
    public Tile forest;
    public Tile sand;
    public Tile water;
    public Tile mountain;
    public Tile snow;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Tilemap size before adding the 4 tiles is:" + tilemap.size);

        tilemap.SetTile(new Vector3Int(0, 0, 0), grass);
        tilemap.SetTile(new Vector3Int(1, 0, 0), water);
        tilemap.SetTile(new Vector3Int(0, 1, 0), mountain);
        tilemap.SetTile(new Vector3Int(1, 1, 0), sand);

        tilemap.SetTile(new Vector3Int(20, 20, 0), sand);

        Debug.Log("Tilemap size after is:" + tilemap.size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
