using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
        
    }
    
    public void ClickedKill()
    {
        
    }
}
