using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Tao do tre khi ban
    public float fireRate;
    
    // Luu fireRate hien tai do fireRate thay doi
    float m_curFireRate;

    // Tao ra ong ngam
    public GameObject viewFinder;

    // Kiem tra da ban hay chua
    bool m_isShooted;

    GameObject m_viewFinderClone;
    private void Awake()
    {
        m_curFireRate = fireRate;
    }

    private void Start()
    {  
        if(viewFinder)
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
    }
    private void Update() 
    {
        // Chuyen toa do cua nguoi choi sang toa do cua Unity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Kiem tra da nhan chuot trai hay chua, neu roi thi ban
        if(Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            Shot(mousePos);
        }

        // Neu da ban thi giam FireRate tu tu
        if (m_isShooted)
        {
            m_curFireRate -= Time.deltaTime;

            if(m_curFireRate <= 0)
            {
                // Luc do sung chua duoc ban thi nguoi choi co the ban duoc sung
                m_isShooted = false;

                m_curFireRate = fireRate;
            }

            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate);
        }

        // Chuot di chuyen cho nao thi ngam cho do
        if(m_viewFinderClone)
        {
            // Gan vi tri con tro chuot vao vi tri ong ngam
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
    }
    void Shot(Vector3 mousePos)
    {
        // Neu nguoi dung click ban sung
        m_isShooted = true;

        // Lay huong tu camera den con tro chuot
        Vector3 shootDir = Camera.main.transform.position - mousePos;

        // Gian luoc de may tinh de tinh toan
        shootDir.Normalize();

        // Cham vao cho nao thi biet duoc thong tin cua vat do
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if(hits != null && hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Kiem tra khoang cach giua doi tuong duoc phat hien boi RayCast den vi tri con tro chuot phai nho hon 0.4
                if(hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    // Khi ban trung con chim, chinh la duong RayCast tro den con chim
                    Bird bird = hit.collider.GetComponent<Bird>();

                    if(bird)
                    {
                        bird.Die();
                    }
                }
            }
        }

        // Tao tieng dong khi ban chim
        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }

}
