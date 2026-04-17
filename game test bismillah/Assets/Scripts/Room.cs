using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomId;
    public Collider2D cameraBound;

    public Object_Environment_Door[] doors;
    public Object_WayArea[] wayAreas;
}
