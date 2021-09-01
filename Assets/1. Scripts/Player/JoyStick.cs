using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public RectTransform stick;  // 가운데 원
    public RectTransform backGround;  // 바깥 원
    private bool isDrag;
    private float limit;
    private PlayerController playerController;
    
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        limit = backGround.rect.width * 0.5f;
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrag)
        {
            var stickPos = Input.mousePosition - backGround.position;
            // localPosition: 부모기준에서 위치값
            // position: Scene 기준에서 위치값
            // ClampMagnitude(): 파라매터 벡터의 최대값을 제한한다
            stick.localPosition = Vector2.ClampMagnitude(stickPos, limit);

            var direction = (stick.position - backGround.position).normalized;
            transform.position += direction * playerController.speed * Time.deltaTime;
            
            var movingLeft = direction.x < 0;
            if (movingLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);  // 캐릭 이미지 이동방향에 따라 좌우 반전
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);  // 캐릭 이미지 이동방향에 따라 좌우 반전
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                stick.localPosition = new Vector3(0, 0, 0);
                isDrag = false;
                animator.SetBool("IsWalk", false);
            }
        }
    }

    public void ClickedStick()
    {
        isDrag = true;
        animator.SetBool("IsWalk", true);
    }
}
