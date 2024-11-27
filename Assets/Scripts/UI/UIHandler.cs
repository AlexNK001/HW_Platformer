using UnityEngine;

namespace Player
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private CoinShower _coinShower;
        [SerializeField] private HeartListener _heartListeners;

        internal void Initilization(Bug bug, int coinAmount, PlayerHeart heart, float maxHealth, float currentHealth)
        {
            _coinShower.Initilization(bug, coinAmount);
            _heartListeners.Initilization(heart, maxHealth, currentHealth);
        }
    }
}
