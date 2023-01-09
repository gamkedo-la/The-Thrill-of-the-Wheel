using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;

    [Header ("Display Scripts")]
    [SerializeField] private CarDisplay carDisplay;
    private int currentMapIndex;

    private void Awake()
    {
        ChangeMap(0);
    }

    public void ChangeMap(int _index)
    {
        currentMapIndex += _index;
        if (currentMapIndex < 0) currentMapIndex = scriptableObjects.Length - 1;
        if (currentMapIndex > scriptableObjects.Length - 1) currentMapIndex = 0;
        
        if(carDisplay != null) carDisplay.UpdateCar((Car)scriptableObjects[currentMapIndex]);
    }
}