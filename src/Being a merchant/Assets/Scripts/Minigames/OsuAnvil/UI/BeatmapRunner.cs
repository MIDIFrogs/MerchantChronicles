using SibGameJam.Minigames;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BeatmapRunner : MonoBehaviour
{
    [SerializeField] RectTransform circlesPanel;
    [SerializeField] ApproachCircle circlePrefab;
    [SerializeField] Conductor conductor;
    [SerializeField] TextMeshProUGUI healthBar;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;
    [SerializeField] Transform centerSide;
    [SerializeField] Image goodImg;
    [SerializeField] Image badImg;

    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip lmbSound;
    [SerializeField] AudioClip rmbSound;

    [Header("Level params")]
    [SerializeField] float failDelay;
    [SerializeField] int minHealth;
    [SerializeField] int maxHealth;

    private readonly LinkedPool<ApproachCircle> circlesPool;
    private Beatmap beatmap;
    private AudioSource player;
    private int health;

    // Circles
    private List<float> leftCirclesApproach;
    private List<float> rightCirclesApproach;

    private readonly HashSet<ApproachCircle> unclickedCircles = new();

    // Coroutines
    private Coroutine gameCoroutine;
    private Coroutine beatCoroutine1;
    private Coroutine beatCoroutine2;

    // Gameplay
    private int goodStreak;

    public int Health
    {
        get => health;
        set
        {
            health = Math.Clamp(value, minHealth, maxHealth);
            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        healthBar.text = health.ToString();
    }

    public event EventHandler<bool> GameEnd;

    public BeatmapRunner()
    {
        circlesPool = new(CreateCircle);
    }

    public void StartGame(Beatmap beatmap, AudioSource gameSource)
    {
        Health = maxHealth;
        this.beatmap = beatmap;
        conductor.FirstBeatOffset = beatmap.FirstBeatOffset;
        conductor.SongBPM = beatmap.SongBPM;
        rightCirclesApproach = beatmap.BeatTicks.Where(x => x.IsRight).Select(x => x.TickTime).ToList();
        leftCirclesApproach = beatmap.BeatTicks.Where(x => !x.IsRight).Select(x => x.TickTime).ToList();
        player = gameSource;
        conductor.Begin();
        gameCoroutine = StartCoroutine(RunBeatmap());
        beatCoroutine1 = StartCoroutine(PlayBeat(rightCirclesApproach, true));
        beatCoroutine2 = StartCoroutine(PlayBeat(leftCirclesApproach, false));
    }

    public void StopGame()
    {
        StopCoroutine(gameCoroutine);
        StopCoroutine(beatCoroutine1);
        StopCoroutine(beatCoroutine2);
    }

    public IEnumerator PlayBeat(List<float> circles, bool right)
    {
        for (int i = 0; i < circles.Count; i++)
        {
            float nextTime = circles[i];
            float nextApproachTime = nextTime - 1 / beatmap.ApproachRate;
            yield return new WaitUntil(() => conductor.SongPosition > nextApproachTime);
            var circle = circlesPool.Get();
            circle.Approached += Circle_Approached;
            circle.Speed = beatmap.ApproachRate;
            circle.TimeTick = nextTime;
            circle.IsRightSide = right;
            if (right)
            {
                circle.From = rightSide;
            }
            else
            {
                circle.From = leftSide;
            }
            circle.To = centerSide;
            unclickedCircles.Add(circle);
            StartCoroutine(circle.Approach());
            Debug.Log($"Summoned circle from {(right ? "right" : "left")}");
        }
    }

    private void Circle_Approached(object sender, EventArgs e)
    {
        var circle = (ApproachCircle)sender;
        if (unclickedCircles.Contains(circle))
        {
            Debug.Log("Player missed a circle.");
            Miss();
        }
        circle.Approached -= Circle_Approached;
        unclickedCircles.Remove(circle);
        circlesPool.Release(circle);
    }

    public void Miss()
    {
        Debug.Log("Missed :_(");
        goodStreak = 0;
        Health -= 3;
        StartCoroutine(AnimateHitResult(badImg, Color.black));
    }

    public void Fail()
    {
        Debug.Log("Failed!");
        goodStreak = 0;
        Health--;
        StartCoroutine(AnimateHitResult(badImg, Color.red));
    }

    public void Success()
    {
        Debug.Log("Successfull hit!");
        goodStreak++;
        if (goodStreak > 5)
        {
            Health++;
        }
        StartCoroutine(AnimateHitResult(goodImg, Color.green));
    }

    public IEnumerator RunBeatmap()
    {
        yield return new WaitUntil(() => conductor.SongPosition > beatmap.AudioBegin.length);
        bool win = Health > 0;
        AudioClip toPlay = win ? beatmap.AudioWin : beatmap.AudioLose;
        player.PlayOneShot(toPlay);
        yield return new WaitUntil(() => conductor.SongPosition > beatmap.AudioBegin.length);
        GameEnd?.Invoke(this, win);
    }

    public ApproachCircle CreateCircle()
    {
        var circle = Instantiate(circlePrefab, circlesPanel);
        return circle;
    }

    public void FixedUpdate()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            audio.PlayOneShot(lmbSound);
            ClickCircle(false);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            audio.PlayOneShot(rmbSound);
            ClickCircle(true);
        }
    }

    private void ClickCircle(bool isRight)
    {
        float time = conductor.SongPosition;
        // Find nearest.
        var nearest = (from item in unclickedCircles
                      where item.IsRightSide == isRight
                      let dt = Mathf.Abs(item.TimeTick - time)
                      orderby dt
                      where dt < failDelay
                      select new { item, dt }).FirstOrDefault();
        if (nearest != null)
        {
            float delta = nearest.dt;
            if (delta < failDelay)
            {
                Success();
            }
            else
            {
                Fail();
            }
            unclickedCircles.Remove(nearest.item);
        }
    }
    
    public IEnumerator AnimateHitResult(Image target, Color color)
    {
        target.color = color;
        yield return new WaitForSeconds(0.3f);
        target.color = default;
    }
}
