using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{

    public GameObject healthContainer;
    private float fillValue;

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)BossHeartController.health;
        fillValue = fillValue / BossHeartController.maxHealth;
        healthContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
