using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    public Color selectedColor;
    public Image[] hexagonImages;
    
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
        Debug.Log("StartMission 1");
        animator.SetBool("IsUp", true);
        playerController = FindObjectOfType<PlayerController>();

        var randomCount = 4;
        while (randomCount-- > 0)
        {
            var initColor = Random.Range(0, 2) == 1 ? selectedColor : Color.white;
            hexagonImages[Random.Range(0, hexagonImages.Length)].color = initColor;
        }
        hexagonImages[Random.Range(0, hexagonImages.Length)].color = selectedColor;  // 최소 하나는 유지함
    }

    public void ClickedCancel()
    {
        animator.SetBool("IsUp", false);
        playerController.EndMission();
    }

    public void ClickedHexagon()
    {
        var buttonImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        if (buttonImage.color == Color.white)
        {
            buttonImage.color = selectedColor;
        }
        else
        {
            buttonImage.color = Color.white;
        }

        var allIsWhite = hexagonImages.All(i => i.color == Color.white);
        if (allIsWhite)
        {
            Invoke(nameof(SuccessMission), 0.2f);
        }
    }

    private void SuccessMission()
    {
        Debug.Log("SuccessMission 1");
        ClickedCancel();
        missionControl.SuccessMission(gameObject);
    }
}
