using UnityEngine;

public class Object_Environment_Door : Object_Environment
{
    [Header("Details")]
    public string targetRoomId;
    public string connectionId;//id pintu darimana berasal untuk spawnpoint di room tujuan
    public Transform spawnPoint;

    private Animator anim;
    private bool isOpen =  false;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
    }

    //protected override void OnInteract()
    //{

    //    if (!isOpen)
    //    {
    //        AudioManager.instance.PlayGlobalSFX("door_open");

    //        isOpen = true;
    //        anim?.SetBool("isOpen", true);
    //        RefreshPrompt();
    //        return;
    //    }

    //    Debug.Log("Entered room");
    //    isOpen = false;
    //    anim?.SetBool("isOpen", false);
    //    RefreshPrompt();

    //    AudioManager.instance.PlayGlobalSFX("door_close");
    //    RoomManager.instance.SwitchRoom(targetRoomId, connectionId, player.transform);
    //}

    protected override void OnInteract()
    {
        isOpen = !isOpen;
        anim?.SetBool("isOpen", isOpen);
        RefreshPrompt();

        if (isOpen)
        {
            AudioManager.instance.PlayGlobalSFX("door_open");
            return;
        }

        AudioManager.instance.PlayGlobalSFX("door_close");
        RoomManager.instance.SwitchRoom(targetRoomId, connectionId, player.transform);
    }

    protected override string GetLockedPrompt() => "(E) Enter the key";

    protected override string GetInteractionPrompt()
    {
        return isOpen ? "(E) Enter room" : "(E) Open";
    }
}
