using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_gen : MonoBehaviour
{
    public GameObject[] rooms;

    Level_gen lg;

    void Start()
    {
        lg = GameObject.FindWithTag("levelGenerator").GetComponent<Level_gen>();

        //make sure each template has a start and end room
        if(lg.start){//need to fix bug where the start room can get overriden by the destroying
            var inst = Instantiate(rooms[0], transform.position, Quaternion.identity);
            inst.GetComponent<Room_type>().start = true;
            lg.start = false;
        }
        else if(lg.end){
            Instantiate(rooms[1], transform.position, Quaternion.identity);
            lg.end = false;
        }    
        else
            Instantiate(rooms[Random.Range(2, rooms.Length)], transform.position, Quaternion.identity);
    }


}
