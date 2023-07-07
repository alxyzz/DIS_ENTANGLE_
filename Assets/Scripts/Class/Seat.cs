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


    public Seat GetLeftNeighbor(Seat b)
    {



        return null;
    }
    public Seat GetRightNeighbor(Seat b)
    {


        return null;
    }

    [HideInInspector] public List<(Student, float, StudentEffectType)> modifiers = new();
    [HideInInspector] public List<(Student, float, StudentEffectType)> modifiedByThis = new();


    void CheckForNullEffects()
    {
        foreach (var item in modifiers)
        {
            if (item.Item1 == null)
            {
                modifiers.Remove(item);
            }
        }

        foreach (var item in modifiedByThis)
        {
            if (item.Item1 == null)
            {
                modifiers.Remove(item);
            }
        }
    }

    //     Possible prerequisits:
    //None = always active
    //if own happiness stat is above or below a certain value
    //if in a certain row
    //if self has neighbours


    void RefreshEffect()
    {
        ChangeEffect(undo: true);
        switch (student.prereq)
        {
            case StudentEffectPrerequisite.NEEDS_NOTHING:
                ChangeEffect();
                break;
            case StudentEffectPrerequisite.NEEDS_HAPPINESS_LEVEL:
                if (student.EFFECTIVE_HAPPINESS >  student.PREREQ_ARGUMENT)
                {
                    ChangeEffect();
                }
                else
                {
                    Debug.Log("Student " + student.chosenName + " tried to apply effect, but did not have the required happiness level.");
                }
                break;
            case StudentEffectPrerequisite.NEEDS_SPECIFIC_ROW:
                if (row != null)
                {
                    if (row.rowID == student.PREREQ_ARGUMENT)
                    {
                        ChangeEffect();
                    }
                    else
                    {
                        Debug.Log("Student " + student.chosenName + " tried to apply effect, but did not have the required row.");
                    }
                }
                break;
            case StudentEffectPrerequisite.NEEDS_NEIGHBORS:
                if (GetLeftNeighbor(this) != null && GetRightNeighbor(this) != null)
                {
                    ChangeEffect();
                }
                else
                {
                    Debug.Log("Student " + student.chosenName + " tried to apply effect, but did not have enough neighbors.");
                }
                break;
            default:
                break;
        }
    }
    void ChangeEffect(bool undo = false)
    {
        if (undo)
        {
            
            return;
        }

        switch (student.effect)
        {
            case StudentEffectType.None:
                Debug.Log("Initialized effect with no effect. :)");
                break;
            case StudentEffectType.GAIN_ALL_OTHER_ROWS:
                int rownumber = row.rowID;
                foreach (var item in ClassroomManager.Instance.LIST_ROWS)
                {
                    if (item.rowID != rownumber)
                    {
                        foreach (var sea in item.seats)
                        {
                            sea.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_ALL_OTHER_ROWS);
                        }
                    }
                }
                break;
            case StudentEffectType.GAIN_OWN_ROW:
                int sameNumber = row.rowID;
                foreach (var item in ClassroomManager.Instance.LIST_ROWS)
                {
                    if (item.rowID == sameNumber)
                    {
                        foreach (var sea in item.seats)
                        {
                            sea.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_OWN_ROW);
                        }
                    }
                }
                break;
            case StudentEffectType.GAIN_SELF:
               AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF);
                break;
            case StudentEffectType.SET_AVERAGE_OF_NEIGHBORS:
                AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.SET_AVERAGE_OF_NEIGHBORS);
                break;
            case StudentEffectType.GAIN_SELF_AND_NEIGHBORS:
                AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF_AND_NEIGHBORS);
                Seat left = GetLeftNeighbor(this);
                Seat right = GetRightNeighbor(this);

                if (left != null)
                {
                    left.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF_AND_NEIGHBORS);
                }
                if (right != null)
                {
                    right.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF_AND_NEIGHBORS);
                }

                break;
            case StudentEffectType.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE:
                Seat lefty = GetLeftNeighbor(this);
                Seat righty = GetRightNeighbor(this);

                if (lefty != null)
                {
                    lefty.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE);
                }
                if (righty != null)
                {
                    righty.AssignModifiers(student, student.EFFECT_ARG_two, StudentEffectType.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE);
                }
                break;
            default:
                Debug.LogError("Got undefined student effect type, for some reason.");
                break;
        }

    }


    //    Effects:
    //all other rows get +X
    //own row gets +X
    //self has +X
    //Sets own happiness to the combined value of neighbours
    //Neighbouring students and self get +X
    //Left neighbour gets +X and right neighbour +X

    public Image ui_studentImage;
    public TextMeshProUGUI ui_learningFactor;
    public float LEARNING_FACTOR
    {
        get
        {

           
            float b = 0;
            if (student != null)
            {
                if (row == null) Debug.LogError("ROW WAS NULL");
                return (b + student.STAT_LEARNING + row.ROW_MODIFIER);
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

    
    public void AssignModifiers(Student source, float mod, StudentEffectType type)
    {



    }


    ///// <summary>
    ///// removes all modifiers sourced from the source. or any at all.
    ///// </summary>
    ///// <param name="source"></param>
    //public void RemoveModifiers(Student source = null)
    //{
    //    if (student.modifiers.Count < 1)
    //    {
    //        return;
    //    }
    //    List<(Student, float, StudentEffectType)> toRemove = new();
    //    foreach (var item in student.modifiers)
    //    {
    //        if (source != null)
    //        {
    //            if (item.Item1 == source)
    //            {
    //                toRemove.Add(item);
    //            }
    //        }
    //        else
    //        {
    //            toRemove.Add(item);
    //        }
            
    //    }

    //    foreach (var item in toRemove)
    //    {
    //        student.modifiers.Remove(item);
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
            return;
        }

        switch (student.prereq)
        {
            case StudentEffectPrerequisite.NEEDS_NOTHING:

                ChangeEffect();

                break;
            case StudentEffectPrerequisite.NEEDS_HAPPINESS_LEVEL:
                ChangeEffect();
                break;
            case StudentEffectPrerequisite.NEEDS_SPECIFIC_ROW:
                ChangeEffect();
                break;
            case StudentEffectPrerequisite.NEEDS_NEIGHBORS:
                ChangeEffect();
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
