using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour
{
    public enum UItype {Avoided, Hit}
    public UItype uiElement; 
    public TMPro.TextMeshProUGUI countText;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = count.ToString();
    }
}
