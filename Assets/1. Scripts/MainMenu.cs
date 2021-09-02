using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionUI;
    public void ClickedQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        
    }
    
    public void ClickedMission()
    {
        gameObject.SetActive(false);
        missionUI.SetActive(true);

        var player = Instantiate(Resources.Load("Character"), new Vector3(0, -2, 0), Quaternion.identity) as GameObject;
        var playerController = player!.GetComponent<PlayerController>();
        playerController.mainMenuUI = gameObject;
        playerController.missionUI = missionUI;
    }
    
    public void ClickedKill()
    {
        
    }
}
