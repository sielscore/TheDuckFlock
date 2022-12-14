using System.Collections.Generic;
using UnityEngine;

namespace TheDuckFlock
{
    public class FlockManager : MonoSingleton<FlockManager>
    {

        [SerializeField] private List<DucksMother> duckMothers = new List<DucksMother>();
        [SerializeField] private List<Duckie> duckies = new List<Duckie>();

        /// <summary>
        /// 
        /// </summary>
        public Vector3 MotherPosition { get { return duckMothers.Count == 0 ? Vector3.zero : duckMothers[0].transform.position; } }
        /*
        public int CurrentDuckiesCount
        {
            get { return duckies.Count; }
        }

        
        public int LostDuckiesCount
        { 
            get { return _lostDuckiesCount; } 
        }

        //private int _currentDuckiesCount = 0;

        private int _lostDuckiesCount = 0;
        */


        /// <summary>
        /// 
        /// </summary>
        public void SpawnDucksMother(DucksMotherSpawnMarker marker)
        {
            // Sets DucksMother active
            GameObject duckObject = ObjectPooler.Instance.SpawnFromPool(PoolTag.DucksMother);

            // Sets position
            duckObject.transform.parent = WorldManager.Instance.FlockRoot;
            duckObject.transform.position = marker.transform.position + Vector3.up;

            // Adds mother to list
            DucksMother newDuck = duckObject.GetComponent<DucksMother>();
            duckMothers.Add(newDuck);

            duckObject.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eggPosition"></param>
        public void SpawnDuckie(Vector3 eggPosition)
        {
            // Sets Duckie active
            GameObject duckObject = ObjectPooler.Instance.SpawnFromPool(PoolTag.Duckie);
            duckObject.SetActive(true);

            // Sets position
            duckObject.transform.parent = WorldManager.Instance.FlockRoot;
            duckObject.transform.position = eggPosition;

            // Adds Duckie to list
            Duckie newDuck = duckObject.GetComponent<Duckie>();
            duckies.Add(newDuck);

            SoundManager.Instance.PlaySound(SoundTag.SqueakingChicks);
            SoundManager.Instance.TurnOffVolume(SoundTag.SqueakingChicks);
            if (duckies.Count > 1)
            {
                SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                if (duckies.Count > 2)
                {
                    SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                    if (duckies.Count > 3)
                    {
                        SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                        if (duckies.Count > 4)
                        {
                            SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                            if (duckies.Count > 5)
                            {
                                SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                                if (duckies.Count > 6)
                                {
                                    SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                                    if (duckies.Count > 7)
                                    {
                                        SoundManager.Instance.TurnUpVolume(SoundTag.SqueakingChicks);
                                    }
                                }
                            }
                        }

                    }
                }
            }
           // _currentDuckiesCount++;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllDucks()
        {
            // Clears lists
            duckMothers.Clear();
            duckies.Clear();

            // Clears FlockRoot
            DuckController[] ducks = WorldManager.Instance.FlockRoot.GetComponentsInChildren<DuckController>();

            foreach (DuckController duck in ducks)
            {
                duck.transform.SetParent(ObjectPooler.Instance.PoolRoot, false);
                duck.transform.position = Vector3.zero;
                duck.gameObject.SetActive(false);
            }

            SoundManager.Instance.TurnOffWithFade(SoundTag.SqueakingChicks);

            //_currentDuckiesCount = 0;
            //_lostDuckiesCount = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionToCheck"></param>
        /// <returns></returns>
        public DuckController GetClosestMotherCandidate(Vector3 positionToCheck, DuckController duckToExclude = null)
        {

            DuckController[] ducks = WorldManager.Instance.FlockRoot.GetComponentsInChildren<DuckController>();

            //Debug.Log(name + " >> ducks count = " + ducks.Length);

            if (ducks.Length == 0)
            {
                return null;
            }

            //float minDistance = duckMothers.Count > 0 ? Vector3.Distance(positionToCheck, MotherPosition) : float.PositiveInfinity;
           // DuckController closestDuckController = duckMothers.Count > 0 ? duckMothers[0] : null;
            float minDistance =float.PositiveInfinity;
            DuckController closestDuckController = null;

            if (duckMothers.Count > 0 && Vector3.Distance(positionToCheck, MotherPosition) < minDistance)
            {
                closestDuckController = duckMothers[0];
            }
            

            foreach (DuckController duck in ducks)
            {
                if ( duck.IsCandidateForParent && (duckToExclude == null || duckToExclude != duck))
                //if (duckToExclude == null || duckToExclude != duck)
                {
                    float distance = Vector3.Distance(positionToCheck, duck.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestDuckController = duck;
                    }
                }

            }
            
           
            

            return closestDuckController;

        }

        /// <summary>
        /// 
        /// </summary>
        /*
        public void IncrementLostDuckiesCounter()
        {
           // _lostDuckiesCount++;

            Debug.Log(name + " | current lost duckies count: " + _lostDuckiesCount);


        }
        */
      
    }
}
