using UnityEngine;

namespace Kelvin{
    public class Player : MonoBehaviour
{
    // This for getting the instace of Player Singleton
    public static Player Instance { get; private set; }

    // Getting the PlayerMovement methods
    PlayerMovement playerMovement;
    // Animator
    Animator animator;


    // Key for Singleton
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Getting Component
    void Start()
    {
        // Get PlayerMovement components
        playerMovement = GetComponent<PlayerMovement>();

        // Get Animator components
        animator = GameObject.Find("EngineEffects").GetComponent<Animator>();
    }

    // Using FixedUpdate to Move because of physics
    void FixedUpdate()
    {
        playerMovement.Move();
    }

    // LateUpdate for animation related
    void LateUpdate()
    {
        playerMovement.MoveBound();
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }

    private WeaponPickup currentWeaponPickup;

    public void SwitchWeapon(Weapon newWeapon, WeaponPickup newWeaponPickup)
    {
        if (currentWeaponPickup != null)
        {
            currentWeaponPickup.PickupHandler(true);  // Make the previous weapon pickup visible again
        }
        currentWeaponPickup = newWeaponPickup;
    }
}

}
