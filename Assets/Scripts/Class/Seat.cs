using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seat : MonoBehaviour
{
    public Image studentImage;
    // Start is called before the first frame update
    void Start()
    {
        studentImage = InitializeChild();
    }
    Image InitializeChild()
    {
        foreach (Transform child in transform)
        {
            // Try to get the component from the current child GameObject
            Image b = child.GetComponent<Image>();

            // If the component is found, break the loop
            if (b != null)
                return b;
           
        }
        Debug.Log("could not find any Images in the children of object " + gameObject);
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        ClassroomManager.Instance.OnClickSeat(this);
    }




}
