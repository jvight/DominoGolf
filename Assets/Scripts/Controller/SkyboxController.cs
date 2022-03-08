using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public static SkyboxController instance;
    public List<Material> skyboxList= new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        instance=this;    
        ChangeSkybox();
    }

    // Update is called once per frame
    public void ChangeSkybox(){
       if(StaticData.level>=4&&StaticData.level<9){
           RenderSettings.skybox=skyboxList[0];
       }
       else if(StaticData.level>=9&&StaticData.level<14){
           RenderSettings.skybox=skyboxList[1];
       }
       else if(StaticData.level>=14&&StaticData.level<19){
           RenderSettings.skybox=skyboxList[2];
       }
       else if(StaticData.level>=19&&StaticData.level<24){
           RenderSettings.skybox=skyboxList[3];
       }
       else if(StaticData.level>=24&&StaticData.level<29){
           RenderSettings.skybox=skyboxList[4];
       }
    }
}
