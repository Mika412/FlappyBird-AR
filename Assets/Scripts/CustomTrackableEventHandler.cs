using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia {
    public class CustomTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        public GameObject mainGameobject;
        public GameManager gameManager;
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start() {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour) {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus) {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
                OnTrackingFound();
            } else {
                OnTrackingLost();
            }
        }
        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS      

        private void OnTrackingFound() {
            mainGameobject.SetActive(true);
            gameManager.ResetGame();
        }


        private void OnTrackingLost() {
            mainGameobject.SetActive(false);
        }

        #endregion // PUBLIC_METHODS
    }
}