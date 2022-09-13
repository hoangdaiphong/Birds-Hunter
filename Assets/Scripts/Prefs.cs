using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luu diem so cao nhat vao trong bo nho
public static class Prefs
{
    public static int bestScore {
        get => PlayerPrefs.GetInt(GameConsts.BEST_SCORE, 0);

        set
        {
            // Diem so da luu duoc trong bo nho
            int curScore = PlayerPrefs.GetInt(GameConsts.BEST_SCORE, 0);

            // Dieu kien de luu diem so cao nhat
            if(value > curScore)
            {
                PlayerPrefs.SetInt(GameConsts.BEST_SCORE, value);
            }
        }
    }
}
