
using UnityEngine;

namespace TheDuckFlock
{
    /// <summary>
    /// Implementation of Observer Pattern
    /// </summary>
    public class GameplayEventsManager : EventsManager<GameplayEvent, IGameplayEventsListener<Vector3>, Vector3>
    {
        //public static int count = 0;
        public static void SetupListeners(IGameplayEventsListener<Vector3> listeners)
        {
            RemoveListeners();

            //Debug.Log("GameplayEventsManager | " + count++);

            CurrentListeners = listeners;

            RegisterListener(GameplayEvent.StartGame, listeners.OnStartGame);
            RegisterListener(GameplayEvent.RestartGame, listeners.OnRestartGame);
            RegisterListener(GameplayEvent.QuitGame, listeners.OnQuitGame);

            RegisterListener(GameplayEvent.DucksMotherLost, listeners.OnDucksMotherLost);
            RegisterListener(GameplayEvent.DuckieLost, listeners.OnDuckieLost);

            RegisterListener(GameplayEvent.EggLost, listeners.OnEggLost);
            RegisterListener(GameplayEvent.EggHatched, listeners.OnEggHatched);

            RegisterListener(GameplayEvent.ReturnedToNest, listeners.OnReturnedToNest);


            RegisterListener(GameplayEvent.ScoreGoalAchieved, listeners.OnScoreGoalAchieved);
            RegisterListener(GameplayEvent.ScoreGoalLost, listeners.OnScoreGoalLost);
        }

        public static void RemoveListeners()
        {
            if (CurrentListeners == null)
            {
                return;
            }
            //Debug.Log("GameplayEventsManager | " + count++);
            UnregisterListener(GameplayEvent.StartGame, CurrentListeners.OnStartGame);
            UnregisterListener(GameplayEvent.RestartGame, CurrentListeners.OnRestartGame);
            UnregisterListener(GameplayEvent.QuitGame, CurrentListeners.OnQuitGame);

            UnregisterListener(GameplayEvent.DucksMotherLost, CurrentListeners.OnDucksMotherLost);
            UnregisterListener(GameplayEvent.DuckieLost, CurrentListeners.OnDuckieLost);

            UnregisterListener(GameplayEvent.EggLost, CurrentListeners.OnEggLost);
            UnregisterListener(GameplayEvent.EggHatched, CurrentListeners.OnEggHatched);

            UnregisterListener(GameplayEvent.ReturnedToNest, CurrentListeners.OnReturnedToNest);

            UnregisterListener(GameplayEvent.ScoreGoalAchieved, CurrentListeners.OnScoreGoalAchieved);
            UnregisterListener(GameplayEvent.ScoreGoalLost, CurrentListeners.OnScoreGoalLost);
        }

    }
}