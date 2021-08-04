using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private BoxCollider2D _roomCollider2D;

    public Bounds RoomBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        _roomCollider2D = GetComponent<BoxCollider2D>();
        RoomBounds = _roomCollider2D.bounds;
    }
}
