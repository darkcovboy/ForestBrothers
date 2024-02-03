using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : IInput
{
    public Vector3 GetInputDirection()
    {
        float horizontalInput = SimpleInput.GetAxis(UsefulConst.HorizontalAxis);
        float verticalInput = SimpleInput.GetAxis(UsefulConst.VerticalAxis);

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        return moveDirection;
    }
}
