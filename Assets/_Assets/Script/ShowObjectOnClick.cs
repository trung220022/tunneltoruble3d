using UnityEngine;

public class ShowObjectOnClick : MonoBehaviour
{
    public GameObject objectToShow;

    public void ShowObject()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
