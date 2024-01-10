using System;
using UnityEngine;
using UnityEngine.UI;

public class StyledButton : SimpleButton
{
    [SerializeField]
    private ButtonStyle _defaultStyle;

    private void Awake()
    {
        if(_defaultStyle != null)
            SetStyle(_defaultStyle);
    }

    public void SetStyle(ButtonStyle style)
    {
        _text.color = style.TextColor;

        _button.colors = new ColorBlock()
        {
            colorMultiplier = 1,
            disabledColor = style.DisabledColor,
            highlightedColor = style.HighlightedColor,
            normalColor = style.NormalColor,
            pressedColor = style.PressedBackgroundColor,
            selectedColor = style.SelectedColor,
        };
    }
}
