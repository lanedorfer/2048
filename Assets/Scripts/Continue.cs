using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public GameObject panel;

    public void continueGame()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
