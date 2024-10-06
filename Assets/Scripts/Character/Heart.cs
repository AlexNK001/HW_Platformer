using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Heart : MonoBehaviour
{
    [SerializeField] protected float _health;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
