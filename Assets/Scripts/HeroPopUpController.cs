using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HeroPopUpController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _attackPowerText;
    [SerializeField] private TextMeshProUGUI _experienceText;

    private void Awake()
    {
        Toggle(false,true);
    }

    public void SetHeroInfo(Combantant_SO combantant, Hero hero, Vector3 position)
    {
        hero.UpdateStats();
        _nameText.text = combantant.Name;
        _levelText.text = hero.Level.ToString();
        _attackPowerText.text = hero.AttackPower.ToString();
        _experienceText.text = hero.Experience.ToString();

        var offset = Locator.Instance.GameSettings.HeroPopUpOffset;
        transform.position = position + offset;
    }

    public void ToggleOffAfterWhile() => StartCoroutine(ToggleOffAfterWhileCoroutine());

    private IEnumerator ToggleOffAfterWhileCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        Toggle(false);
    }

    public void Toggle(bool on,bool instant = false)
    {
        var duration = instant ? 0f : 0.15f;
        var targetScale = on ? Vector3.one : Vector3.zero;

        transform.DOScale(targetScale, duration);
    }
}
