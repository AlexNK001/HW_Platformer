using UnityEngine;
using Player;

[RequireComponent(typeof(Collider2D))]
public class AreaAction : MonoBehaviour
{
    public bool HaveCharacter { get; private set; }
    public Transform Target { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerHeart heart))
        {
            HaveCharacter = true;
            Target = heart.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerHeart _))
        {
            HaveCharacter = false;
        }
    }
}
