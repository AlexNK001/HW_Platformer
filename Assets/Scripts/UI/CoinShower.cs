using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class CoinShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsAmount;

        private Bug _bug;

        private void OnDestroy()
        {
            _bug.CoinAmountChanged -= (amount) => _coinsAmount.text = amount.ToString();
        }

        internal void Initilization(Bug bug, int coinAmount)
        {
            _bug = bug;
            _bug.CoinAmountChanged += (amount) => _coinsAmount.text = amount.ToString();

            _coinsAmount.text = coinAmount.ToString();
        }
    }
}
