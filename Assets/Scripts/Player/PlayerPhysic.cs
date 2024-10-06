using UnityEngine;

public class PlayerPhysic : MonoBehaviour
{
    [SerializeField] private Bug _bug;
    [SerializeField] private PlayerHeart _heart;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private float _raycastDistanse = 0.2f;

    private RaycastHit2D[] _hits;

    public bool IsGround { get; private set; } = true;

    private void Start()
    {
        _hits = new RaycastHit2D[10];
    }

    private void Update()
    {
        int hitCount = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            _filter,
            _hits,
            _raycastDistanse);

        IsGround = hitCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            switch (item)
            {
                case Coin coin:
                    _bug.AddCoin(coin);
                    break;

                case HealPotion potion:
                    _heart.DrinkHealingPotion(potion);
                    break;
            }

            item.Pickup();
        }
    }
}
