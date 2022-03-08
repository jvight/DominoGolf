using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public static SkyboxController instance;
    public Material skybox1;
    public Material skybox2;
    public Material skybox3;

    // Start is called before the first frame update
    void Start()
    {
        instance=this;    
        ChangeSkybox();
    }

    // Update is called once per frame
    public void ChangeSkybox(){
       if(StaticData.level>=4&&StaticData.level<9){
           RenderSettings.skybox=skybox1;
       }
       else if(StaticData.level>=9&&StaticData.level<14){
           RenderSettings.skybox=skybox2;
       }
       else if(StaticData.level>=14&&StaticData.level<19){
           RenderSettings.skybox=skybox3;
       }
    }
}
