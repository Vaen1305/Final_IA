using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bonus : MonoBehaviour
{
    public Image BonusBar;
    public int Point;
    public int PointMax;
    private void Start()
    {
        
        UpdabeHEalthBar();
    }
    public void AddBonus(Bonus _Bonus)
    {
        Point += _Bonus.Point;
        Point = Mathf.Clamp(Point,0, PointMax);
        UpdabeHEalthBar();
    }
    public void UpdabeHEalthBar()
    {
        BonusBar.fillAmount =  ((float)Point / (float)PointMax);
    }
    public float Percent()
    {
        return ((float)Point / (float)PointMax);
    }
}
