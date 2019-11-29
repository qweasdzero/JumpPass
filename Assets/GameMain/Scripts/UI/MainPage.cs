using DG.Tweening;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class MainPageModel : UGuiFormModel<MainPage, MainPageModel>
    {
        #region ScoreProperty

        private readonly Property<int> _privateScoreProperty = new Property<int>();

        public Property<int> ScoreProperty
        {
            get { return _privateScoreProperty; }
        }

        public int Score
        {
            get { return _privateScoreProperty.GetValue(); }
            set { _privateScoreProperty.SetValue(value); }
        }

        #endregion

        #region ScoreProperty

        private readonly Property<int> _privateMaxScoreProperty = new Property<int>();

        public Property<int> MaxScoreProperty
        {
            get { return _privateMaxScoreProperty; }
        }

        public int MaxScore
        {
            get { return _privateMaxScoreProperty.GetValue(); }
            set { _privateMaxScoreProperty.SetValue(value); }
        }

        #endregion

        #region PowerProperty

        private readonly Property<float> _privatePowerProperty = new Property<float>();

        public Property<float> PowerProperty
        {
            get { return _privatePowerProperty; }
        }

        public float Power
        {
            get { return _privatePowerProperty.GetValue(); }
            set { _privatePowerProperty.SetValue(value); }
        }

        #endregion

        #region GameOverPosProperty

        private readonly Property<Vector3> _privateGameOverPosProperty = new Property<Vector3>();

        public Property<Vector3> GameOverPosProperty
        {
            get { return _privateGameOverPosProperty; }
        }

        public Vector3 GameOverPos
        {
            get { return _privateGameOverPosProperty.GetValue(); }
            set { _privateGameOverPosProperty.SetValue(value); }
        }

        #endregion

        public void ReturnToMain()
        {
            Page.ReturnToMain();
        }

        public void ReturnToMenu()
        {
            Page.ReturnToMenu();
        }
    }

    public class MainPage : UGuiFormPage<MainPage, MainPageModel>
    {
        private Tweener m_GameOver;
        private Tweener m_Power;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Model.Power = 0;
            Model.GameOverPos = new Vector3(0, -1170, 0);
            m_Power = DOTween.To(() => Model.Power, x => Model.Power = x, 1, 1).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo).Pause();

            
            GameEntry.Event.Subscribe(AddScoreEventArgs.EventId, OnAddScore);
            
            GameEntry.Event.Subscribe(GameOverEventArgs.EventId, OnGameOver);
        }

        private void OnAddScore(object sender, GameEventArgs e)
        {
            AddScoreEventArgs ne = e as AddScoreEventArgs;
            if (ne == null)
            {
                return;
            }

            Model.Score += ne.Score;
        }

        private void OnGameOver(object sender, GameEventArgs e)
        {
            GameOverEventArgs ne = e as GameOverEventArgs;
            if (ne == null)
            {
                return;
            }

            if (Model.MaxScore < Model.Score)
            {
                Model.MaxScore = Model.Score;
            }

            m_GameOver = DOTween.To(() => Model.GameOverPos, x => Model.GameOverPos = x, Vector3.zero, 1);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                m_Power.Restart();
            }

            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.JoystickButton0))
            {
                m_Power.Pause();
                
                GameEntry.Event.Fire(this, ReferencePool.Acquire<StartJumpEventArgs>().Fill(Model.Power));
                Model.Power = 0;
            }
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            
            GameEntry.Event.Unsubscribe(AddScoreEventArgs.EventId, OnAddScore);
            
            GameEntry.Event.Unsubscribe(GameOverEventArgs.EventId, OnGameOver);
        }

        public void ReturnToMain()
        {
            if (m_GameOver != null)
            {
                m_GameOver.Kill();
                m_GameOver = null;
            }

            Model.GameOverPos = new Vector3(0, -1170, 0);
            Model.Score = 0;
            
            GameEntry.Event.Fire(this, ReferencePool.Acquire<ReStartEventArgs>().Fill());
        }

        public void ReturnToMenu()
        {
            Model.Score = 0;
            
            GameEntry.Event.Fire(this, ReferencePool.Acquire<ReturnMenuEventArgs>().Fill());
        }
    }
}