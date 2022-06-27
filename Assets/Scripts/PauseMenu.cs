using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Toggle();
        }
    }
    public void Restart(){
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit(){
        Toggle();
        SceneManager.LoadScene("MainMenu");
    }

    public void Toggle(){
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf){
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }
}
