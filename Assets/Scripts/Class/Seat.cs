using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;





public class Seat : MonoBehaviour
{

    #region happiness calculations
  


    #endregion
    [HideInInspector]public SeatRow row;
    [HideInInspector] public SeatRow column;
    [HideInInspector] public Student student;
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
        Debug.Log("TEST!");
        ui_studentImage = InitializeChild();
        Button b = GetComponent<Button>();
        if (ui_studentImage == null)
        {
            Debug.LogError("ui_studentImage is null at gameobject " + gameObject.name);
        }
        if (ui_learningFactor == null)
        {
            Debug.LogError("ui_learningFactor is null at gameobject " + gameObject.name);
        }
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

    public void Refresh()
    {
        if (student != null)
        {
            ui_studentImage.enabled = true;
            ui_learningFactor.text = (student.STAT_LEARNING + row.ROW_MODIFIER).ToString();
            ui_studentImage.sprite = student.seatedImage;
        }
        else
        {
            ui_studentImage.enabled = false;
            ui_learningFactor.text = "";
            ui_studentImage.sprite = null ;
        }

        switch (student.prereq)
        {
            case StudentPrerequisite.NEEDS_NOTHING:

                DoEffect();

                break;
            case StudentPrerequisite.NEEDS_HAPPINESS_LEVEL:
                DoEffect();
                break;
            case StudentPrerequisite.NEEDS_SPECIFIC_ROW:
                DoEffect();
                break;
            case StudentPrerequisite.NEEDS_NEIGHBORS:
                DoEffect();
                break;
            default:
                break;
        }
    }


    void DoEffect()
    {
        switch (student.effect)
        {
            case StudentEffects.None:
                break;
            case StudentEffects.GAIN_ALL_OTHER_ROWS:
                break;
            case StudentEffects.GAIN_OWN_ROW:
                break;
            case StudentEffects.GAIN_SELF:
                break;
            case StudentEffects.SET_AVERAGE_OF_NEIGHBORS:
                break;
            case StudentEffects.GAIN_SELF_AND_NEIGHBORS:
                break;
            case StudentEffects.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE:
                break;
            default:
                break;
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked student.");
        ClassroomManager.Instance.OnClickSeat(this);
    }




}
