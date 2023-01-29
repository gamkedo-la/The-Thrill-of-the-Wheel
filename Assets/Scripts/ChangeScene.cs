using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Text carName;
    public void LoadScene()
    {
        PlayerPrefs.SetString("SelectedCar", carName.text);
        SceneManager.LoadScene("SampleScene");
    }
}