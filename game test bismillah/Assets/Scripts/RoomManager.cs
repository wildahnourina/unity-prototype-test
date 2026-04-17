using Unity.Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [Header("Camera")]
    [SerializeField] private CinemachineCamera cam;
    private CinemachineConfiner2D confiner;

    [Header("Room Prefabs")]
    [SerializeField] private Room[] roomPrefabs;

    // runtime storage room yang sudah di-instantiate
    private Dictionary<string, Room> loadedRooms = new();

    private void Awake()
    {
        instance = this;
        confiner = cam.GetComponent<CinemachineConfiner2D>();

        RegisterExistingRooms();
    }

    public void SwitchRoom(string targetRoomId, string connectionId, Transform player)
    {
        StartCoroutine(SwitchRoomCo(targetRoomId, connectionId, player));
    }

    private IEnumerator SwitchRoomCo(string targetRoomId, string connectionId, Transform player)
    {
        UI_FadeScreen fadeScreen = UI.instance.fadeScreenUI;

        fadeScreen.DoFadeOut();
        yield return fadeScreen.fadeEffectCo;

        Room targetRoom = GetOrCreateRoom(targetRoomId);
        SetActiveRoom(targetRoom);//active kan targetRoom yang lain inactive

        Transform targetPoint = FindPoint(targetRoom, connectionId);

        player.position = targetPoint.position;//teleport player

        confiner.BoundingShape2D = targetRoom.cameraBound; //update confiner
        confiner.InvalidateBoundingShapeCache();

        cam.ForceCameraPosition(player.position, Quaternion.identity);//snap camera 
        cam.PreviousStateIsValid = false; //paksa cinemachine update sekarang juga

        yield return null;

        fadeScreen.DoFadeIn();
    }

    private Transform FindPoint(Room room, string pointId)
    {
        foreach (var door in room.doors)
        {
            if (door.connectionId == pointId)
                return door.spawnPoint;
        }

        foreach (var wayArea in room.wayAreas)
        {
            foreach (var target in wayArea.targets)
            {
                if (target.connectionId == pointId)
                    return target.spawnPoint;
            }
        }

        Debug.LogError("Point not found: " + pointId);
        return room.transform;
    }

    private Room GetOrCreateRoom(string roomId)
    {
        if (loadedRooms.ContainsKey(roomId))
            return loadedRooms[roomId];

        Room prefab = roomPrefabs.First(r => r.roomId == roomId);
        Room instance = Instantiate(prefab, transform.position, Quaternion.identity, transform);

        loadedRooms.Add(roomId, instance);

        return instance;
    }

    private void RegisterExistingRooms()
    {
        Room[] rooms = FindObjectsByType<Room>(FindObjectsInactive.Include, FindObjectsSortMode.None); // include inactive juga

        foreach (var room in rooms)
        {
            if (!loadedRooms.ContainsKey(room.roomId))
                loadedRooms.Add(room.roomId, room);
            else
                Debug.LogWarning($"Duplicate Room ID: {room.roomId}");
        }
    }

    private void SetActiveRoom(Room activeRoom)
    {
        foreach (var room in loadedRooms.Values)
            room.gameObject.SetActive(room == activeRoom); //room yang lain brati inactive karena SetActive(false);
    }

    //public void SwitchRoom(Collider2D newBounds, Transform player, Vector3 spawnPos)
    //{
    //    StartCoroutine(SwitchRoomCo(newBounds, player, spawnPos));
    //}

    //private IEnumerator SwitchRoomCo(Collider2D newBounds, Transform player, Vector3 spawnPos)
    //{
    //    UI_FadeScreen fadeScreen = UI.instance.fadeScreenUI;

    //    fadeScreen.DoFadeOut();
    //    yield return fadeScreen.fadeEffectCo;

    //    player.position = spawnPos; 

    //    confiner.BoundingShape2D = newBounds; 
    //    confiner.InvalidateBoundingShapeCache();           

    //    cam.ForceCameraPosition(player.position, Quaternion.identity);        
    //    cam.PreviousStateIsValid = false; 

    //    fadeScreen.DoFadeIn();
    //}
}
