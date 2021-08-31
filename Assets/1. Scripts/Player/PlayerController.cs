using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject joyStick;
    public bool isJoyStick;
    
    void Start()
    {
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        joyStick.SetActive(isJoyStick);
        
        if (isJoyStick)
        {
            
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                var direction = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f))
                    .normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
