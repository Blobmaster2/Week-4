using Blobmaster2.CommandLib;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;
    private Stack<ICommand> undoStack = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null)
            {
                return;
            }

            if (hit.collider.gameObject.tag == "Square")
            {
                Color randomColour = new Color(Random.value, Random.value, Random.value);

                var command = new ChangeColour(hit.collider.gameObject, randomColour);

                AddCommand(command);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            UndoCommand();
        }
    }

    public void AddCommand(ICommand command)
    {
        undoStack.Push(command);

        command.Execute();
    }

    public void UndoCommand()
    {
        if (undoStack.Count <= 0)
        {
            return;
        }

        undoStack.Pop().Undo();
    }
}