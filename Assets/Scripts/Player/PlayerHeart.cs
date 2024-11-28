using UnityEngine;

namespace Player
{
    public class PlayerHeart : Heart
    {
        private PlayerPhysic _playerPhysic;

        public void Heal(Item item)
        {
            if (item is HealPotion potion)
            {
                CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + potion.HealPower);
                HealthChanged.Invoke(CurrentHealth);
                potion.Pickup();
            }
        }

        public void Heal(float healPower)
        {
            CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + healPower);
            HealthChanged.Invoke(CurrentHealth);
        }

        internal void Initilization(PlayerPhysic playerPhysic, float maxHealt, float currentHealt)
        {
            base.Initilization(maxHealt, currentHealt);

            _playerPhysic = playerPhysic;
            _playerPhysic.Raised += Heal;
        }
    }
}