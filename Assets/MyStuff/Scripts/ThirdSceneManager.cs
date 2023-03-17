using UnityEngine;

public class ThirdSceneManager : MonoBehaviour
{
    public void SwitchSkybox(Material SkyboxMaterial)
    {
        RenderSettings.skybox = SkyboxMaterial;
    }

}
