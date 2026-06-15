using UnityEngine;

public class Object_WayArea : Object_Interactable
{
    public WayTarget[] targets;

    public override void Interact(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        foreach (var t in targets)
        {
            Vector2 targetDir = GetDirectionVector(t.direction);

            if (Vector2.Dot(direction.normalized, targetDir) > .8f)
            {
                SwitchArea(t);
                return;
            }
        }
    }

    private void SwitchArea(WayTarget target)
    {
        if (target.targetRoomId == null || target.connectionId == null)
            return;

        RoomManager.instance.SwitchRoom(target.targetRoomId, target.connectionId, player.transform);
    }

    private Vector2 GetDirectionVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Vector2.up;
            case Direction.Down: return Vector2.down;
            case Direction.Left: return Vector2.left;
            case Direction.Right: return Vector2.right;
        }

        return Vector2.zero;
    }

    protected override string GetPromptText() => "Go another area";
    public override void Interact(Player player) {}

}

[System.Serializable]
public class WayTarget
{
    public Direction direction;
    public string targetRoomId;
    public string connectionId;
    public Transform spawnPoint;
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
