using UnityEngine;
using UnityEngine.UI;

public class UIHeartDisplay : MonoBehaviour
{
    [SerializeField]
    private Image _slidingImage;

    [SerializeField]
    private Image _combatBackgroundImage;

    [SerializeField]
    private Image _backgroundImage;

    [Header("Listening on")]
    [SerializeField]
    private BoolEventChannelSO _combatStateEvent;

    private void OnEnable()
    {
        _combatStateEvent.OnEventRaised += OnCombatState;
    }

    private void OnDisable()
    {
        _combatStateEvent.OnEventRaised -= OnCombatState;
    }

    public void SetImage(float percent)
    {
        _slidingImage.fillAmount = percent;
        if (percent == 0f)
        {
            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0.5f);
        }
        else
        {
            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
        }
    }

    private void OnCombatState(bool isCombat)
    {
        _combatBackgroundImage.gameObject.SetActive(isCombat);
    }
}