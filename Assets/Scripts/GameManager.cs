using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Bien de luu lai cac con chim o trong prefabs
    public Bird[] birdPrefabs;

    // Khoang thoi gian de tao ra cac con chim
    public float spawnTime;

    // Gioi han thoi gian cho  nguoi choi
    public int timeLimit;
    int m_curTimeLimit;

    // Nhung con chim da bi ban ha
    int m_birdKilled;

    // Tro choi da ket thuc hay chua
    bool m_isGameover;

    // Cac ham get va set
    public bool IsGameover { get => m_isGameover; set => m_isGameover = value; }
    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }

    // Khong luu cac gia tri khi load sang sence moi
    public override void Awake()
    {
        MakeSingleton(false);

        m_curTimeLimit = timeLimit;

        // Xoa diem so luu trong bo nho
        // PlayerPrefs.DeleteAll();
    }

    public override void Start() 
    {
        GameGUIManager.Ins.ShowGameGui(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKilled);
    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());

        StartCoroutine(TimeCountDown());

        GameGUIManager.Ins.ShowGameGui(true);
    }

    IEnumerator TimeCountDown()
    {
        // Giam gia tri cua timeLimit
        while(m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;

            if(m_curTimeLimit <= 0)
            {
                m_isGameover = true;

                // So con chim nguoi choi ban ha duoc lon hon so con chim ban ha cu
                if(m_birdKilled > Prefs.bestScore)
                {
                    // Cap nhat hop thoai moi
                     GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED : x" + m_birdKilled);
                } 
                else if(m_birdKilled < Prefs.bestScore) 
                {
                    // Neu khong thi van giu nguyen
                     GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED : x" + Prefs.bestScore);
                }

                // Luu lai diem so cao nhat
                Prefs.bestScore = m_birdKilled;

                // Neu thoi gian ket thuc
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;
            }
            

            // Cap nhat thoi gian
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));
        }
    }

    // Cho trong mot khoang thoi gian nhat dinh va thuc hien
    IEnumerator GameSpawn()
    {
        // Lap lai tao ra nhung con chim o vi tri ngau nhien trong khoang thoi gian nhat dinh
        while(!m_isGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);     // Doi trong khoang thoi gian
        }
    }

   void SpawnBird()
   {
       Vector3 spawnPos = Vector3.zero;

        // Lay ngau nhien gia tri
       float randCheck = Random.Range(0f, 1f);

        // Tao ngau nhien gia tri de chim bay tu trai hoac phai o ngoai man hinh
       if(randCheck >= 0.5f)
       {
           spawnPos = new Vector3(12, Random.Range(1.5f, 4f), 0);
       }else
       {
           spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);
       }

        // Tao ra cac con chim
        if(birdPrefabs != null && birdPrefabs.Length > 0)
        {
            int ranIdx = Random.Range(0, birdPrefabs.Length);

            if(birdPrefabs[ranIdx] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[ranIdx], spawnPos, Quaternion.identity);
            }
        }
   }

    // Chuyen so chuoi sang thoi gian
    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minutes.ToString("00") + " : " + seconds.ToString("00");
    }
}
