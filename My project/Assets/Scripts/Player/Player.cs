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
    void Start(){
        //mengambil informasi dari PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
        //mengambil informasi dari Animator pada EngineEffects
        GameObject engineEffects = GameObject.Find("EngineEffects");
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
    }
}
