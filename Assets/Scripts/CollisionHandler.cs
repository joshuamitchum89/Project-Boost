using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip missionSuccess;
    
    [SerializeField] ParticleSystem explodeParticles;
    
    GameObject landingPad;

    ParticleSystem successParticles;
    ParticleSystem portalParticles;

    int currentSceneIndex;
    PlayerController playerController;
    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        landingPad = GameObject.Find("Landing Pad");
        successParticles = landingPad.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
        portalParticles = landingPad.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning)
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
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(missionSuccess);
        successParticles.Play();
        playerController.thrustParticles.Stop();
        playerController.enabled = false;
        Invoke("LoadNextLevel", 2f);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        explodeParticles.Play();
        playerController.thrustParticles.Stop();
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
