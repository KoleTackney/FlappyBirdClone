using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.Input;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;
    public float gravityForce = 1f;
    public float maxVelocity = 10f;
    public float maxGravity = 6f;
    public float currentVelocity = 0f;
    private bool isDead = false;

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            currentVelocity += upForce;
            currentVelocity = Mathf.Clamp(currentVelocity, 3, maxVelocity);
        }
        else
        {
            currentVelocity -= gravityForce * Time.deltaTime;
            currentVelocity = currentVelocity < -maxGravity ? -maxGravity : currentVelocity;
        }

        transform.position += Vector3.up * currentVelocity * Time.deltaTime;
    }

    void KillBird()
    {
        isDead = true;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        KillBird();
        GameManager.Instance.GameOver();
    }

    void OnCollisionEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
        KillBird();
        GameManager.Instance.GameOver();
    }
}
