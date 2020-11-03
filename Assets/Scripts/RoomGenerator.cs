using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for reset the scene
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    //the use of the enum
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("RoomMessage")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor;
    public Color endColor;
    private GameObject endRoom;

    [Header("PositionControl")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public int maxStep;

    public GameObject teleport;

    //the use of the List<T>
    public List<Room> rooms = new List<Room>();

    //storage the room which stepToStart is equal to maxStep
    List<GameObject> farRooms = new List<GameObject>();

    //storage the room which stepToStart is equal to maxStep - 1 
    List<GameObject> lessFarRooms = new List<GameObject>();

    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            //don't want to type so many time in GetComponent<>, so let rooms be the List of Room
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //change the position of the generatorPoint
            ChangePointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        //This sentence not good enough
        //rooms[roomNumber - 1].GetComponent<SpriteRenderer>().color = endColor;


        endRoom = rooms[0].gameObject;

        //generate the door
        foreach (var room in rooms)
        {
            //this sqrMagnitude is compared to the origin, the widely use is (position1 - position2).sqrMagnitude
            /*
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room.gameObject;
            }
            */

            SetupRoom(room, room.transform.position);
        }

        //find the endRoom
        FindEndRoom();

        EndRoomFunc();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.anyKeyDown)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }

    public void ChangePointPos()
    {
        do
        {
            //the 4 is not included, just 0,1,2,3
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
            //while or do-while? OverlapCircle or OverlapBox?
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }

    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xOffset, yOffset);

        //select different room
        switch(newRoom.doorNumber)
        {
            case 1:
                if(newRoom.roomUp)
                    Instantiate(wallType.singleUp,roomPosition,Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleBottom, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(wallType.doubleLB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.doubleUB, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.doubleRB, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.tripleLUB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleURB, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleLRB, roomPosition, Quaternion.identity);
                break;
            case 4:
                Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;
        }
    }


    //can also find the room have the biggest number of the stepToStart, 
    //and find the no door direction(roomUp... == false), then instantiate a new room in this direction as the EndRoom
    public void FindEndRoom()
    {
        //get the maxStep
        for (int i = 0; i < rooms.Count; i++)
        {
            if(rooms[i].stepToStart > maxStep)
            {
                maxStep = rooms[i].stepToStart;
            }
        }

        //generate the farRooms and lessFarRooms
        foreach (var room in rooms)
        {
            if(room.stepToStart == maxStep)
            {
                farRooms.Add(room.gameObject);
            }

            if(room.stepToStart == maxStep - 1)
            {
                lessFarRooms.Add(room.gameObject);
            }
        }

        //generate the oneWayRooms
        //from the farRooms
        for (int i = 0; i < farRooms.Count; i++)
        {
            //farRooms[i] is a gameObject, if want to access the parameter in the room, should GetComponent
            if(farRooms[i].GetComponent<Room>().doorNumber == 1)
            {
                oneWayRooms.Add(farRooms[i]);
            }
        }

        //from the lessFarRooms
        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
            {
                oneWayRooms.Add(lessFarRooms[i]);
            }
        }

        if(oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }

    public void EndRoomFunc()
    {
        endRoom.GetComponent<SpriteRenderer>().color = endColor;

        Instantiate(teleport,
            new Vector3(endRoom.transform.position.x - 7f, endRoom.transform.position.y - 3f, 0), Quaternion.identity);
    }
}

[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleBottom,
                      doubleLU, doubleLR, doubleLB, doubleUR, doubleUB, doubleRB,
                      tripleLUR, tripleLUB, tripleURB, tripleLRB,
                      fourDoors;
}
