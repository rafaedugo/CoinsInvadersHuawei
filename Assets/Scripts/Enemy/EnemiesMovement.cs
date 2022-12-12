using UnityEngine;
using System;
using DG.Tweening;

public class EnemiesMovement : MonoBehaviour
{
    private AnimationCurve speed;
    private float speedMultiplier = 1;
    private Vector3 dir = Vector2.right;
    private float increaseSpeedMultiplier;
    [SerializeField] private float moveDownValue;

    private void Start()
    {
        speed = GameManager.currentDifficulty.enemiesSpeed;
        increaseSpeedMultiplier = GameManager.currentDifficulty.enemiesSpeedMultiplier;
    }

    private void Update()
    {
        if (GameManager.Instance.enemiesOn && transform.position.y >= -6 && GameManager.Instance.isPaused == false) 
            Movement();
        else return;
    }
    void Movement()
    {
        Helicop helicop = FindObjectOfType<Helicop>();
        if (helicop == null || helicop.fedCurrentState == HelicopState.IsWaiting)
            transform.position += dir * speed.Evaluate(GameManager.Instance.percentKilled) * Time.deltaTime * speedMultiplier;
        else if (helicop.fedCurrentState == HelicopState.IsMoving || helicop.fedCurrentState == HelicopState.InBetween)
            transform.position += dir * 3.2f * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform coinInvader in transform)
        {
            if (transform.childCount <= 0) { continue; }

            if (dir == Vector3.right && coinInvader.position.x >= (rightEdge.x - 1)) GoARowDown();
            else if (dir == Vector3.left && coinInvader.position.x <= (leftEdge.x + 1)) GoARowDown();
        }
    }
    void GoARowDown()
    {
        Helicop helicop = FindObjectOfType<Helicop>();

        dir.x *= -1;

        if (helicop != null && helicop.fedCurrentState == HelicopState.IsMoving || helicop.fedCurrentState == HelicopState.InBetween)
        {
            FindObjectOfType<Flashes>().Flash();
            Camera.main.transform.DOShakePosition(.3f, 1, 10, 90, false, true);
            Vector3 pos = transform.position;
            pos.y -= moveDownValue;
            transform.position = pos;
        }        

        if (helicop == null || helicop.fedCurrentState == HelicopState.IsWaiting)
        {
            Vector3 pos = transform.position;
            pos.y -= moveDownValue;
            transform.position = pos;

            speedMultiplier *= increaseSpeedMultiplier;
        }   
    }
}