using UnityEngine;
using UnityEngine.UI;

public class Mission5 : MonoBehaviour
{
    public Transform handleTransform;
    public Transform rotateTransform;
    public Color normalColor;
    public Color successColor;
    
    private Animator animator;
    private PlayerController playerController;
    private MissionControl missionControl;
    private RectTransform rectTransformHandle;
    private bool isDragHandle;
    private bool isStart = false; 
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        missionControl = FindObjectOfType<MissionControl>();
        rectTransformHandle = handleTransform.GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        if (!isStart)
            return;
        
        if (isDragHandle)
        {
            handleTransform.position = Input.mousePosition;
            rectTransformHandle.anchoredPosition = new Vector2(
                184,
                Mathf.Clamp(rectTransformHandle.anchoredPosition.y, -195, 195));  // 핸들 드래그시 이동하는 y 좌표를 제한함
            if (Input.GetMouseButtonUp(0))
            {
                isDragHandle = false;
                if (-5 < rectTransformHandle.anchoredPosition.y && rectTransformHandle.anchoredPosition.y < 5)
                {
                    isStart = false;
                    SuccessMission();
                    // Invoke(nameof(SuccessMission), 0.2f);
                }
            }
        }

        rotateTransform.eulerAngles = new Vector3(0, 0, 90 * rectTransformHandle.anchoredPosition.y / 195);
        if (-5 < rectTransformHandle.anchoredPosition.y && rectTransformHandle.anchoredPosition.y < 5)
        {
            rotateTransform.GetComponent<Image>().color = successColor;
        }
        else
        {
            rotateTransform.GetComponent<Image>().color = normalColor;
        }
        
    }

    public void StartMission()
    {
        Debug.Log("StartMission 5");
        isStart = true;
        animator.SetBool("IsUp", true);
        playerController = FindObjectOfType<PlayerController>();

        var handleY = 0;
        do
        {
            handleY = Random.Range(-195, 195);
        } while (-10 < handleY && handleY < 10);

        rectTransformHandle.anchoredPosition = new Vector2(184, handleY);
    }

    public void ClickedCancel()
    {
        isStart = false;
        animator.SetBool("IsUp", false);
        playerController.EndMission();
    }
    
    public void ClickedHandle()
    {
        isDragHandle = true;
    }

    private void SuccessMission()
    {
        Debug.Log("SuccessMission 5");
        ClickedCancel();
        missionControl.SuccessMission(gameObject);
    }
}
