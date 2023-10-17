using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TweenManager
{
    public static void ClosePopupWithTween(GameObject obj)
    {
        obj.LeanScale(Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }

    public static void OpenPopupWithTween(GameObject obj)
    {
        obj.LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }
}
