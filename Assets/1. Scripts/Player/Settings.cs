using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isJoyStick = false;  // TODO: PlayerController 로 이동시키고 버튼눌렷을때 좌하단 조이스틱 가시성변경하기 
    public Image touchButtonImage;
    public Image joyStickButtonImage;
    public Color selectedColor;
    public PlayerController playerController;
    public GameObject settingButton;
    
    public GameObject mainMenuUI;
    public GameObject playUI;
    
    void Start()
    {
        touchButtonImage.color = selectedColor;
        mainMenuUI = playerController.mainMenuUI;
        playUI = playerController.playUI;
    }
    
    public void ClickedSetting()
    {
        gameObject.SetActive(true);
        settingButton.SetActive(false);
        playerController.canMove = false;
    }

    public void ClickedBack()
    {
        gameObject.SetActive(false);
        settingButton.SetActive(true);
        playerController.canMove = true;
    }

    public void ClickedTouch()
    {
        isJoyStick = false;
        touchButtonImage.color = selectedColor;
        joyStickButtonImage.color = Color.white;
    }

    public void ClickedJoyStick()
    {
        isJoyStick = true;
        touchButtonImage.color = Color.white;
        joyStickButtonImage.color = selectedColor;
    }

    public void ClickedQuit()
    {
        mainMenuUI.SetActive(true);
        playUI.SetActive(false);
        playerController.DestroyPlayer();
    }
}
