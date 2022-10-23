
using UnityEngine;

public class Level_gen : MonoBehaviour
{
    public Transform[] positions;
    public GameObject[] rooms; //LR = 0, LRB = 1, LRT = 2, LRTB = 3

    private int direction;
    public int moveAmount;
    [Tooltip("sets level boundaries.")]
    public Vector2 min, max;
    [Tooltip("set a position axis to 0 if that axis is irrelevant")]
    public Vector2 end;
    private bool generate = true;

    public LayerMask roomlayer;

    float timeBetweenRoom;
    public float startTBR = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        generate = true;
        //creates the first room
        transform.position = positions[Random.Range(0, positions.Length)].position;
        Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, Quaternion.identity);
        
        
        direction = Random.Range(1, 6);

    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenRoom <= 0 && generate == true){
            move();
            timeBetweenRoom = startTBR;
        }
        timeBetweenRoom -= Time.deltaTime;
    }

    //moves to a new position and creates a room there
    void move()
    {
        Vector2 newPos = Vector2.zero;
        //right
        if(direction == 1 || direction == 2){
            
            if(transform.position.x < max.x){//prevents it from going over the border
                newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); 
                if(direction == 3) direction = 1;//prevents it from going backwards
                else if(direction == 4) direction = 5;
            } else{//prevents it from getting stuck
                direction = 5;
            }
        }
        //left
        else if(direction == 3 || direction == 4){
            if(transform.position.x > min.x){
                newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); 
                if(direction == 1) direction = 3;
                else if (direction == 2) direction = 5;  
            } else{
                direction = 5;
            }
        }
            
        //down
        else if(direction == 5){
            if(transform.position.y > min.y){
                //makes sure the top room has a bottom opening
                Collider2D roomDestruction = Physics2D.OverlapCircle(transform.position, 1, roomlayer);
                Debug.Log(roomDestruction);
                var rt = roomDestruction.GetComponent<Room_type>();
                if(rt.type == 0 || rt.type == 2){
                    rt.DestroyRoom();

                    Instantiate(rooms[3], transform.position, Quaternion.identity);//made it the 4 sided room becuse I'm lazy
                }



                newPos = new Vector2(transform.position.x, transform.position.y - moveAmount); 
                transform.position = newPos;

                Instantiate(rooms[Random.Range(2, rooms.Length)], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); 
            }
        }
        

        if(transform.position.y == min.y){
            generate = false;
        }
    }

    
}
