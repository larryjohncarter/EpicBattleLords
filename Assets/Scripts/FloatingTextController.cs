using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private bool _customDurationBool;
    [SerializeField] private float _duration;

    [SerializeField] private float _moveAmount = 1;
   
    public void SetFloatText(int amount) => _text.text = amount.ToString();

    private void OnEnable()
    {
        ResetCache();
        AnimateText();
    }

    private void AnimateText()
    {
        _text.DOFade(0f, .6f).SetEase(Ease.InSine).SetDelay(.7f);
        var duration = _customDurationBool ? _duration : 1.3f;
        transform.DOMoveY(_moveAmount, duration).SetEase(Ease.OutSine).SetRelative(true)
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void ResetCache()
    {
        transform.DOComplete();
        _text.DOComplete();

        ResetAlpha();
    }

    private void ResetAlpha()
    {
        var textColor = _text.color;
        textColor.a = 1f;
        _text.color = textColor;
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
