using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SpeelCollider : MonoBehaviour
{
    private List<Heart> _hearts = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Heart heart) && heart.IsAlive)
        {
            _hearts.Add(heart);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Heart heart))
        {
            _hearts.Remove(heart);
        }
    }

    private void OnEnable()
    {
        _hearts.Clear();
    }

    public bool TryFindNearestTarget(out Heart heart)
    {
        float currentMinDistance = float.MaxValue;
        heart = null;

        if (_hearts.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < _hearts.Count; i++)
        {
            Heart currentHeart = _hearts[i];
            float currentDistance = Vector3.Distance(transform.position, currentHeart.transform.position);

            if (currentHeart.IsAlive && currentMinDistance > currentDistance)
            {
                heart = currentHeart;
                currentMinDistance = currentDistance;
            }
        }

        return true;
    }
}