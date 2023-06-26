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



}
