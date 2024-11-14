using System.Collections;
using Kelvin;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton
public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;

        // animator.SetTrigger("startTransition");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);

        animator.SetTrigger("endTransition");

        Player.Instance.transform.position = new(0, -4.5f);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
