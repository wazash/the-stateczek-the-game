using UnityEngine;

public class InputNames
{
    public const string HORIZONTAL_INPUT_NAME = "Horizontal";
    public const string VERTICAL_INPUT_NAME = "Vertical";
    public const string SHOOT_INPUT_NAME = "Fire1";
}

public class InputManager : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private bool shootInput;

    public float HorizontalInput { get { return horizontalInput; } }
    public float VerticalInput { get { return verticalInput;} }
    public bool ShootInput { get { return shootInput;} }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw(InputNames.HORIZONTAL_INPUT_NAME);
        verticalInput = Input.GetAxisRaw(InputNames.VERTICAL_INPUT_NAME);

        shootInput = Input.GetButtonDown(InputNames.SHOOT_INPUT_NAME);
    }
}