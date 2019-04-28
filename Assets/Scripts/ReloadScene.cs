
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    bool readyToReload = false;

    void Update()
    {
        if(readyToReload && Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    void AnimReload()
    {
        readyToReload = true;
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
