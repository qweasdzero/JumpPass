using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class StaticGridData : MapGridData
    {
        public StaticGridData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}