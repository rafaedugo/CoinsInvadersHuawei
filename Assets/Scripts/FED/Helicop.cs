using UnityEngine;

public enum HelicopState
{
    IsMoving,
    IsWaiting,
    InBetween
}
public class Helicop : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] [Range(-1, 1)] private int dir;
    private float timer;
    private float inBetTimer;
    [SerializeField] private float minWait, maxWait;
    private float randomTime;
    private Transform _transform;
    public HelicopState fedCurrentState;
    private bool reached;
    public ParticleSystem particles;
    private bool isDying;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        _transform = transform;
    }
    private void Start()
    {
        SetDirectionOnStart();
        particles.Stop();
        randomTime = UnityEngine.Random.Range(minWait, maxWait);
        fedCurrentState = HelicopState.IsWaiting;
    }

    private void SetDirectionOnStart()
    {
        if (_transform.position.x >= 14) dir = -1;
        else if (_transform.position.x <= -14) dir = 1;

        if (dir == 1) _transform.eulerAngles = new Vector3(0, 0, 38);
        else _transform.eulerAngles = new Vector3(0, 180, 38);
    }

    private void Update()
    {
        if (GameManager.Instance.isPaused == false)
        {
            StateControl();
            EdgeReached();
        }      
    }

    private void StateControl()
    {
        switch(fedCurrentState)
        {
            case HelicopState.IsMoving: Move(); break;
            case HelicopState.IsWaiting: WaitTime(); break;
            case HelicopState.InBetween: InBetweenState(); break;
        }
    }
    private void Move()
    {
        AudioManager.Instance.Pause("MenuTheme", AudioManager.Instance.music);
        AudioManager.Instance.UpdatePlay("FedTheme", AudioManager.Instance.music);
        AudioManager.Instance.UpdatePlay("Helicoptero", AudioManager.Instance.sounds);
        if(particles.isPlaying == false && isDying == false)
            particles.Play();
        Vector3 pos = transform.position;
        pos.x += dir * speed * Time.deltaTime;
        _transform.position = pos;
    }

    private void WaitTime()
    {
        AudioManager.Instance.Stop("FedTheme", AudioManager.Instance.music);
        AudioManager.Instance.UnPause("MenuTheme", AudioManager.Instance.music);
        AudioManager.Instance.Stop("Helicoptero", AudioManager.Instance.sounds);
        if (particles.isPlaying)
            particles.Stop();
        timer += Time.deltaTime;
        if(timer >= randomTime)
        {
            timer = 0;
            fedCurrentState = HelicopState.InBetween;
        }
    }

    private void InBetweenState()
    {
        Move();
        inBetTimer += Time.deltaTime;
        if(inBetTimer >= 1.5f)
        {
            inBetTimer = 0;
            fedCurrentState = HelicopState.IsMoving;
        }
    }
    private void EdgeReached()
    {
        if ((_transform.position.x > 14 || _transform.position.x < -14) && fedCurrentState == HelicopState.IsMoving)
            reached = true;

        if (reached)
        {
            reached = false;
            randomTime = Random.Range(minWait, maxWait);
            fedCurrentState = HelicopState.IsWaiting;

            dir *= -1;
            if (dir == 1) _transform.eulerAngles = new Vector3(0, 0, 38);
            else _transform.eulerAngles = new Vector3(0, 180, 38);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("pBullet"))
        {
            AudioManager.Instance.UnPause("MenuTheme", AudioManager.Instance.music);
            AudioManager.Instance.Stop("FedTheme", AudioManager.Instance.music);
            AudioManager.Instance.Stop("Helicoptero", AudioManager.Instance.sounds);
            FindObjectOfType<HelicopSpawner>().fedKilled?.Invoke();
            particles.Stop();
            isDying = true;
        }         
    }
}