using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public GameObject shopPanel;
   


    private int nextWavePower = 0;
    private int[] wavePowerValues = { 5, 6, 7, 8 };
    private int[] wavePowerCoast = { 5, 10, 30, 50};

    public void buyWavePower()
    {
        int actualPoints = PlayerPrefs.GetInt("points");
        if (actualPoints >= wavePowerCoast[nextWavePower] && nextWavePower <=3)
        {
            actualPoints = actualPoints - wavePowerCoast[nextWavePower];
            PlayerPrefs.SetInt("points", actualPoints);
            PlayerPrefs.SetInt("Power_waveForce", wavePowerValues[nextWavePower]);
            nextWavePower++;
        }
    }

    private int nextHumanSpaw = 0;
    private int[] HumanSpawValues = { 8, 6, 3, 1 };
    private int[] HumanSpawCoast = { 5, 10, 30, 50 };

    public void buyHumanSpaw()
    {
        int actualPoints = PlayerPrefs.GetInt("points");
        if (actualPoints >= HumanSpawCoast[nextHumanSpaw] && nextHumanSpaw <= 3)
        {
            actualPoints = actualPoints - HumanSpawCoast[nextHumanSpaw];
            PlayerPrefs.SetInt("points", actualPoints);
            PlayerPrefs.SetInt("Power_spawnTime", HumanSpawValues[nextHumanSpaw]);
            nextHumanSpaw++;
        }
    }

    private int nextatrrPoints = 0;
    private int[] atrrPointsValues = { 1, 2, 3};
    private int[] atrrPointsCoast = { 5, 10, 30};

    public void buyatrrPoints()
    {
        int actualPoints = PlayerPrefs.GetInt("points");
        if (actualPoints >= atrrPointsCoast[nextatrrPoints] && nextatrrPoints <=2)
        {
            actualPoints = actualPoints - atrrPointsCoast[nextatrrPoints];
            PlayerPrefs.SetInt("points", actualPoints);
            PlayerPrefs.SetInt("Power_attractions", atrrPointsValues[nextatrrPoints]);
            nextatrrPoints++;
        }
    }

    private int nexthumanMult = 0;
    private int[] humanMultValues = { 1, 2, 3 };
    private int[] humanMultCoast = { 5, 10, 30 };

    public void buyhumanMult()
    {
        int actualPoints = PlayerPrefs.GetInt("points");
        if (actualPoints >= humanMultCoast[nexthumanMult] && nexthumanMult <= 2)
        {
            actualPoints = actualPoints - humanMultCoast[nexthumanMult];
            PlayerPrefs.SetInt("points", actualPoints);
            PlayerPrefs.SetInt("Power_humanMult", humanMultValues[nexthumanMult]);
            nexthumanMult++;
        }
    }   

    public void OpenShop()
    {
        if (shopPanel.active)
        {
            Time.timeScale = 1;

        }
        else {
            Time.timeScale = 0;
        
        }
        shopPanel.SetActive(!shopPanel.active);
    }

   

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public Text wave;
    public Text atrr;
    public Text mult;
    public Text spaw;


    public void Update() {
    
        wave.text = "Actual: " + PlayerPrefs.GetInt("Power_waveForce") + " Coast: " + wavePowerCoast[nextWavePower];
        atrr.text = "Actual: " + PlayerPrefs.GetInt("Power_attractions") + " Coast: " + atrrPointsCoast[nextatrrPoints];
        mult.text = "Actual: " + PlayerPrefs.GetInt("Power_humanMult") + " Coast: " + humanMultCoast[nexthumanMult];
        spaw.text = "Actual: " + PlayerPrefs.GetInt("Power_spawnTime") + " Coast: " + HumanSpawCoast[nextHumanSpaw];
    }
}