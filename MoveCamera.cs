using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public bool inBoundaries;
    float previousX = 0;
    float nextX = 0;
    float previousY = 0;
    float nextY = 0;
    public Vector3 playerPosOffset;
    public Vector3 cameraOffset;
    private Vector3 offset;
    Vector3 XandYCoordinates = new Vector3(0, 0, 0);
    float middleOffset;
    Dictionary<int, List<int>> basePositions;

    // Start is called before the first frame update
    void Start()
    {
        playerPosOffset = new Vector3(32.95164f, 6.3f, 0);
        middleOffset = 204 + (51 / 2);
        cameraOffset = new Vector3(-7.5f, 6.3f, -11);
        basePositions = new Dictionary<int, List<int>>();
        CreateBasePositions(basePositions);
        player = GameObject.Find("Player");
        UpdatePreviousAndNextPositions();
        CheckBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position + playerPosOffset;
        if (playerPos.x >= 204 && playerPos.y > 149)
        {
            if (playerPos.x < middleOffset)
            {
                transform.position = new Vector3(204 + cameraOffset.x, 150 + cameraOffset.y, -11);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x, 150 + cameraOffset.y, -11);
            }
        }
        else if ((playerPos.x >= 204 && playerPos.x < 255) && playerPos.y > 59)
        {
            transform.position = new Vector3(204 + cameraOffset.x, playerPos.y + cameraOffset.y, -11);
        }
        else
        {
            CheckBoundaries();
        }
    }

    void CreateBasePositions(Dictionary<int, List<int>> basePositions)
    {
        basePositions.Add(0, new List<int> { 0 });
        basePositions.Add(51, new List<int> { 0 });
        basePositions.Add(102, new List<int> { 0 });
        basePositions.Add(153, new List<int> { -30, 0, 30, 60 });
        basePositions.Add(204, new List<int> { 0, 30, 60, 90, 120, 150 });
        basePositions.Add(255, new List<int> { 0, 30, 150 });
        basePositions.Add(306, new List<int> { 150 });
        basePositions.Add(357, new List<int> { 150 });
        basePositions.Add(408, new List<int> { 150 });
        basePositions.Add(459, new List<int> { 150 });
    }

    void SetPosition(Dictionary<int, List<int>> basePositions)
    {
        XandYCoordinates = FindXAndY(basePositions);

        if (XandYCoordinates.x != -1 && XandYCoordinates.y != -1)
        {
            transform.position = XandYCoordinates + cameraOffset;
        }
    }

    private void CheckBoundaries()
    {
        Vector3 playerPos = player.transform.position + playerPosOffset;

        // if player is in the box
        if ((previousX < playerPos.x && playerPos.x < nextX) && (previousY < playerPos.y && playerPos.y < nextY))
        {
            inBoundaries = true;
        }
        else
        {
            inBoundaries = false;
        }

        if (!inBoundaries)
        {
            SetPosition(basePositions);
            UpdatePreviousAndNextPositions();
        }
    }

    void UpdatePreviousAndNextPositions()
    {
        previousX = XandYCoordinates.x - 1;
        nextX = XandYCoordinates.x + 51;

        previousY = XandYCoordinates.y - 1;
        nextY = XandYCoordinates.y + 30;
    }

    Vector3 FindXAndY(Dictionary<int, List<int>> basePositions)
    {
        Vector3 playerPos = player.transform.position + playerPosOffset;

        int xCoordinate = ((int)playerPos.x / 51) * 51;
        int yCoordinate;
        if (playerPos.y < 0)
        {
            yCoordinate = ((((int)playerPos.y) / 30) - 1) * 30;
        }
        else
        {
            yCoordinate = (((int)playerPos.y) / 30) * 30;
        }

        if (basePositions.ContainsKey(xCoordinate))
        {
            if (basePositions[xCoordinate].Contains(yCoordinate))
            {
                return new Vector3(xCoordinate, yCoordinate, 0);
            }
            else
            {
                return new Vector3(-1, -1, 0);
            }
        }
        else
        {
            return new Vector3(-1, -1, 0);
        }
    }
}