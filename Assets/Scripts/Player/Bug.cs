using UnityEngine;
using System;

namespace Player
{
    internal class Bug : MonoBehaviour
    {
        [SerializeField] private int _coinAmount;

        private PlayerPhysic _playerPhysics;

        internal event Action<int> CoinAmountChanged;

        private void OnDestroy()
        {
            _playerPhysics.Raised -= AddCoin;
        }

        internal void Initilization(PlayerPhysic playerPhysic, int coinAmount)
        {
            _coinAmount = coinAmount;
            _playerPhysics = playerPhysic;
            _playerPhysics.Raised += AddCoin;
        }

        private void AddCoin(Item item)
        {
            if (item is Coin coin)
            {
                CoinAmountChanged?.Invoke(++_coinAmount);
                coin.Pickup();
            }
        }
    }
}
