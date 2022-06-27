using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded;
    public GameObject gameOverUI;

    private void Start()
    {
        gameEnded = false;
    }
    void Update()
    {
        if(gameEnded){
            return;
        }

        if(PlayerStats.lives <= 0){
            EndGame();
        }
    }

    private void EndGame(){
        gameEnded = true;
        gameOverUI.SetActive(true);
    }
}
