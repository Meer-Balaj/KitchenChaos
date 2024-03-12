using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    //interfaces down side is that they do not show up in the inspector because unity does not know if it will be implemented or not
    //we can not drag and drop items in inspector if it is an interface
    private IHasProgress hasProgress;

    private void Start()
    {
        //this way using a gameobject we can see it in the inspector
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have the component that implements IHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
