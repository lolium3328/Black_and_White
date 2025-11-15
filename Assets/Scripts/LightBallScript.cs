using UnityEngine;

public class LightBallScript : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    private Vector3 launchDir = Vector3.zero;
    private float maxSpeed = 5f;
    private float speed = 0f;
    private float timer = 0f;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 worldPos = Vector3.zero; // 保存鼠标世界坐标


    private void Awake()
    {
        var actions = InputManager.Instance.PlayerInputActions;
        actions.Player.MousePosition.performed += ctx =>
        {
            Vector2 mouseScreenPos = ctx.ReadValue<Vector2>();
            // 更新 worldPos 字段（每次鼠标移动时调用）
            worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f)
            );
        };
    }

    private void Update()
    {
        // 每帧都计算 launchDir，指向当前鼠标位置
        launchDir = (worldPos - transform.position).normalized;
        
        timer += Time.deltaTime;
        if (timer < 2f)
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, maxSpeed);
            moveDir = launchDir;
        }
        transform.position += moveDir * speed * Time.deltaTime;
        
        if (timer >= 5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // 脚本禁用时注销事件
        var actions = InputManager.Instance?.PlayerInputActions;
        if (actions != null)
        {
            actions.Player.MousePosition.performed -= ctx =>
            {
                Vector2 mouseScreenPos = ctx.ReadValue<Vector2>();
                worldPos = Camera.main.ScreenToWorldPoint(
                    new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f)
                );
            };
        }
    }
}
