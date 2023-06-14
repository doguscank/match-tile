using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseTile
{
    bool IsLocked { get; set; }

    List<IBaseTile> TopTiles { get; set; }
    List<IBaseTile> BottomTiles { get; set; }

    void Lock();
    void Unlock();
}
