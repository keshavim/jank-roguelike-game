using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_type : MonoBehaviour
{
    public int type;
    public bool start;

    public void DestroyRoom(){
    Destroy(gameObject);
    } 
}
