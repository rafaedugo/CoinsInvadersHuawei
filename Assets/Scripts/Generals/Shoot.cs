using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform origin;
    [Range(-1, 1)] public int direction;
    [Range(0, 20)] public float speed;

    public void ShootBullet()
    {
        GameObject o = Instantiate(bullet);
        if (gameObject.name == "Player")
        {
            bullet.GetComponent<Bullet>().speed = speed;
            AudioManager.Instance.Play("PlayerLaser", AudioManager.Instance.sounds);
        }
        else
        {
            AudioManager.Instance.Play("EnemyLaser", AudioManager.Instance.sounds); 
            switch(GameManager.currentDifficulty.name)
            {
                case "Easy": bullet.GetComponent<Bullet>().speed = speed - 4; break;
                case "Normal": bullet.GetComponent<Bullet>().speed = speed - 2; break;
                case "Hard": bullet.GetComponent<Bullet>().speed = speed; break;
            }
        }
        if (origin == null) return;
        o.transform.position = origin.position;
        bullet.GetComponent<Bullet>().direction = direction;
        //bullet.GetComponent<Bullet>().speed = speed;
    }
}
