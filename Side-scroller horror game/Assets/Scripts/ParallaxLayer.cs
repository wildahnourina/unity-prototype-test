using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;//multiplier 0 objek tidak bergerak atau diam (terdekat) jadi terlewati dg mudah,
                                                      //multiplier 1 objek bergerak speed sama dg kamera, seperti tidak berubah (terjauh)
    [SerializeField] private bool enableLoop = true;
    [SerializeField] private float imageWidthOffset = 10;//bg akan loop 10 unit lebih awal (offset)

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        if (!enableLoop)
            return;

        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }
    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }
    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        if (!enableLoop)
            return;

        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
    }
}
