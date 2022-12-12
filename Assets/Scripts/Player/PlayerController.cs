using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        limits = Camera.main.ViewportToWorldPoint(new Vector2(moveAreaPercent, 0)).x;
        shoot = GetComponent<Shoot>();
    }

    private void Update()
    {
        if (GameManager.Instance.isPaused == false)
        {
            Movement();
            if (GameManager.Instance.enemiesOn) Shoot();
        }
    }

    [Header("Movement")]

    [Range(0, 10)] public float speed;

    [Tooltip("Percentage of total camera area where player is able to move")]
    [Range(0, 1)] public float moveAreaPercent;

    private float limits;
    float Clickpos = 0;
    [Range(0.1f, 10.0f )]
    public float touchSensReset;
    private void Movement()
    {

        float direction = Input.acceleration.x;
#if UNITY_EDITOR
        //direction = Input.GetAxisRaw("Horizontal");
#endif
 
        //TOUCH LEFT SIZE
        /*if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.position.x < Screen.width / 2)
                {
                    
                    if (touch.phase == TouchPhase.Began)
                    {
                        Clickpos = touch.position.x;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        float value = touch.position.x - Clickpos;
                        direction = value / Mathf.Abs(value);
                        if(Mathf.Abs(value) < touchSensReset)
                        {
                            Clickpos = touch.position.x;
                        }
                        
                    }
                }
            }
        }*/

        

        float velocity = speed * Time.deltaTime;
        Debug.Log(direction);
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x += direction * velocity, -limits, limits);
        transform.position = pos;
    }

    [Header("Shoot")]

    public KeyCode shootKey;
    [Range(0, 3)] public float delay;
    private float timer;
    private bool canShoot = true;
    private Shoot shoot;

    private void Shoot()
    {
        if (Input.GetKey(shootKey))
            if (canShoot) { canShoot = false; shoot.ShootBullet(); }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (/*touch.position.x > Screen.width / 2 &&*/ touch.phase == TouchPhase.Began)
                {
                    if (canShoot) { canShoot = false; shoot.ShootBullet(); }
                }
            }
        }

        BulletsDelay();

    }
    private void BulletsDelay()
    {
        if (canShoot == false) timer += Time.deltaTime;
        if (timer >= delay) { canShoot = true; timer = 0;  }
    }

    public void Destroy() => gameObject.SetActive(false);
}
