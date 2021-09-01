using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject joyStick;
    public Settings settings;
    public bool canMove = true;
    
    private  Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);
    }

    
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        joyStick.SetActive(settings.isJoyStick);
        
        if (settings.isJoyStick)
        {
            
        }
        else
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // 터치 이벤트가 있는 오브젝트.. 가 아닌경우에만
            {
                if (Input.GetMouseButton(0))
                {
                    var direction = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f))
                        .normalized;
                    transform.position += direction * speed * Time.deltaTime;
                    animator.SetBool("IsWalk", true);

                    var movingLeft = direction.x < 0;
                    if (movingLeft)
                    {
                        transform.localScale = new Vector3(-1, 1, 1); // 캐릭 이미지 이동방향에 따라 좌우 반전
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1); // 캐릭 이미지 이동방향에 따라 좌우 반전
                    }
                }
                else
                {
                    animator.SetBool("IsWalk", false);
                }
            }
        }
    }
}
