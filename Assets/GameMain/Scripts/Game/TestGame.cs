using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class TestGame : GameBase
    {
        public override GameMode GameMode
        {
            get { return GameMode.Test; }
        }

        private float lastx;
        private float lasty;
        private int m_MapGridId;

        public override void Initialize()
        {
            base.Initialize();

            Init();

            GameEntry.Event.Subscribe(ReStartEventArgs.EventId, ReStart);
            GameEntry.Event.Subscribe(ShowNewMapEventArgs.EventId, OnShowMap);
        }

        private void Init()
        {
            m_MapGridId = 0;
            lastx = 0;
            lasty = 0;
            GameEntry.Entity.ShowPlayer(new PlayerData(GameEntry.Entity.GenerateSerialId(), 11));

            GameEntry.Entity.ShowStartGrid(new StartGridData(m_MapGridId, 20)
            {
                Position = new Vector3(0, -1),
            });
            ShowMap(1);
        }

        private void OnShowMap(object sender, GameEventArgs e)
        {
            ShowNewMapEventArgs ne = e as ShowNewMapEventArgs;
            if (ne == null)
            {
                return;
            }

            if (m_MapGridId - ne.Grid < 6)
            {
                ShowMap(m_MapGridId + 1);
                for (int i = m_MapGridId - 6; i < m_MapGridId - 12; i--)
                {
                    Log.Info(i);
                    if (GameEntry.Entity.HasEntity(i))
                    {
                        GameEntry.Entity.HideEntity(i);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void ReStart(object sender, GameEventArgs e)
        {
            ReStartEventArgs ne = e as ReStartEventArgs;
            if (ne == null)
            {
                return;
            }

            GameEntry.Entity.HideAllLoadedEntities();
            GameEntry.Entity.HideAllLoadingEntities();

            Init();
        }

        public override void Shutdown()
        {
            base.Shutdown();
            GameEntry.Event.Unsubscribe(ReStartEventArgs.EventId, ReStart);
            GameEntry.Event.Unsubscribe(ShowNewMapEventArgs.EventId, OnShowMap);
        }


        public void ShowMap(int id)
        {
            for (int i = id; i < id + 6; i++)
            {
                m_MapGridId = i;
                int a = GameFramework.Utility.Random.GetRandom(0, 100);
                lastx += ExUtility.GetRandom(2, 3);
                lasty += ExUtility.GetRandom(-1, 1);
                if (lasty < -2.5f)
                {
                    lasty += 1;
                }

                if (lasty > 2.5f)
                {
                    lasty -= 1;
                }

                if (a < 10 && a > 0)
                {
                    GameEntry.Entity.ShowVerticalGrid(
                        new VerticalGridData(m_MapGridId, 20)
                        {
                            Position = new Vector3(lastx, lasty),
                        });
                }
                else if (a < 20 && a > 10)
                {
                    GameEntry.Entity.ShowHorizontalGrid(
                        new HorizontalGridData(m_MapGridId, 20)
                        {
                            Position = new Vector3(lastx, lasty),
                        });
                }
                else if (a < 30 && a > 20)
                {
                    GameEntry.Entity.ShowLongStaticGrid(
                        new LongStaticGridData(m_MapGridId, 21)
                        {
                            Position = new Vector3(lastx, lasty),
                        });
                }
                else
                {
                    GameEntry.Entity.ShowStaticGrid(
                        new StaticGridData(m_MapGridId, 20)
                        {
                            Position = new Vector3(lastx, lasty),
                        });
                }
            }
        }
    }
}