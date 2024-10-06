using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private List<Coin> _coins;

    private void Start()
    {
        _coins = new List<Coin>();
    }

    public void AddCoin(Coin coin)
    {
        _coins.Add(coin);
    }
}
