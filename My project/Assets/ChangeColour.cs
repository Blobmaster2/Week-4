using UnityEngine;

public class ChangeColour : ICommand
{
    private SpriteRenderer _renderer;

    private Color _color;
    private Color _previousColor;

    public ChangeColour(GameObject gameObject, Color newColor)
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();

        _previousColor = _renderer.color;
        _color = newColor;

        CommandManager.Instance.AddCommand(this);
    }

    public void Execute()
    {
        _renderer.color = _color;
    }

    public void Undo()
    {
        _renderer.color = _previousColor;
    }
}

