using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnOffset = 1f;
    [SerializeField] private Transform conveyor;

    [Header("Animations")]
    [SerializeField] private float bulletAnimationDuration = 0.2f;
    [SerializeField] private float pulseAnimationDuration = 0.2f;
    [SerializeField] private float animationStrength = 0.2f;

    public GameObject blocksPool;
    public List<BlockParent> blockParents;

    private float timer;
    private int index = 0;

    private GameObject bulletsFolder;

    private void Start()
    {
        timer = spawnInterval - 1;
        bulletsFolder = new GameObject("Bullets");
    }

    private void Update()
    {
        if (blockParents.Count <= 0)
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            index = 0;
            timer = 0f;

            blocksPool.transform.DOMoveY(-animationStrength, pulseAnimationDuration).SetLoops(2, LoopType.Yoyo);

            foreach (BlockParent block in blockParents)
            {
                for (int i = 0; i < block.bulletsToGenerate; i++)
                {
                    GenerateBullet(block);
                }
            }
        }
    }

    private void GenerateBullet(BlockParent block)
    {
        Vector3 spawnPosition = block.transform.position;
        GameObject bullet = Instantiate(block.bulletPrefab, spawnPosition, block.bulletPrefab.transform.rotation);
        bullet.transform.localScale *= 1.5f;
        bullet.transform.parent = bulletsFolder.transform;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        bullet.transform.DOMove(conveyor.position + new Vector3(0, 0f, index * spawnOffset), bulletAnimationDuration).OnComplete(() =>
        {
            rb.freezeRotation = false;
            rb.isKinematic = true;
        });

        index++;
    }
}
