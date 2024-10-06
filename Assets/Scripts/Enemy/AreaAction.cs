using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaAction : MonoBehaviour
{
    public bool HaveCharacter { get; private set; }
    public Transform Target { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerPhysic physicCharacter))
        {
            HaveCharacter = true;
            Target = physicCharacter.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerPhysic>(out _))
        {
            HaveCharacter = false;
        }
    }
}
