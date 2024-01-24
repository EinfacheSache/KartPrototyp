using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Update()
    {
        StartCoroutine(AutoRestart());
    }

    private IEnumerator AutoRestart()
    {
        yield return null;
        dropdown.Show();
    }
}
