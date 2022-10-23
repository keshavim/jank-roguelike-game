
using System.ComponentModel;
using UnityEngine;

public class Level_gen : MonoBehaviour
{
    public Transform[] positions;
    public GameObject[] rooms; //LR = 0, LRB = 1, LRT = 2, LRTB = 3

    private int direction;
    public int moveAmount;
    [Tooltip("sets level boundaries.")]
    public Vector2 min, max;

    private int downCounter = 0;
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

        if(!generate){
            fillEmpty();
        }
    }

    //moves to a new position and creates a room there
    void move()
    {
        Vector2 newPos = Vector2.zero;
        //right
        if(direction == 1 || direction == 2){
            
            if(transform.position.x < max.x){//prevents it from going over the border
                downCounter = 0;
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
                downCounter = 0;

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
            downCounter++; 

            if(transform.position.y > min.y){
                //for replacing previous room
                //makes sure the top room has a bottom opening
                Collider2D roomDestruction = Physics2D.OverlapCircle(transform.position, 1, roomlayer);
                var rt = roomDestruction.GetComponent<Room_type>();
                if(rt.type == 0 || rt.type == 2){
                    rt.DestroyRoom();
                    int rindex = downCounter >=2 ? 3 : Random.Range(1, rooms.Length);
                    if(rindex == 2)rindex = 3;

                    Instantiate(rooms[rindex], transform.position, Quaternion.identity);
                }


                //creating the new room
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

//searches through all the rooms spawns and fills the empty ones. 
    void fillEmpty()
    {
        transform.position = positions[0].position;
        Vector2 startpos = new Vector2(transform.position.x, transform.position.y);
        //searches through rooms from top to bottom, left to right, row by row. stops when below bottom border
        while(transform.position.y >= min.y){

            Collider2D r = Physics2D.OverlapCircle(transform.position, 1, roomlayer);
            if(r == null){
                Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, Quaternion.identity);
            }

            if(transform.position.x == max.x){
                startpos -= new Vector2(0, moveAmount);
                transform.position = startpos;
            }
            else transform.position += new Vector3(10,0);
        
        }

    }
}
