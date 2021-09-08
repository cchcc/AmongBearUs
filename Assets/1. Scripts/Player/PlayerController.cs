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
    public GameObject playUI;
    public Button actionButton;
    public Sprite useSprite;
    public Sprite killSprite;
    public Text coolTimeText;

    private Animator animator;
    private GameObject collided;
    private bool IsMission => playUI.name == "Mission";
    private float coolTimer;
    private bool isCoolTime;
    private KillControl killControl;
    private bool isPlayKillAnim;

    void Start()
    {
        animator = GetComponent<Animator>();
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);

        if (IsMission)
        {
            actionButton.GetComponent<Image>().sprite = useSprite;
            coolTimeText.text = "";
        }
        else
        {
            actionButton.GetComponent<Image>().sprite = killSprite;
            coolTimer = 5;
            isCoolTime = true;
            isPlayKillAnim = false;
            killControl = FindObjectOfType<KillControl>();
        }
    }

    
    void Update()
    {
        if (canMove)
        {
            Move();
        }

        if (isCoolTime)
        {
            coolTimer -= Time.deltaTime;
            coolTimeText.text = Mathf.Ceil(coolTimer).ToString();
            if (coolTimer <= 0)
            {
                isCoolTime = false;
                coolTimeText.text = "";
            }
        }

        var doneKillAnim = isPlayKillAnim &&
                           killControl.killAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
        if (doneKillAnim)
        {
            killControl.killAnim.SetActive(false);
            canMove = true;
            isPlayKillAnim = false;
            killControl.Kill();
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
            
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject()) // 터치 이벤트가 있는 오브젝트.. 가 아닌경우에만
#else 
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) // 터치 이벤트가 있는 오브젝트.. 가 아닌경우에만
#endif
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
        if (IsMission && other.CompareTag("Mission"))
        {
            actionButton.interactable = true;
            collided = other.gameObject;
        } 
        else if (!IsMission && other.CompareTag("NPC") && !isCoolTime)
        {
            actionButton.interactable = true;
            collided = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        actionButton.interactable = false;
        collided = null;
    }

    public void ClickedUse()
    {
        if (IsMission)
        {
            collided.SendMessage("StartMission");    
        }
        else
        {
            Kill();
        }
        
        canMove = false;
        actionButton.interactable = false;
    }

    private void Kill()
    {
        killControl.killAnim.SetActive(true);
        isPlayKillAnim = true;
        
        collided.SendMessage("Dead");
        collided.GetComponent<CircleCollider2D>().enabled = false;

        
    }

    public void EndMission()
    {
        canMove = true;
        actionButton.interactable = true;
    }
}
