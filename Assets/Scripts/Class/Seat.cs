using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;





public class Seat : MonoBehaviour
{
    public SeatRow row;
    public SeatRow column;
    public StudentSerializableObject student;
    public Image ui_studentImage;
    public TextMeshProUGUI ui_learningFactor;
    public float LEARNING_FACTOR
    {
        get
        {

           
            float b = 0;
            if (student!= null)
            {
                return (b + student.STAT_LEARNING + row.ROW_MODIFIER + column.ROW_MODIFIER);
            }
            else
            {
                return 0;
            }
            
        }
    }
    bool moving;
    Vector3 Target;
    Vector3 posStart;
    float lerp = 0;
    // Start is called before the first frame update
    void Start()
    {
        ui_studentImage = InitializeChild();
        Button b = GetComponent<Button>();
        b.targetGraphic = ui_studentImage;
        b.onClick.AddListener(delegate () { OnClick(); });
    }

    //void Move()
    //{
    //    if (posStart == Vector3.zero)
    //    {
    //        posStart = transform.position;
    //    }
    //    transform.position = Vector3.Lerp(posStart, Target, lerp);
    //    lerp += 0.05f;

    //    if (transform.position == Target)
    //    {
    //        lerp = 0;
    //        moving = false;
    //        posStart = Vector3.zero;
    //    }
    //}

    

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
    public void SetupMovement(Vector3 target)
    {
        Target = target;
        moving = true;

    }

    public void PostMove()
    {
        if (student != null)
        {
            ui_studentImage.enabled = true;
            ui_learningFactor.text = student.LANE_MODIFIER.ToString();
            ui_studentImage.sprite = student.seatedImage;
        }
        else
        {
            ui_studentImage.enabled = false;
            ui_learningFactor.text = "";
            ui_studentImage.sprite = null ;
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked student.");
        ClassroomManager.Instance.OnClickSeat(this);
    }




}
