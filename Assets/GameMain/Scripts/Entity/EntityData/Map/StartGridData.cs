using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class StartGridData : MapGridData
    {
        public override int m_Score
        {
            get { return 0; }
        }

        public StartGridData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}