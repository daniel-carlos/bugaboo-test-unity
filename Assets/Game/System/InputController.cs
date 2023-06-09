using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerController controller;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (controller.inputType)
        {
            case InputType.Keyboard:
                ProcessKeyboardInput();
                break;
            case InputType.Gamepad:
                ProcessGamepadInput();
                break;
            default:
                break;
        }

    }


    void ProcessKeyboardInput()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        Vector2 inputDirection = Vector2.zero;
        if (kb.aKey.isPressed) inputDirection.x -= 1;
        if (kb.dKey.isPressed) inputDirection.x += 1;
        if (kb.sKey.isPressed) inputDirection.y -= 1;
        if (kb.wKey.isPressed) inputDirection.y += 1;

        controller.Move(inputDirection);
    }

    void ProcessGamepadInput()
    {
        Gamepad gp = Gamepad.current;
        if (gp == null) return;

        controller.Move(gp.leftStick.ReadValue());
    }
}
