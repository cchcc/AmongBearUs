using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionUI;
    public GameObject killUI;
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
        playerController.playUI = missionUI;
        
        missionUI.SendMessage("ResetMissions");
    }
    
    public void ClickedKill()
    {
        gameObject.SetActive(false);
        killUI.SetActive(true);

        var player = Instantiate(Resources.Load("Character"), new Vector3(0, -2, 0), Quaternion.identity) as GameObject;
        var playerController = player!.GetComponent<PlayerController>();
        playerController.mainMenuUI = gameObject;
        playerController.playUI = killUI;
        
        killUI.SendMessage("ResetKill");
    }
}
