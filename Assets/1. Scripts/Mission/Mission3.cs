using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission3 : MonoBehaviour
{
    public Text inputText;
    public Text keycodeText;
    
    private Animator animator;
    private PlayerController playerController;
    private MissionControl missionControl;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        missionControl = FindObjectOfType<MissionControl>();
    }

    public void StartMission()
    {
        Debug.Log("StartMission 3");
        animator.SetBool("IsUp", true);
        playerController = FindObjectOfType<PlayerController>();

        inputText.text = "";
        keycodeText.text = "";
        
        var keycodeSize = 5;
        while (keycodeSize-- > 0)
        {
            keycodeText.text += Random.Range(0, 10).ToString();
        }
    }

    public void ClickedCancel()
    {
        animator.SetBool("IsUp", false);
        playerController.EndMission();
    }

    public void ClickedNumber()
    {
        if (inputText.text.Length > 4)
            return;
        
        var objectName = EventSystem.current.currentSelectedGameObject.name;
        var number = "";
        switch (objectName)
        {
            case "Button0":
                number = "0";
                break;
            case "Button1":
                number = "1";
                break;
            case "Button2":
                number = "2";
                break;
            case "Button3":
                number = "3";
                break;
            case "Button4":
                number = "4";
                break;
            case "Button5":
                number = "5";
                break;
            case "Button6":
                number = "6";
                break;
            case "Button7":
                number = "7";
                break;
            case "Button8":
                number = "8";
                break;
            case "Button9":
                number = "9";
                break;
        }
        inputText.text += number;
    }

    public void ClickedDelete()
    {
        var text = inputText.text;
        if (text.Length == 0)
            return;

        inputText.text = text.Substring(0, text.Length - 1);
    }

    public void ClickedCheck()
    {
        if (inputText.text == keycodeText.text)
        {
            Invoke(nameof(SuccessMission), 0.2f);
        }
    }

    private void SuccessMission()
    {
        Debug.Log("SuccessMission 3");
        ClickedCancel();
        missionControl.SuccessMission(gameObject);
    }
}
