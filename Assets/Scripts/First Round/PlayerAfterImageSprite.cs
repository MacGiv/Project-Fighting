using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class PlayerAfterImageSprite : MonoBehaviour
    {
        [SerializeField] float activeTime = 0.1f;
        [SerializeField] float alphaSet = 0.8f;
        float timeActivated;
        float alpha;
        float alphaMultiplier = 0.85f;

        Color afterimageColor;

        Transform player;
        SpriteRenderer afterimageSR;
        SpriteRenderer playerSR;

        private void OnEnable()
        {
            afterimageSR = GetComponent<SpriteRenderer>();
            player = FindObjectOfType<PlayerController>().transform;
            playerSR = player.GetComponent<SpriteRenderer>();

            alpha = alphaSet;
            afterimageSR.sprite = playerSR.sprite;
            transform.position = player.position;
            transform.rotation = player.rotation;
            timeActivated = Time.time;
        }

        private void Update()
        {
            alpha *= alphaMultiplier;
            afterimageColor = new Color(1f, 1f, 1f, alpha);
            afterimageSR.color = afterimageColor;

            if (Time.time >= (timeActivated + activeTime))
            {
                PlayerAfterImagePool.Instance.AddToPool(gameObject);
            }

        }
    }

}

