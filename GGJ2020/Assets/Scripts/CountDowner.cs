using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace KandooZ
{
    public class CountDowner : MonoBehaviour
    {
        public int countDownTimes = 3;
        public string countDownPhrase = "GO";
        private TextMeshProUGUI text;
        private int count;
        private System.Action callback;
        public bool is_repair_timer_ = false;

        public TextMeshProUGUI Text
        {
            get
            {
                text = this.GetComponent<TextMeshProUGUI>();
                if (text == null) Debug.LogError("No Text Mesh On");
                return text;
            }

            set
            {
                text = value;
            }
        }

        private void Start()
        {
            //Text = this.GetComponent<TextMeshProUGUI>();
            //if (Text == null) Debug.LogError("No Text Mesh On");
            transform.localScale = Vector3.zero;
            if (countDownTimes <= 0) countDownTimes = 1;
        }

       
        public void StartCountDown(System.Action callback)
        {
            count = countDownTimes;
            this.callback = callback;
            CountDown();
        }

        private void CountDown()
        {
            //Debug.Log(count);
            transform.localScale = Vector3.zero;
            Text.text = count.ToString();
            transform.LeanScale(Vector3.one, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                count--;
                if(count == 1 && is_repair_timer_)
                {
                    FindObjectOfType<AudioManager>().Play("RepairIncoming");
                }
                if (count > 0)
                {
                    CountDown();
                }

                else
                {
                    Text.text = countDownPhrase;
                    transform.localScale = Vector3.zero;
                    transform.LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                    {
                        transform.LeanScale(Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                        {

                            callback?.Invoke();

                        });
                    });
                }
            });
        }

        public void CancelCountDown(System.Action callback)
        {
            LeanTween.cancel(this.gameObject);
            transform.LeanScale(Vector3.zero, 0.1f).setOnComplete(() =>
            {
                callback?.Invoke();
            });
        }

        
    }
}