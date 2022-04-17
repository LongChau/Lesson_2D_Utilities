using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Lesson2D
{
    public class Canvas_UIHealth : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _txtHp;
        [SerializeField] RectTransform _hpBar;
        [SerializeField] Image _imgHp;
        [SerializeField] Image _imgHp_2;
        [SerializeField] int _configHp = 500;
        [SerializeField] int _currentHp;

        int _fromHp;
        int _toHp;

        // Start is called before the first frame update
        void Start()
        {
            _currentHp = _configHp;
            _fromHp = _configHp;
            _toHp = _configHp;

            _txtHp.SetText($"{_currentHp}");

            //_hpBar.DOPunchScale(Vector3.one * 1.3f, 1.5f);
            _hpBar.DOScale(Vector3.one * 1.3f, 0.5f).OnComplete(() =>
            {
                _hpBar.DOScale(Vector3.one, 0.5f);
            });
        }

        // Update is called once per frame
        void Update()
        {
            //_imgHp.fillAmount = (float)_currentHp / _configHp;

            // Lerp imgHp value.
            //float hpPercent = (float)_currentHp / _configHp;
            //_imgHp.fillAmount = Mathf.Lerp(_imgHp.fillAmount, hpPercent, Time.deltaTime);

            // Lerp with percentage.
            //_imgHp.fillAmount = Mathf.Lerp(_imgHp.fillAmount, hpPercent, hpPercent / _imgHp.fillAmount);
        }

        [ContextMenu("TakeDamage")]
        void TakeDamage()
        {
            _fromHp = _currentHp;
            _currentHp -= 50;
            _toHp = _currentHp;

            _imgHp_2.fillAmount = _imgHp.fillAmount;
            _imgHp_2.CrossFadeAlpha(1f, 0f, true);
            _imgHp_2.CrossFadeAlpha(0f, 3f, true);

            float hpPercent = (float)_currentHp / _configHp;
            float fillAmount = _imgHp.fillAmount;
            var tween = DOVirtual.Float(fillAmount, hpPercent, 1.5f, (amount) =>
            {
                _imgHp.fillAmount = amount;
            });

            DOVirtual.Int(_fromHp, _toHp, 2.5f, (hp) =>
            {
                _txtHp.SetText($"{hp}");
            });
        }
    }
}
