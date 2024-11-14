using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
  [SerializeField] private float shootIntervalInSeconds = 3f;


  [Header("Bullets")]
  public Bullet bullet;
  [SerializeField] private Transform bulletSpawnPoint;


  [Header("Bullet Pool")]
  private IObjectPool<Bullet> objectPool;


  private readonly bool collectionCheck = false;
  private readonly int defaultCapacity = 30;
  private readonly int maxSize = 100;
  private float timer;
  public Transform parentTransform;

    void Awake(){
        objectPool = new ObjectPool<Bullet>(BulletFactory, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private Bullet BulletFactory(){
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet bulletInstance){
        bulletInstance.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet bulletInstance){
        bulletInstance.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet bulletInstance){
        Destroy(bulletInstance.gameObject);
    }

    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0) && Time.time > timer && objectPool != null){
            timer = Time.time + shootIntervalInSeconds;
            Bullet bulletInstance = objectPool.Get();
            if (bulletInstance != null){
                return;
            }
            bulletInstance.transform.position = bulletSpawnPoint.position;
            bulletInstance.transform.rotation = bulletSpawnPoint.rotation;
            bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletInstance.transform.up * bulletInstance.bulletSpeed;
            
        }
    }

}
