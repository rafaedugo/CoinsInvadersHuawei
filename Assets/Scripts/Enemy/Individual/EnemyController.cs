using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public enum Enemy
{
    etherium,
    tether,
    libra,
    doge,
    fed,
    all
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy currentEnemy;
    [SerializeField] private GameObject scorePop;
    private Animator anim;
    public System.Action killed;

    void Awake() => anim = GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Instance.enemiesOn)
        {
            if (collision.gameObject.tag == "Shield")
                Destroy(collision.gameObject);

            if (collision.gameObject.tag == "pBullet")
            {
                Destroy(collision.gameObject);
                GetComponent<Collider2D>().enabled = false;
                GameManager.Instance.currentEnemy = currentEnemy;
                GameManager.Instance.enemyKilled?.Invoke();
                AudioManager.Instance.Play("Enemy@Explo", AudioManager.Instance.sounds);
                if(currentEnemy != Enemy.fed) GetComponent<EnemyShoot>().enabled = false;
                PlayEnemyExplosionAnimation();
            }
        }    
    }
    void PlayEnemyExplosionAnimation()
    {
        switch (currentEnemy)
        {
            case Enemy.etherium: 
                anim.Play("Etherium@Explosion"); 
                ShowEarnedScore(1); 
                break;
            case Enemy.libra: 
                anim.Play("Libra@Explosion");
                ShowEarnedScore(3); 
                break;
            case Enemy.tether: 
                anim.Play("Tether@Explosion"); 
                ShowEarnedScore(2); 
                break;
            case Enemy.doge: 
                anim.Play("Dog@Explosion"); 
                ShowEarnedScore(4); 
                break;
            case Enemy.fed: 
                anim.Play("Fed@Explosion"); 
                ShowEarnedScore(10); 
                break;
        }
    }

    public void Destroy() => GetComponent<SpriteRenderer>().enabled = false;
    
    private System.Collections.IEnumerator fedDestroy()
    {
        yield return new WaitUntil(() => GetComponent<Helicop>().particles.IsAlive() == false);
        Destroy(gameObject);
    }

    private void ShowEarnedScore(int score) => StartCoroutine(fadeScore(score));
    private System.Collections.IEnumerator fadeScore(int score)
    {
        GameObject obj = Instantiate(scorePop);
        obj.transform.position = transform.position;
        obj.GetComponent<TextMeshPro>().text = "+" + score;
        obj.GetComponent<TextMeshPro>().DOFade(1, .3f);
        yield return new WaitForSeconds(.7f);
        obj.GetComponent<TextMeshPro>().DOFade(0, .3f);
        yield return new WaitForSeconds(.3f);

        if (currentEnemy == Enemy.fed)
            StartCoroutine(fedDestroy());
        else Destroy(gameObject);

        Destroy(obj);

        if (currentEnemy != Enemy.fed)
            Destroy(transform.parent.gameObject);
    }
}