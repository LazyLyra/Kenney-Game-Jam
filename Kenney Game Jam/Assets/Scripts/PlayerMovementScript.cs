using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float MoveSpeed;
    [SerializeField] bool IsFacingRight;
    [SerializeField] float angleFromRight;

    [Header("References")]
    public BoxCollider2D BC;
    public Rigidbody2D RB;
    
    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider2D>();
        RB = GetComponent<Rigidbody2D>();

        IsFacingRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
        TurnCheck();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 movement = input.normalized * MoveSpeed * Time.deltaTime;
        RB.velocity = movement;
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        angleFromRight = Vector2.SignedAngle(Vector2.right, direction);

        if (IsFacingRight)
        {
            transform.right = direction;
        }
        else
        {
            transform.right = new Vector2(-direction.x, -direction.y);
        }
        
    }

    private void TurnCheck()
    {

        if (angleFromRight > -90 && angleFromRight < 90 && !IsFacingRight)
        {
            //change from left to right
            FlipX();        
        }
        else if ((angleFromRight > 90 || angleFromRight < -90) && IsFacingRight)
        {
            //change from right to left
            FlipX();
        }
    }

    private void FlipX()
    {
        if (IsFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            IsFacingRight = !IsFacingRight;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            IsFacingRight = !IsFacingRight;
        }
    }
}
