using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Mission6 : MonoBehaviour
{
    public bool[] linkedValidColor = new bool[4];
    public RectTransform[] rightLines;
    public LineRenderer[] leftLines;
    
    private Animator animator;
    private PlayerController playerController;
    private MissionControl missionControl;
    private bool isDragLine;
    private LineRenderer clickedLineRenderer;
    private Vector2 clickedPos;
    private float leftY;
    private float rightY;
    private Color leftColor;
    private Color rightColor;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        missionControl = FindObjectOfType<MissionControl>();
    }
    
    private void Update()
    {
        if (isDragLine)
        {
            clickedLineRenderer.SetPosition(1, new Vector3(
                (Input.mousePosition.x - clickedPos.x) * 1920f / Screen.width,   // 모바일 화면 사이즈 대응
                (Input.mousePosition.y - clickedPos.y) * 1080f / Screen.height,
                -10f
                ));
            
            if (Input.GetMouseButtonUp(0))
            {
                isDragLine = false;
                
                var ray = Camera.main!.ScreenPointToRay(Input.mousePosition); // 화면에서 마우스 위치로 광선을 쏨
                var reached = Physics.Raycast(ray, out var hit);
                if (reached)
                {
                    var rightLine = hit.transform.gameObject;  // Box Collider 필요 (Box Collider 2D 아님)
                    rightY = rightLine.GetComponent<RectTransform>().anchoredPosition.y;  // 오른선 y
                    rightColor = rightLine.GetComponent<Image>().color;  // 오른선 색
                    clickedLineRenderer.SetPosition(1, new Vector3(500, rightY - leftY, -10));

                    int lineIdxByY = -1;
                    switch (leftY)
                    {
                        case 225: lineIdxByY = 0; break;
                        case 75: lineIdxByY = 1; break;
                        case -75: lineIdxByY = 2; break;
                        case -225: lineIdxByY = 3; break;
                    }
                    linkedValidColor[lineIdxByY] = leftColor == rightColor;

                    if (linkedValidColor.All(isValid => isValid))
                    {
                        Invoke(nameof(SuccessMission), 0.2f);
                    }

                }
                else
                {
                    clickedLineRenderer.SetPosition(1, new Vector3(0, 0, -10f));  // reset position
                }
            }
        }
    }

    public void StartMission()
    {
        Debug.Log("StartMission 6");
        animator.SetBool("IsUp", true);
        playerController = FindObjectOfType<PlayerController>();

        for (var i = 0; i < linkedValidColor.Length; ++i)
        {
            linkedValidColor[i] = false;
            leftLines[i].SetPosition(1, new Vector3(0, 0, -10));
        }

        var count = rightLines.Length;
        while (count-- > 0)
        {
            var tempPosition = rightLines[count].anchoredPosition;
            var randomLine = rightLines[Random.Range(0, rightLines.Length)];
            rightLines[count].anchoredPosition = randomLine.anchoredPosition;
            randomLine.anchoredPosition = tempPosition;
        }
    }

    public void ClickedCancel()
    {
        animator.SetBool("IsUp", false);
        playerController.EndMission();
    }
    
    public void ClickedLine(LineRenderer lineRenderer)
    {
        isDragLine = true;
        clickedLineRenderer = lineRenderer;
        clickedPos = Input.mousePosition;
        leftY = lineRenderer.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;  // 왼선 y
        leftColor = lineRenderer.transform!.parent.GetComponent<Image>().color;
    }

    private void SuccessMission()
    {
        Debug.Log("SuccessMission 6");
        ClickedCancel();
        missionControl.SuccessMission(gameObject);
    }
}
