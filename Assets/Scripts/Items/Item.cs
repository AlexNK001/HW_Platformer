using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Item : MonoBehaviour
{
    public virtual void Pickup()
    {
        gameObject.SetActive(false);
    }
}
