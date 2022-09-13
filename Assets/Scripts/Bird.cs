using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float xSpeed;
    public float minYspeed;
    public float maxYspeed;

    // Tham chieu den hieu ung mau ban ra
    public GameObject deathVfx;

    Rigidbody2D m_rb;

    // Bien kiem tra xem chim co di chuyen ve phia tay trai khong
    bool m_moveLeftOnStart;

    // Bien kiem tra chim chet hay chua
    bool m_isDead;

    private void Awake() 
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        // Khi game bat dau chay thi random
        RandomMovingDirection();
    }

    private void Update()
    {
        // Thay doi van toc cua con chim va di chuyen ve ben tay phai(xSpeed la duong)
        m_rb.velocity = m_moveLeftOnStart ? 
            new Vector2(-xSpeed, Random.Range(minYspeed, maxYspeed))
            : new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed));
        // Goi gam Flip() de doi hinh anh con chim
        Flip();
    }

    // Lay ngau nhien huong di chuyen cua con chim
    public void RandomMovingDirection()
    {
        // Di chuyen nguoc huong theo vi tri cua con chim
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;
    }

    // Thay doi scale de thay doi hinh anh con chim
    void Flip()
    {
        // Neu con chim di chuyen ben tay trai cho gia tri am
        if (m_moveLeftOnStart)
        {
            if(transform.localScale.x < 0) return;

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }else
        {
            if(transform.localScale.x > 0) return;

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    // Khi con chim chet
    public void Die()
    {
        m_isDead = true;

        GameManager.Ins.BirdKilled++;

        Destroy(gameObject);

        // Khi con chim chet thi tao ra mau ban ra o tren man hinh game
        if(deathVfx)
            Instantiate(deathVfx, transform.position, Quaternion.identity);

        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);   
    }
}
