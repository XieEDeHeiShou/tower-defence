﻿using Core.Data;
using Core.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Game
{
    /// <inheritdoc />
    /// <summary>
    ///     Game Manager - a persistent single that handles persistence, and level lists, etc.
    ///     This should be initialized when the game starts.
    /// </summary>
    public class GameManager : GameManagerBase<GameManager, GameDataStore>
    {
        /// <summary>
        ///     Script-able object for list of levels
        /// </summary>
        public LevelList levelList;

        /// <inheritdoc />
        /// <summary>
        ///     Set sleep timeout to never sleep
        /// </summary>
        protected override void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            base.Awake();
        }

        /// <summary>
        ///     Method used for completing the level
        /// </summary>
        /// <param name="levelId">The levelId to mark as complete</param>
        /// <param name="starsEarned"></param>
        public void CompleteLevel(string levelId, int starsEarned)
        {
            if (!levelList.ContainsKey(levelId))
            {
                Debug.LogWarningFormat("[GAME] Cannot complete level with id = {0}. Not in level list", levelId);
                return;
            }

            MDataStore.CompleteLevel(levelId, starsEarned);
            SaveData();
        }

        /// <summary>
        ///     Gets the id for the current level
        /// </summary>
        public LevelItem GetLevelForCurrentScene()
        {
            var sceneName = SceneManager.GetActiveScene().name;

            return levelList.GetLevelByScene(sceneName);
        }

//		/// <summary>
//		/// Determines if a specific level is completed
//		/// </summary>
//		/// <param name="levelId">The level ID to check</param>
//		/// <returns>true if the level is completed</returns>
//		public bool IsLevelCompleted(string levelId)
//		{
//			if (levelList.ContainsKey(levelId)) return MDataStore.IsLevelCompleted(levelId);
//			Debug.LogWarningFormat("[GAME] Cannot check if level with id = {0} is completed. Not in level list", levelId);
//			return false;
//
//		}

        /// <summary>
        ///     Gets the stars earned on a given level
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public int GetStarsForLevel(string levelId)
        {
            if (levelList.ContainsKey(levelId)) return MDataStore.GetNumberOfStarForLevel(levelId);
            Debug.LogWarningFormat("[GAME] Cannot check if level with id = {0} is completed. Not in level list",
                levelId);
            return 0;
        }
    }
}