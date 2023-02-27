using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stage/StageConfig", fileName = "StageConfig")]
public class StagesData : ScriptableObject
{
    public List<Stages.EnumStages> stageList;
}
