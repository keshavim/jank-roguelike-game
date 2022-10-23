using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_gen : MonoBehaviour
{
    public GameObject[] rooms;

    void Start()
    {
        Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, Quaternion.identity);
    }


}
