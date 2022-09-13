using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : Singleton<BGController>
{
    // Bien hinh anh
    public Sprite[] sprites;

    // Tham chieu den SpriteRederer
    public SpriteRenderer bgImage;

    private void Awake() 
    {
        // Khong cho phep luu du lieu khi load sang sence khac
        MakeSingleton(false);
    }

    // ke thua ghi de phuong thuc Singleton
    public override void Start() 
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if(bgImage != null && sprites != null && sprites.Length > 0)
        {
            // Lay ngau nhien
            int randomIdx = Random.Range(0, sprites.Length);

            if(sprites[randomIdx] != null)
            {
                // Lay ngau nhien
                bgImage.sprite = sprites[randomIdx];
            }
        }
    }
}
