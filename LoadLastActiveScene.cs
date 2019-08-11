using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastActiveScene : MonoBehaviour {

    public void LoadActive()
    {
        SceneManager.LoadScene(PlayerStats.GetLastActiveSceneName());
    }
}
