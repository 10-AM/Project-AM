using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace AM.UpBall.MODULE
{
    public static class SpriteRenderExter
    {
        public static void SetAlpha(this SpriteRenderer sprite, float alpha)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }

        public static void SetAlpha(this Image sprite, float alpha)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }

        public static void SetAlpha(this Text sprite, float alpha)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
    }
}