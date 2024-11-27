using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private Collider2D _damageCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Heart attacked))
        {
            attacked.TakeDamage(_damage);
            _damageCollider.enabled = false;
        }
    }

    public void SetActiveCollider(bool isAtack)
    {
        _damageCollider.enabled = isAtack;
    }
}
