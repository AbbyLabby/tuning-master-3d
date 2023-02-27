using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton { get; private set; }

    [SerializeField] private StagesData stageData;
    private int m_CurrentIndex = 0;

    [SerializeField] private MenuController menuController;
    
    [SerializeField] private Texture rustTexture;
    [SerializeField] private Texture brokenGlass;
    [SerializeField] private Texture normalBrokenGlass;
    [SerializeField] private Texture metallicBrokenGlass;

    [SerializeField] private List<Material> lightMaterials;

    private void Awake()
    {
        Singleton = this;
    }

    private void CheckPluginStage()
    {
        var result = false;
        
        foreach (var stage in stageData.stageList)
        {
            if (stage == Stages.EnumStages.PowerPlugin)
                result = true;
        }

        if (!result)
            return ;
        
        DisableCarLights();
        DisableCarExhaust();
    }

    private void DisableCarLights()
    {
        foreach (var mat in lightMaterials)
        {
           mat.DisableKeyword("_EMISSION");
        }
    }

    private void DisableCarExhaust()
    {
        CarManager.Singleton.GetCar().exhaustParticle.SetActive(false);
    }
    
    private void CheckPolishingStage()
    {
        var result = false;
        
        foreach (var stage in stageData.stageList)
        {
            if (stage == Stages.EnumStages.Polishing)
                result = true;
        }

        if (!result)
            return ;
        
        UpdateCarTexture();
    }

    private void CheckGlassStage()
    {
        var result = false;
        
        foreach (var stage in stageData.stageList)
        {
            if (stage == Stages.EnumStages.BreakingTheWindows)
                result = true;
        }

        if (!result)
            return ;
        
        UpdateGlassTexture();
    }

    private void UpdateGlassTexture()
    {
        var glasses = CarManager.Singleton.GetCar().glasses;

        foreach (var glass in glasses)
        {
            var color = glass.material.color;
            
            glass.material.mainTexture = brokenGlass;
            glass.material.SetTexture("_BumpMap", normalBrokenGlass);
            glass.material.SetTexture("_MetallicGlossMap", metallicBrokenGlass);

            glass.material.color = new Color(color.r, color.g, color.b, 1);
        }
    }

    private void CheckInflatingStage()
    {
        var result = false;

        foreach (var stage in stageData.stageList)
        {
            if (stage == Stages.EnumStages.InflatingTheTires)
                result = true;
        }
        
        if(!result)
            return;
        
        UpdateCarTires();
    }

    private void Start()
    {
        CheckPolishingStage();
        CheckInflatingStage();
        CheckGlassStage();
        CheckPluginStage();
        
        menuController.ApplyStage(stageData.stageList[m_CurrentIndex]);
        m_CurrentIndex++;
    }

    public void NextStage()
    {
        menuController.ApplyStage(stageData.stageList[m_CurrentIndex]);
        m_CurrentIndex++;
    }
    
    private void UpdateCarTexture()
    {
        CarManager.Singleton.GetCar().bodyRenderer.material.mainTexture = rustTexture;
        CarManager.Singleton.GetCar().bodyRenderer.sharedMaterial.SetFloat("_GlossMapScale", 0);
        CarManager.Singleton.GetCar().changeCounterRust.GetComponent<P3dChangeCounter>().Texture = rustTexture;
    }

    private void UpdateCarTires()
    {
        var tires = CarManager.Singleton.GetCar().tires;
        
        foreach (var tire in tires)
        {
            tire.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, 100);
            tire.GetComponent<SphereCollider>().radius = .26f;
        }
    }
}
