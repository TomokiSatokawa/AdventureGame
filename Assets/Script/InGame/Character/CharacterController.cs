using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Image _mainImage;
    [SerializeField] private Image _subImage;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _fadeDelay;

    public void ShowCharacter(Sprite character, string changeType)
    {
        switch (changeType[0])
        {
            case '0':
                _subImage.sprite = null;
                _mainImage.sprite = character;
                break;

            case '1':
                SetAlpha(_mainImage, 0);
                SetAlpha(_subImage, 1);

                _subImage.sprite = _mainImage.sprite;
                _mainImage.sprite = character;

                _subImage.DOFade(0f, _fadeDuration);
                _mainImage.DOFade(1f, _fadeDuration).SetDelay(_fadeDelay);
                break;

            case '2':
                SetAlpha(_mainImage, 0);
                SetAlpha(_subImage, 1);

                _subImage.sprite = _mainImage.sprite;
                _mainImage.sprite = character;

                _mainImage.DOFade(1f, _fadeDuration);
                DOVirtual.DelayedCall(_fadeDuration,() => SetAlpha(_subImage, 0));
                break;

            default:
                _mainImage.sprite = character;
                break;
        }

    }

    private void SetAlpha(Image image, float alpha)
    {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
