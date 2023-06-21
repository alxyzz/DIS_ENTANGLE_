using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seat : MonoBehaviour
{
    public Image studentImage;
    bool moving;
    Vector3 Target;
    Vector3 posStart;
    float lerp = 0;
    // Start is called before the first frame update
    void Start()
    {
        studentImage = InitializeChild();
        Button b = GetComponent<Button>();
        b.targetGraphic = studentImage;
        b.onClick.AddListener(delegate () { OnClick(); });
    }

    void Move()
    {
        if (posStart == Vector3.zero)
        {
            posStart = transform.position;
        }
        transform.position = Vector3.Lerp(posStart, Target, lerp);
        lerp += 0.05f;

        if (transform.position == Target)
        {
            lerp = 0;
            moving = false;
            posStart = Vector3.zero;
        }
    }

    public void ReplaceImage(Image b)
    {
        studentImage = b;
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
        if (moving)
        {
            Move();
        }
    }
    public void SetupMovement(Vector3 target)
    {
        Target = target;
        moving = true;

    }

    public void OnClick()
    {
        Debug.Log("Clicked student.");
        ClassroomManager.Instance.OnClickSeat(this);
    }




}
