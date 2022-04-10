using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    protected RectTransform rectTransform;

    public virtual void ShowView()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideView()
    {
        if(gameObject != null)
        {
            gameObject.SetActive(false);
        }
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
