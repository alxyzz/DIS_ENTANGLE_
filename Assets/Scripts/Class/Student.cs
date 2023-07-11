using UnityEngine;

public class Student
{
    public string chosenName = "";
    public SeatRow row;



    #region neighbor modifiers



    public Student(string cn,
                   Sprite imagedown,
                   Sprite protra,
                   float statlearn,
                   string des,
                   float rowmod,
                   StudentEffectPrerequisite prerequi,
                   float prereqarg,
                   StudentEffectType effc,
                   float effecarg1,
                   float effecarg2)
    {
        chosenName = cn;
        seatedImage = imagedown;
        portrait = protra;
        STAT_LEARNING = statlearn;
        DESC = des;
        ROW_MODIFIER = rowmod;
        prereq = prerequi;
        PREREQ_ARGUMENT = prereqarg;
        effect = effc;
        EFFECT_ARG_ONE = effecarg1;
        EFFECT_ARG_two = effecarg2;
    }

    #endregion
    [Header("Seated image")]
    public Sprite seatedImage;

    [Header("Portrait image")]
    public Sprite portrait;

    [Header("Base happiness state")]
    public float STAT_LEARNING = 0;

    [Header("Student description")]
    public string DESC = "";

    //[Header("Modifier for the entire lane")]
    //public float LANE_MODIFIER;

    [Header("Modifier for the entire row")]
    public float ROW_MODIFIER = 0;

    public StudentEffectPrerequisite prereq = StudentEffectPrerequisite.NEEDS_NOTHING;

    [Header("Prerequisite argument")]
    public float PREREQ_ARGUMENT = 0;

    [Header("Effect of prerequisite being met.")]
    public StudentEffectType effect = StudentEffectType.None;


    [Header("Effect argument 1/left")]
    public float EFFECT_ARG_ONE;

    [Header("Effect argument 2/right")]
    public float EFFECT_ARG_two;
}
