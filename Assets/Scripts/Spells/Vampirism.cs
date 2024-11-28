using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

//Продолжаем работать над платформером.

//Добавим игроку возможность вытягивать здоровье из врагов.
//Способность должна активироваться нажатием кнопки,
//на всём протяжении действия способности плавно должен наноситься урон тому врагу в области действия,
//который является ближайшим, если в области вампиризма нет врагов, то никому урон не наносится,
//но способность от этого не должна выключаться,
//она должна работать строго 6 секунд и после окончания перед следующим запуском способность должна зарядиться,
//время перезарядки после выключения должна составлять 4 секунды.

//Отрисуйте радиус действия способности (просто прозрачный спрайт за персонажем),
//чтобы было видно, что она активировалась и визуально был виден радиус.
//В виде числа или бара визуально отображать время действия способности.
//Как вариант для бара, уменьшение значение бара, пока используется способность, а увеличение - пока работает перезарядка способности. 

//namespace Player
//{
public class Vampirism : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField, Min(0f)] private float _radius;
    [SerializeField, Min(0f)] private float _duration = 6f;
    [SerializeField, Min(0f)] private float _recharge = 4f;
    [SerializeField, Min(0f)] private float _spellPower = 70f;
    [SerializeField] private PlayerHeart _playerHeart;
    [SerializeField] private TargetMarker _marker;
    [SerializeField] private Vector3 _markerOffset;
    [SerializeField] private SpriteRenderer _scopeAction;

    private UserInput _userInput;
    private Collider2D[] _results;
    private bool _isCast;

    public Action<float> SpellIsCast;
    public Action<float> SpellIsRecharged;

    //private void Start()
    //{
    //    _results = new Collider2D[30];
    //    _scopeAction.transform.localScale *= _radius * 2;

    //    _marker.gameObject.SetActive(false);
    //    _scopeAction.enabled = false;
    //}
    private void OnDestroy()
    {
        _userInput.SpellCast -= TryCast;
    }

    internal void Initilization(UserInput userInput)
    {
        _userInput = userInput;
        _userInput.SpellCast += TryCast;

        _results = new Collider2D[30];
        _scopeAction.transform.localScale *= _radius * 2;

        _marker.gameObject.SetActive(false);
        _scopeAction.enabled = false;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z) && _isCast == false)
    //    {
    //        Heart enemyHeart = FindNearestTarget();
    //        StartCoroutine(TakeHealth(enemyHeart));
    //    }
    //}

    private void TryCast()
    {
        Heart enemyHeart = FindNearestTarget();
        StartCoroutine(TakeHealth(enemyHeart));
    }

    private Heart FindNearestTarget()
    {
        int count = Physics2D.OverlapCircle(transform.position, _radius, _filter, _results);
        float currentMinDistance = float.MaxValue;
        Heart currentNearestHeart = null;

        if (count <= 0)
            return currentNearestHeart;

        for (int i = 0; i < _results.Length; i++)
        {
            Collider2D currentCollider = _results[i];

            if (currentCollider != null && currentCollider.TryGetComponent(out Heart heart))
            {
                float currentDistance = Vector3.Distance(transform.position, heart.transform.position);

                if (currentDistance < currentMinDistance && heart.IsAlive)
                {
                    currentNearestHeart = heart;
                    currentMinDistance = currentDistance;
                }
            }
        }

        return currentNearestHeart;
    }

    private IEnumerator TakeHealth(Heart enemyHeart)
    {
        float castTime = _duration;
        _scopeAction.enabled = true;
        _isCast = true;

        while (castTime > 0f)
        {
            if (enemyHeart != null && enemyHeart.IsAlive)
            {
                float damage = _spellPower * Time.deltaTime / _duration;
                enemyHeart.TakeDamage(damage);
                _playerHeart.Heal(damage);

                _marker.transform.position = enemyHeart.transform.position + _markerOffset;
                _marker.gameObject.SetActive(enemyHeart.IsAlive);
            }
            else
            {
                enemyHeart = FindNearestTarget();
                _marker.gameObject.SetActive(false);
            }

            castTime -= Time.deltaTime;
            SpellIsCast.Invoke(Mathf.InverseLerp(0f, _duration, castTime));

            yield return null;
        }

        SpellIsCast.Invoke(0f);

        float recharTime = _recharge;

        while (recharTime > 0f)
        {
            SpellIsRecharged.Invoke(Mathf.InverseLerp(0f, _recharge, recharTime));
            recharTime -= Time.deltaTime;

            yield return null;
        }

        SpellIsRecharged.Invoke(0f);
        _marker.gameObject.SetActive(false);
        _scopeAction.enabled = false;
        _isCast = false;
    }
}

//}
