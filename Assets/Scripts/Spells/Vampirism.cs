using System;
using System.Collections;
using UnityEngine;
using Player;

public class Vampirism : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _radius;
    [SerializeField, Min(0f)] private float _duration = 6f;
    [SerializeField, Min(0f)] private float _recharge = 4f;
    [SerializeField, Min(0f)] private float _spellPower = 70f;
    [SerializeField] private PlayerHeart _playerHeart;
    [SerializeField] private TargetMarker _marker;
    [SerializeField] private Vector3 _markerOffset;
    [SerializeField] private SpeelCollider _speelCollider;

    private UserInput _userInput;
    private bool _isCast;

    public Action<float> SpellIsCast;
    public Action<float> SpellIsRecharged;

    private void OnDestroy()
    {
        _userInput.SpellCast -= TryCast;
    }

    internal void Initilization(UserInput userInput)
    {
        _userInput = userInput;
        _userInput.SpellCast += TryCast;

        _marker.gameObject.SetActive(false);

        _speelCollider.gameObject.SetActive(false);
    }

    private void TryCast()
    {
        if (_isCast == false)
        {
            StartCoroutine(TakeHealth());
        }
    }

    private IEnumerator TakeHealth()
    {
        _isCast = true;
        _speelCollider.gameObject.SetActive(true);

        float castTime = _duration;
        float recharTime = _recharge;

        while (castTime > 0f)
        {
            bool haveHeart = _speelCollider.TryFindNearestTarget(out Heart heart);
            _marker.gameObject.SetActive(haveHeart);

            if (haveHeart)
            {
                float damage = _spellPower * Time.deltaTime / _duration;
                heart.TakeDamage(damage);
                _playerHeart.Heal(damage);
                _marker.transform.position = heart.transform.position + _markerOffset;
            }

            castTime -= Time.deltaTime;
            SpellIsCast?.Invoke(Mathf.InverseLerp(0f, _duration, castTime));

            yield return null;
        }

        _speelCollider.gameObject.SetActive(false);
        _marker.gameObject.SetActive(false);

        while (recharTime > 0f)
        {
            recharTime -= Time.deltaTime;
            SpellIsRecharged?.Invoke(Mathf.InverseLerp(0f, _recharge, recharTime));

            yield return null;
        }

        _isCast = false;
    }
}
