using UnityEngine;
using UnityEngine.UI;

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info
}

[CreateAssetMenu(fileName = "ButtonStyle", menuName = "Noodlepop/Samples/UI/Button Style")]
public class ButtonStyle : ScriptableObject
{
    [SerializeField]
    private Color _textColor;
    public Color TextColor => _textColor;

    [SerializeField]
    private Color _backgroundColor;
    public Color NormalColor => _backgroundColor;

    [SerializeField]
    private Color _highlightedColor;
    public Color HighlightedColor => _highlightedColor;

    [SerializeField]
    private Color _pressedBackgroundColor;
    public Color PressedBackgroundColor => _pressedBackgroundColor;

    [SerializeField]
    private Color _selectedColor;
    public Color SelectedColor => _selectedColor;

    [SerializeField]
    private Color _disabledColor;
    public Color DisabledColor => _disabledColor;

    private void Reset()
    {
        ColorBlock defaultColorBlock =ColorBlock.defaultColorBlock;

        _textColor = Color.black;
        _backgroundColor = defaultColorBlock.normalColor;
        _highlightedColor = defaultColorBlock.highlightedColor;
        _pressedBackgroundColor = defaultColorBlock.pressedColor;
        _selectedColor = defaultColorBlock.selectedColor;
        _disabledColor = defaultColorBlock.disabledColor;
    }
}
