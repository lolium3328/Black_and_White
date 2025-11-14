using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScytheScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject AimPoint;
    private PlayerInputActions playerInputActions;
    public GameObject Blade;
    private bool isWaiting = false;

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += ctx => {
            if (isWaiting) return;
            isWaiting = true;
            StartCoroutine(Attack());
        };
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.05f); // 攻击前摇
        Blade.SetActive(true);
        yield return new WaitForSeconds(0.1f); // 刀锋碰撞箱显示
        Blade.SetActive(false);
        yield return new WaitForSeconds(0.5f); // 攻击后摇
        isWaiting = false;
    }

    private void Update()
    {
        transform.position = Player.transform.position;
        Vector3 aimPos = AimPoint.transform.position;
        Vector3 dir = aimPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
