using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Update()
    {

        StartCoroutine(AutoRestart());
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
    }

    private IEnumerator AutoRestart()
    {
        yield return null;
        dropdown.Show();
    }

    private IEnumerator AutoSelect()
    {
        yield return new WaitForSeconds(2);
        dropdown.Select();
    }
}
