using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;





public class Seat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region happiness calculations



    #endregion
    [HideInInspector] public SeatRow row;
    [HideInInspector] public SeatRow column;
    [HideInInspector] public Student student;


    [SerializeReference] public Seat left;
    [SerializeReference] public Seat right;

    public float EFFECTIVE_HAPPINESS //happiness post all modifiers
    {
        get
        {
            if (student == null)
            {
                return 0;
            }
            float modi = 0;
            foreach (var item in modifiers)
            {
                modi += item.Item2;
            }
            float b = student.STAT_LEARNING + row.ROW_MODIFIER + modi;
            
            return b;
        }
    }


    [HideInInspector] public List<(Student, float, StudentEffectType)> modifiers = new();


   

    //     Possible prerequisits:
    //None = always active
    //if own happiness stat is above or below a certain value
    //if in a certain row
    //if self has neighbours


    public void CheckPrerequisiteAndDoEffect()
    {
        if (student == null)
        {
            return;
        }
       
        switch (student.prereq)
        {
            case StudentEffectPrerequisite.NEEDS_NOTHING:
                DoEffect();
                break;
            case StudentEffectPrerequisite.NEEDS_HAPPINESS_LEVEL:
                if (EFFECTIVE_HAPPINESS > student.PREREQ_ARGUMENT)
                {
                    DoEffect();
                }
                else
                {
                    Debug.Log(" " + student.chosenName + " tried to apply effect, but did not have the required happiness level.");
                }
                break;
            case StudentEffectPrerequisite.NEEDS_SPECIFIC_ROW:
                if (row != null)
                {
                    if (row.rowID == student.PREREQ_ARGUMENT)
                    {
                        DoEffect();
                    }
                    else
                    {
                        Debug.Log(" " + student.chosenName + " tried to apply effect, but did not have the required row.");
                    }
                }
                break;
            case StudentEffectPrerequisite.NEEDS_NEIGHBORS:
                bool doit = false;
                if (left != null)
                {
                    if (left.student != null)
                    {
                        doit = true;
                    }
                }
                if (right != null)
                {
                    if (right.student != null)
                    {
                        doit = true;
                    }
                }
                if (doit)
                {
                    DoEffect();
                }

                
                break;
            default:
                break;
        }
    }
    void DoEffect()
    {


        switch (student.effect)
        {
            case StudentEffectType.None:
                break;
            case StudentEffectType.GAIN_ALL_OTHER_ROWS:
                // ALEX MARK OF APPROVAL ON THIS FINE PIECE OF WORK
                int rownumber = row.rowID;
                foreach (var item in ClassroomManager.Instance.LIST_SEATS)
                {
                    if (item != this && item.row.rowID != rownumber)
                    {
                        item.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_ALL_OTHER_ROWS);

                    }
                }
                break;
            case StudentEffectType.GAIN_OWN_ROW:
                // ALEX MARK OF APPROVAL ON THIS FINE PIECE OF WORK
                foreach (var item in ClassroomManager.Instance.LIST_SEATS)
                {
                    if (item.row.rowID == row.rowID || item.row.rowID == row.rowID+1)
                    {
                        item.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_OWN_ROW);
                    }
                }

                student.ROW_MODIFIER = student.EFFECT_ARG_ONE;

                break;
            case StudentEffectType.GAIN_SELF:

                //just gain to self
                AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF);
                break;
            case StudentEffectType.SET_AVERAGE_OF_NEIGHBORS:
                //sum up neighbors then gain it for yourself
                // ALEX MARK OF APPROVAL ON THIS FINE PIECE OF WORK - could use a list but im not gonna. its the last day cut me some slack bro-man
                float AoN = 0;
                if (left != null)
                {
                    AoN += left.EFFECTIVE_HAPPINESS;
                }

                if (right != null)
                {
                    AoN += right.EFFECTIVE_HAPPINESS;
                }

                if (right != null && left != null)
                {
                    AoN /= 2;
                }
                AssignModifiers(student, AoN, StudentEffectType.SET_AVERAGE_OF_NEIGHBORS);
                break;
            case StudentEffectType.GAIN_SELF_AND_NEIGHBORS:
                //gain the bonus both to self and people nearby
                // ALEX MARK OF APPROVAL ON THIS FINE PIECE OF WORK
                if (left != null)
                {
                    if (left.student != null)
                    {
                        left.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF_AND_NEIGHBORS);

                    }

                }
                if (right != null)
                {
                    if (right.student != null)
                    {
                        right.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF_AND_NEIGHBORS);

                    }

                }
                AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_SELF_AND_NEIGHBORS);

                break;
            case StudentEffectType.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE:
                //give a different bonus to neighbors

                // ALEX MARK OF APPROVAL ON THIS FINE PIECE OF WORK

                if (left != null)
                {
                    left.AssignModifiers(student, student.EFFECT_ARG_ONE, StudentEffectType.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE);
                }
                if (right != null)
                {
                    right.AssignModifiers(student, student.EFFECT_ARG_two, StudentEffectType.GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE);
                }
                break;
            default:
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
                if (row == null)
                {
                    Debug.LogError("ROW WAS NULL");
                }

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
        ui_studentImage = InitializeChild();
        //ui_studentImage = GetComponent<Image>();
        Button b = GetComponent<Button>();

        b.targetGraphic = ui_studentImage;
        b.onClick.AddListener(delegate () { OnClick(); });
    }



    public void AssignModifiers(Student source, float mod, StudentEffectType type)
    {
        if (student != null)
        {
            //Debug.LogWarning("Added modifier " + type.ToString() + " from " + source.chosenName + " with value" + mod.ToString());

            modifiers.Add((source, mod, type));

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (student != null)
        {
            
           
           
            ClassroomManager.Instance.OnHoverSeatEnter(student);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (student != null)
        {
            ClassroomManager.Instance.OnHoverSeatExit();
        }


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
            {
                return b;
            }
        }
        Debug.Log("could not find any Images in the children of object " + gameObject);
        return null;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void RefreshGraphics()
    {

        if (student != null)
        {
            ui_studentImage.enabled = true;
            float totalModifier = 0;
            string mod = "";
            foreach (var item in modifiers)
            {
                totalModifier += item.Item2;
            }
            if (totalModifier != 0)
            {
                if (totalModifier < 0)
                {
                    mod =  totalModifier.ToString();

                }
                else
                {
                    mod = "+" + totalModifier.ToString();

                }
            }
            ui_learningFactor.text = (student.STAT_LEARNING + row.ROW_MODIFIER).ToString() + mod;
            ui_studentImage.sprite = student.seatedImage;
        }
        else
        {
            ui_studentImage.enabled = false;
            ui_learningFactor.text = "";
            ui_studentImage.sprite = null;
            return;
        }


      

      
        
    }




    public void OnClick()
    {


        ClassroomManager.Instance.OnClickSeat(this);
    }




}
