using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPointScript : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.MousePosition.performed += ctx =>
        {
            Vector2 mouseScreenPos = ctx.ReadValue<Vector2>();
            // 屏幕坐标 → 世界坐标转换
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f)
            );
            transform.position = worldPos;
        };
    }   

     private void Update()
    {

    }
}
