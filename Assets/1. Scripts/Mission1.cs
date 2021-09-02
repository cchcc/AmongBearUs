using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    public Color selectedColor;
    public Image[] hexagonImages;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartMission()
    {
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
        ClickedCancel();
    }
}
