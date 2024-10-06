public class PlayerHeart : Heart
{
    public void DrinkHealingPotion(HealPotion potion)
    {
        _health += potion.HealPower;
    }
}