using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    // Singleton dari gameObject Player
    private static Player instance = null ;
    void Awake ( ){
        if ( instance == null ){
        instance = this ;
        DontDestroyOnLoad ( gameObject ) ;
        }
        else if ( instance != this ){
        Destroy ( gameObject ) ;
        }
    }

    public Vector2 screenBounds;
    public float objectWidth;
    public float objectHeight;

    void Start(){
        //mengambil informasi dari PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
        //mengambil informasi dari Animator pada EngineEffects
        GameObject engineEffects = GameObject.Find("EngineEffects");
        GameObject ship = GameObject.Find("Ship");
        if (engineEffects != null){
            animator = engineEffects.GetComponent<Animator>();
            if (animator != null){
                Debug.Log("Animator retrieved successfully from EngineEffects.");
            }
            //error message jika ada masalah
            else{
                Debug.LogWarning("Animator component not found on EngineEffects.");
            }
        }

        if (ship != null){
            objectWidth = ship.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            objectHeight = ship.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        } else {
            Debug.LogWarning("Ship not found");
        }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    void FixedUpdate(){
        //Memanggil method Move dari PlayerMovement
        playerMovement.isMoving = playerMovement.moveVelocity != Vector2.zero;
        if (playerMovement != null){
            playerMovement.Move();
            playerMovement.rb.velocity = playerMovement.moveVelocity;
        }
    }
    void LateUpdate(){
        //pengaturan bool isMoving
        if (playerMovement != null){
            animator.SetBool("IsMoving", playerMovement.isMoving);
        }

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 - objectHeight, screenBounds.y - (objectHeight * 2));
        transform.position = viewPos;
    }
}
