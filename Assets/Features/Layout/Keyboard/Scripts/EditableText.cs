using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableText
{
    public event System.Action<EditableText> ValueChanged;

    private string _textValue = string.Empty;
    public string Value 
    { 
        get { return _textValue; }
        private set
        {
            _textValue = value;
            ValueChanged?.Invoke(this);
        }
    }

    private int _caretPosition;
    public int CaretPosition 
    { 
        get { return _caretPosition; }
        set
        {
            Debug.Log("Setting caret position: " + value);
            _caretPosition = Mathf.Clamp(value, 0, _textValue.Length);
            Debug.Log("Caret new position: " + _caretPosition);
        }
    }

    public void Set(string Text)
    {
        Value = Text;
        CaretPosition = Text.Length;
    }

    public void AddText(string Text)
    {
        _textValue = Value.Substring(0, CaretPosition) + Text + Value.Substring(CaretPosition);
        CaretPosition += Text.Length;
        ValueChanged?.Invoke(this);
    }

    public void Backspace()
    {
        //Value = Value.Substring(0, Value.Length - 1);
        Value = Value.Substring(0, CaretPosition - 1) + Value.Substring(CaretPosition);
        CaretPosition--;
    }
}
