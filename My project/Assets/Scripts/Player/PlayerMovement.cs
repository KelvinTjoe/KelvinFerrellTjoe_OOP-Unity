using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxspeed = new Vector2(4.0f, 4.0f);
    [SerializeField] Vector2 timetomaxspeed = new Vector2(2.0f, 2.0f);
    [SerializeField] Vector2 timetostop = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 stopclamp = new Vector2(2.0f, 2.0f);
    public Vector2 moveDirection = Vector2.zero;
    public Vector2 moveVelocity = Vector2.zero;
    public Vector2 moveFriction = Vector2.zero;
    public Vector2 stopFriction = Vector2.zero;
    public Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //kalkulasi awal Vector yang diperlukan
        moveVelocity = (2 * maxspeed) / timetomaxspeed; 
        moveFriction = -2 * (maxspeed / (timetomaxspeed * timetomaxspeed)); 
        stopFriction = -2 * (maxspeed / (timetostop * timetostop));         
    }

    public void Update()
    {
        //pengambilan Input 
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    public void Move()
    {
        if (moveDirection != Vector2.zero)
        {
            // percepatan pada moveVelocity dengan moveFriction
            moveVelocity -= moveDirection*getFriction()*Time.fixedDeltaTime;

            // pemakaian Mathf.Clamp untuk mengimplementasi maxSpeed dua arah
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxspeed.x, maxspeed.x);
            moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxspeed.y, maxspeed.y);
        }
        else
        {
            // perlambatan pada moveVelocity dengan stopFiction
            moveVelocity += moveVelocity*getFriction()*Time.fixedDeltaTime;

            // pemberian batas minimal kecepatan saat melambat
            if (Mathf.Abs(moveVelocity.x) < stopclamp.x) {
                moveVelocity.x = 0;
            }
            if (Mathf.Abs(moveVelocity.y) < stopclamp.y) {
                moveVelocity.y = 0;
            }
        }
    }

    public Vector2 getFriction()
    {
        // perubahan gaya gesek yang bergantung pada input
        if(moveDirection != Vector2.zero){
            return moveFriction;
        }
        else{
            return stopFriction;
        }
    }

    public bool isMoving;
}