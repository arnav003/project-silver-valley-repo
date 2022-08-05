using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stat : MonoBehaviour
{

    public int Health;
    public int MaxHealth;
    public Slider HealtBar;
    public TMP_Text HealthText;

    void Start()
    {

    }


    void Update()
    {
        HealthBar();
    }

    private void HealthBar()
    {
        HealtBar.value = Health;
        HealthText.text = Health.ToString();
        HealtBar.maxValue = MaxHealth;
    }
}
