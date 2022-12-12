using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float atkRate;
    private Shoot shoot;
    private float timer;

    private void Awake() => shoot = GetComponent<Shoot>();
    
    private void Update()
    {
        if (GameManager.Instance.enemiesOn && GameManager.Instance.isPaused == false)
        {
            timer += Time.deltaTime;
            if (timer >= atkRate) { timer = 0; Attack(); }
        }   
    }
    void Attack()
    {
        if (Random.value < (GameManager.currentDifficulty.shootProbabilityMultiplier / GameManager.Instance.amountAlive))
            shoot.ShootBullet();
    }
}
