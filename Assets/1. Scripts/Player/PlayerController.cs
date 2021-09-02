using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject joyStick;
    public Settings settings;
    public bool canMove = true;
    public GameObject mainMenuUI;
    public GameObject missionUI;
    public Button useButton;

    private Animator animator;
    private GameObject collidedMission;

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
                        transform.localScale = new Vector3(-1, 1, 1); // 이동방향에 따라 캐릭터 이미지 좌우 반전
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1); // 이동방향에 따라 캐릭터 이미지 좌우 반전
                    }
                }
                else
                {
                    animator.SetBool("IsWalk", false);
                }
            }
        }
    }

    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mission"))
        {
            useButton.interactable = true;
            collidedMission = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        useButton.interactable = false;
        collidedMission = null;
    }

    public void ClickedUse()
    {
        collidedMission.SendMessage("StartMission");
        canMove = false;
        useButton.interactable = false;
    }

    public void EndMission()
    {
        canMove = true;
        useButton.interactable = true;
    }
}
