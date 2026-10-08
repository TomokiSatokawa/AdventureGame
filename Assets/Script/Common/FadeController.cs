using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    /// <summary>
    /// 画面のフェード処理を担当するクラス。
    /// </summary>
    public class FadeController : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _duration = 0.5f;

        /// <summary>
        /// 画面を暗転させます。
        /// </summary>
        public async UniTask FadeOut()
        {
            SetFade(false);

            await _fadeImage
                .DOFade(1f, _duration)
                .AsyncWaitForCompletion();
        }

        /// <summary>
        /// 画面を明転させます。
        /// </summary>
        public async UniTask FadeIn()
        {
            SetFade(true);

            await _fadeImage
                .DOFade(0f, _duration)
                .AsyncWaitForCompletion();
        }

        /// <summary>
        /// フェード状態を即座に設定します。
        /// </summary>
        public void SetFade(bool isFade)
        {
            var color = _fadeImage.color;
            color.a = isFade ? 1f : 0f;
            _fadeImage.color = color;
        }
    }
}
