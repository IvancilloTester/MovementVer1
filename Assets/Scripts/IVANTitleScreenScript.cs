using UnityEngine;
using UnityEngine.SceneManagement;

public class IVANTitleScreenScript : MonoBehaviour
{
    public void Play() 

    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()

    {
        Debug.Log("Exiting...");
        Application.Quit();
    
    }
}
