using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_gen : MonoBehaviour
{
    public GameObject[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        var instance = Instantiate(tiles[Random.Range(0, tiles.Length)], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }


}
