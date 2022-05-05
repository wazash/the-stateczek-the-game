using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    protected RectTransform rectTransform;
    public bool isActiveView;

    public virtual void ShowView()
    {
        gameObject.SetActive(true);

        isActiveView = true;
    }

    public virtual void HideView()
    {
        if(gameObject != null)
        {
            gameObject.SetActive(false);
        }

        isActiveView = false;
    }

    public RectTransform GetRect()
    {
        if(rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        return rectTransform;
    }
}
