using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private float scaleXVelocity = 0f; // добавь поле в класс
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    [SerializeField] private float smoothTime;
    //private const string IS_RUNNING = "IsRunning";
    private static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, player.IsRunning());
        AdjustPlayerFacingDirection();
    }
    private void AdjustPlayerFacingDirection()  
    {
        Vector3 mouseRos = Gameinput.Instance.GetMousePosition();
        Vector3 PlayerPosition = Player.Instance.GetPlayerPosition();

        float direction = (mouseRos.x < PlayerPosition.x) ? -1f : 1f;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.SmoothDamp(scale.x, direction, ref scaleXVelocity, smoothTime);
        transform.localScale = scale;
    }
}
