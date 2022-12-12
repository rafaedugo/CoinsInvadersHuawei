using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public int direction;

    [SerializeField] private string[] destroyableTags;
    [SerializeField] private float heightLimit;

    void Update()
    {
        if (GameManager.Instance.isPaused == true) return;

        float velocity = speed * Time.deltaTime;
        Vector2 pos = transform.position;
        pos.y += direction * velocity;
        transform.position = pos;

        if (pos.y > heightLimit || pos.y < -heightLimit) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //COLLISION WITH ANY DESTROYABLE GAMEOBJECT
        for(int i = 0; i < destroyableTags.Length; i++)
        {
            if (collision.gameObject.tag == destroyableTags[i]) 
            { 
                Destroy(collision.gameObject);
                Destroy(this.gameObject); 
            }
        }   

        //COLLISION WITH PLAYER
        if (collision.gameObject.tag == "Player" && this.gameObject.tag == "eBullet")
        {
            GameManager.Instance.playerKilled?.Invoke();
            Destroy(gameObject);
        }   
    } 
}