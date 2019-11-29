//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity) entity.Logic;
        }

        public static void ShowPlayer(this EntityComponent entityComponent, PlayerData data)
        {
            entityComponent.ShowEntity(typeof(Player), "Player", Constant.AssetPriority.PlayerAsset, data);
        }

        public static void ShowIndicator(this EntityComponent entityComponent, IndicatorData data)
        {
            entityComponent.ShowEntity(typeof(Indicator), "Player", Constant.AssetPriority.PlayerAsset, data);
        }

        public static void ShowStaticGrid(this EntityComponent entityComponent, StaticGridData data)
        {
            entityComponent.ShowEntity(typeof(StaticGrid), "Map", Constant.AssetPriority.MapAsset, data);
        }

        public static void ShowStartGrid(this EntityComponent entityComponent, StartGridData data)
        {
            entityComponent.ShowEntity(typeof(StartGrid), "Map", Constant.AssetPriority.MapAsset, data);
        }

        public static void ShowVerticalGrid(this EntityComponent entityComponent, VerticalGridData data)
        {
            entityComponent.ShowEntity(typeof(VerticalGrid), "Map", Constant.AssetPriority.MapAsset, data);
        }

        public static void ShowHorizontalGrid(this EntityComponent entityComponent, HorizontalGridData data)
        {
            entityComponent.ShowEntity(typeof(HorizontalGrid), "Map", Constant.AssetPriority.MapAsset, data);
        }   
        
        public static void ShowLongStaticGrid(this EntityComponent entityComponent, LongStaticGridData data)
        {
            entityComponent.ShowEntity(typeof(LongStaticGrid), "Map", Constant.AssetPriority.MapAsset, data);
        }

        /// <summary>
        /// 不能放在有子类实体的Update
        /// </summary>
        /// <param name="entityComponent"></param>
        /// <param name="entity"></param>
        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId,
            string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup,
            int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup,
                priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}