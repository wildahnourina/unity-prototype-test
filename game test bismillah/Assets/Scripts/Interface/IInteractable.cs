using UnityEngine;
public interface IInteractable
{
    public void Interact(Player player);
    public void Interact(Vector2 direction);
}
