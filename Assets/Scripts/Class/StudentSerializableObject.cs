using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New StudentSerializableObject", menuName = "StudentSerializableObject")]
public class StudentSerializableObject : ScriptableObject
{

    public string chosenName = "";
    
    [Header("Seated image")]
    public Sprite seatedImage;

    [Header("Portrait image")]
    public Sprite portrait;

    [Header("Names to be picked at random")]
    public List<string> Names = new();

    [Header("Base learning state")]
    public float STAT_LEARNING = 0;

    [Header("Student description")]
    public string DESC = "";

    //[Header("Modifier for the entire lane")]
    //public float LANE_MODIFIER;

    [Header("Modifier for the entire row")]
    public float ROW_MODIFIER = 0;

    [Header("Prerequisite to meet.")]
    public StudentPrerequisite prereq;

    [Header("Number related to prereq. Be it happiness level, or row, or howm any neighbors,.")]
    public int prereq_argument;

    [Header("Effect of prerequisite being met.")]
    public StudentEffects effect;

    [Header("Number related to effect. affects the gain or malus or whatever")]
    public int effect_argument;

}



public enum StudentPrerequisite
{
    NEEDS_NOTHING,
    NEEDS_HAPPINESS_LEVEL,
    NEEDS_SPECIFIC_ROW,
    NEEDS_NEIGHBORS
}

public enum StudentEffects
{
    None,
    GAIN_ALL_OTHER_ROWS,
    GAIN_OWN_ROW,
    GAIN_SELF,
    SET_AVERAGE_OF_NEIGHBORS,
    GAIN_SELF_AND_NEIGHBORS,
    GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE

}


//     Possible prerequisits:
//None = always active
//if own happiness stat is above or below a certain value
//if in a certain row
//if self has neighbours



//    Effects:
//all other rows get +X
//own row gets +X
//self has +X
//Sets own happiness to the combined value of neighbours
//Neighbouring students and self get +X
//Left neighbour gets +X and right neighbour +X