using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Tagged as Friendly");
                break;
            case "Finish":
                Debug.Log("Tagged as Finish");
                break;
            case "Fuel":
                Debug.Log("Tagged as Fuel");
                break;
            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
}
