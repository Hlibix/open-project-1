using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CreditsList
{
    public List<ContributerProfile> Contributors = new();
}

[System.Serializable]
public class ContributerProfile
{
    public string Name;
    public string Contribution;

    public override string ToString()
    {
        return Name + " - " + Contribution;
    }
}

public class UICredits : MonoBehaviour
{
    public UnityAction OnCloseCredits;

    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private TextAsset _creditsAsset;

    [SerializeField]
    private TextMeshProUGUI _creditsText;

    [SerializeField]
    private UICreditsRoller _creditsRoller;

    [Header("Listening on")]
    [SerializeField]
    private VoidEventChannelSO _creditsRollEndEvent;

    private CreditsList _creditsList;

    private void OnEnable()
    {
        _inputReader.MenuCloseEvent += CloseCreditsScreen;
        SetCreditsScreen();
    }

    private void OnDisable()
    {
        _inputReader.MenuCloseEvent -= CloseCreditsScreen;
    }

    private void SetCreditsScreen()
    {
        _creditsRoller.OnRollingEnded += EndRolling;
        FillCreditsRoller();
        _creditsRoller.StartRolling();
    }

    private void CloseCreditsScreen()
    {
        _creditsRoller.OnRollingEnded -= EndRolling;
        OnCloseCredits.Invoke();
    }

    private void FillCreditsRoller()
    {
        _creditsList = new CreditsList();
        var json = _creditsAsset.text;
        _creditsList = JsonUtility.FromJson<CreditsList>(json);
        SetCreditsText();
    }

    private void SetCreditsText()
    {
        var creditsText = "";
        for (var i = 0; i < _creditsList.Contributors.Count; i++)
        {
            if (i == 0)
            {
                creditsText = creditsText + _creditsList.Contributors[i];
            }
            else
            {
                creditsText = creditsText + "\n" + _creditsList.Contributors[i];
            }
        }

        _creditsText.text = creditsText;
    }

    private void EndRolling()
    {
        if (_creditsRollEndEvent != null)
        {
            _creditsRollEndEvent.RaiseEvent();
        }
    }
}