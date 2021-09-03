using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission4 : MonoBehaviour
{
    public Transform numbers;
    public Color selectedColor;
    
    private Animator animator;
    private PlayerController playerController;
    private int numberCount;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartMission()
    {
        Debug.Log("StartMission 4");
        animator.SetBool("IsUp", true);
        playerController = FindObjectOfType<PlayerController>();
        
        var relocateCount = 10;
        while (relocateCount-- > 0)
        {
            // 랜덤하게 재배치
            var child = numbers.GetChild(relocateCount);
            var currentImage = child.GetComponent<Image>();
            var currentSprite = currentImage.sprite;
            var randomImage = numbers.GetChild(Random.Range(0, 10)).GetComponent<Image>();
            currentImage.sprite = randomImage.sprite;
            randomImage.sprite = currentSprite;

            // 초기 색상 설정
            currentImage.color = Color.white;
            child.GetComponent<Button>().enabled = true;
        }

        numberCount = 1;
        
        // Grid Layout Group:
    }

    public void ClickedCancel()
    {
        animator.SetBool("IsUp", false);
        playerController.EndMission();
    }

    public void ClickedNumber()
    {
        var selectedImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        var selectedNumber = selectedImage.sprite.name;
        var correct = numberCount.ToString() == selectedNumber;
        if (correct)
        {
            selectedImage.color = selectedColor;
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
            ++numberCount;

            var isDone = numberCount == 11;
            if (isDone)
            {
                Invoke(nameof(SuccessMission), 0.2f);
            }
        }
        
    }

    private void SuccessMission()
    {
        Debug.Log("SuccessMission 4");
        ClickedCancel();
    }
}
