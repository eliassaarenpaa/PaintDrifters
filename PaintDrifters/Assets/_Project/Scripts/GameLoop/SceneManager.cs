#region
using UnityEngine;
#endregion

public class SceneManager : MonoBehaviour {

    public void LoadMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene( 0 );
    }

    public void LoadGameScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene( 1 );
    }

    public void ExitGame() {
        Application.Quit();
    }

}