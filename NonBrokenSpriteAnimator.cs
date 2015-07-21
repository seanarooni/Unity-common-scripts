using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class NonBrokenSpriteAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    //fields actually representing the state of the animator
    [SerializeField]
    private NonBrokenSpriteAnimationClip currentAnimation = null;
    private float animationBeginTime;

    //fields that just run calculations based on other fields
    private float animationEndTime { get { return animationBeginTime + currentAnimation.animationDuration; } }
    public bool IsPlaying { get { return currentAnimation != null; } }
    public int CurrentFrameIndex
    {
        get
        {
            //figure out how far through we've gone through the animation in percent
            float timeElapsed = Time.time - animationBeginTime;
            float percentElapsed = timeElapsed / currentAnimation.animationDuration;

            //use the percent to find an index - if percent is 0.5 and the number of frames is 16, then 50% of that is frame 8
            return (int)System.Math.Floor(percentElapsed * currentAnimation.spriteFrames.Length);
        }
    }

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (currentAnimation != null)
        {
            BeginAnimation(currentAnimation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if we've reached the end of the animation
        if (currentAnimation != null && Time.time > animationEndTime)
        {
            //if it's looping, update the animation begin time to start the animation over from the beginning
            if (currentAnimation.animationType == NonBrokenSpriteAnimationClip.AnimationType.Looping)
            {
                animationBeginTime += currentAnimation.animationDuration;
            }
            else
            {
                currentAnimation = null;
            }
        }

        if (currentAnimation != null)
        {
            //update the sprite renderer with the current frame
            spriteRenderer.sprite = currentAnimation.spriteFrames[CurrentFrameIndex];
        }
    }

    public void BeginAnimation(NonBrokenSpriteAnimationClip clip)
    {
        currentAnimation = clip;
        animationBeginTime = Time.time;
        spriteRenderer.sprite = currentAnimation.spriteFrames[0];
    }
}
