using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed = 0.15f;
    [SerializeField] float rotateSpeed = 5.0f;

    Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
            ChangePosition();


        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            transform.position = Vector2.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
