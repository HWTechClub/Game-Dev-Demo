using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image weaponIcon;

    [SerializeField] Image[] healthIcons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateWeaponIcon (Sprite icon)
    {
        weaponIcon.gameObject.SetActive(true);
        weaponIcon.sprite = icon;
    }

    public void UpdateHealthUI (int currHealth)
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i >= currHealth)
            {
                healthIcons[i].gameObject.SetActive(false);
            }
        }
    }
   
}
