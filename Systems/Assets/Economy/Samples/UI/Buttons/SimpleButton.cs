using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI _text;

    [SerializeField]
    protected Button _button;

    // Start is called before the first frame update
    void OnValidate()
    {
        _text = _text == null ? GetComponentInChildren<TextMeshProUGUI>() : _text;
        _button = _button == null ? GetComponentInChildren<Button>() : _button;
    }

    public void SetLabel(string t)
    {
        _text.text = t;
    }

    public void SetColor(ColorBlock colors)
    {
        _button.colors = colors;
    }

    public void AddListener(UnityAction call)
    {
        _button.onClick.AddListener(call);
    }

    public void RemoveListener(UnityAction call)
    {
        _button.onClick.RemoveListener(call);
    }
}
