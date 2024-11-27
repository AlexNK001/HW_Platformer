using UnityEngine;

public class HealPotion : Item
{
    [SerializeField] private float _healPower;

    public float HealPower => _healPower;
}
