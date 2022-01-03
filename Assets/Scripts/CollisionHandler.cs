using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip missionSuccess;

    int currentSceneIndex;
    PlayerController playerController;
    AudioSource audioSource;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Tagged as Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        audioSource.PlayOneShot(missionSuccess);
        // TODO: Add particle effect upon success
        playerController.enabled = false;
        Invoke("LoadNextLevel", 2f);
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(deathClip);
        // TODO: Add particle effect upon crash
        playerController.enabled = false;
        Invoke("ReloadLevel", 2f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
