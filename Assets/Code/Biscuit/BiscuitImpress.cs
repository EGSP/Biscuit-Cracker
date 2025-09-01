using Assets.Code;
using DG.Tweening;
using UnityEngine;

public class BiscuitImpress : MonoBehaviour
{
    public Transform Sprite;
    public Vector2 SpriteScaleRange = new Vector2(0.5f, 1.5f);
    
    
    public float Duration = 0.3f;

    public Ease Ease = Ease.OutSine;


    private float _originalScale;
    private Tween _tween;

    private void Awake()
    {
        if(Sprite == null)
        {
            Sprite = GetComponentInChildren<Transform>();
            Debug.LogWarning("Sprite transform is not assigned in BiscuitImpress.");
        }
        _originalScale = Sprite.localScale.x;

    }

    public void Impress(Biscuit biscuit)
    {
        // Do nothing if already fully clicked
        if (biscuit.ClickedPoints >= biscuit.ClickPoints)
            return;

        // Reset scale
        Sprite.localScale = Vector3.one * _originalScale;

        // Add scale tweening
        _tween?.Kill();
        _tween = Sprite.DOScale(_originalScale + Random.Range(SpriteScaleRange.x, SpriteScaleRange.y), Duration)
            .SetEase(Ease)
            .SetLoops(2, LoopType.Yoyo);
    }
}
