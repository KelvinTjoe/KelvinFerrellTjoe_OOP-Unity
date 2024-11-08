using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup1 : MonoBehaviour{
    [SerializeField] private Weapon weaponHolder; 
    private Weapon weapon;
    private SpriteRenderer weapon_sprite;
    private BoxCollider2D collide;

    void Awake()
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            weapon.transform.SetParent(other.transform);
            weapon.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, 1);

            TurnVisual(true);
        }
    }
    void TurnVisual(bool on)
    {
        
    }
}


