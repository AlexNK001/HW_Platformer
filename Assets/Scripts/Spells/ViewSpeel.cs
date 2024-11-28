using UnityEngine;
using UnityEngine.UI;

public class ViewSpeel : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Image _main;
    [SerializeField] private Image _filling;
    [SerializeField] private Image _foregroundProgressBar;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Vampirism _vampirism;

    private void Start()
    {
        _main.sprite = _icon;
        _filling.sprite = _icon;
        _vampirism.SpellIsCast += ShowProgressBar;
        _vampirism.SpellIsRecharged += ShowRechage;

        _foregroundProgressBar.enabled = false;
        _progressBar.enabled = false;
    }

    private void ShowRechage(float normalizedRecargeTime)
    {
        _filling.fillAmount = normalizedRecargeTime;
    }

    private void ShowProgressBar(float normalizedCastTime)
    {
        bool haveEnableProgressBar = normalizedCastTime > 0;
        _foregroundProgressBar.enabled = haveEnableProgressBar;
        _progressBar.enabled = haveEnableProgressBar;

        _progressBar.fillAmount = 1f - normalizedCastTime;
    }
}
