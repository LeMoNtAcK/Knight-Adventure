using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField]private float movingSpeed = 5f;

    private Rigidbody2D rb;
    //

    private float minMovingSpeed = 0.1f;

    private bool isRunning = false;
       
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        

    }

    private void Start()
    {
        Gameinput.Instance.OnPlayerAttack += Gameinput_OnPlayerAttack;
    }

    private void Gameinput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        //Vector2 inputVecor = new Vector2(0, 0);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVecor.y = 1f;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVecor.y = -1f;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVecor.x = -1f;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVecor.x = 1f;
        //}      
    }
    private void HandleMovement()
    {
        Vector2 inputVector = Gameinput.Instance.GetMovementVector();
        //Vector2 targetVelocity = inputVector * movingSpeed;

        //rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, 0.1f);
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

    }

    public bool IsRunning() {
        return isRunning;
    }

    public Vector3 GetPlayerPosition()
    {
        Vector3 PlayerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return PlayerScreenPosition;
    }

}
