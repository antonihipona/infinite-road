using UnityEngine;


public enum MovementType { None, Left, Right };

public class TouchController : MonoBehaviour
{
  public MovementType MovementType = MovementType.None;

  private void Update()
  {
    CheckMovement();
  }

  public void CheckMovement()
  {
    bool touchLeft = false;
    bool touchRight = false;

    foreach (Touch touch in Input.touches)
    {
      if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
      {
        float halfWidth = Screen.width / 2;
        if (touch.position.x < halfWidth) touchLeft = true;
        if (touch.position.x > halfWidth) touchRight = true;
      }
    }

    if (touchLeft && !touchRight) MovementType = MovementType.Left;
    if (!touchLeft && touchRight) MovementType = MovementType.Right;
    if ((touchLeft && touchRight) || (!touchLeft && !touchRight)) MovementType = MovementType.None;
  }
}
