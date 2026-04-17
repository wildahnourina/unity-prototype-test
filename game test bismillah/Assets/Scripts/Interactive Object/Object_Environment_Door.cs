using UnityEngine;

public class Object_Environment_Door : Object_Environment
{
    [Header("Details")]
    public string targetRoomId;
    public string connectionId;//id pintu darimana berasal untuk spawnpoint di room tujuan
    public Transform spawnPoint;

    private bool isOpen =  false;

    protected override void OnInteract()
    {
        if (!isOpen)
        {
            AudioManager.instance.PlayGlobalSFX("door_open");

            isOpen = true;
            RefreshPrompt();
            return;
        }

        Debug.Log("Entered room");
        isOpen = false;
        RefreshPrompt();
        AudioManager.instance.PlayGlobalSFX("door_close");

        RoomManager.instance.SwitchRoom(targetRoomId, connectionId, player.transform);
    }

    protected override string GetLockedPrompt() => "(E) Enter the key";

    protected override string GetInteractionPrompt()
    {
        return isOpen ? "(E) Enter room" : "(E) Open";
    }
}
