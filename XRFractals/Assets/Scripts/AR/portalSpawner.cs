using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
	public class portalSpawner : MonoBehaviour
	{
        public GameObject portalPrefab, fractalPrefab;

		public Transform m_HitTransform;
		public float maxRayDistance = 50.0f;
		public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer
		private Animator portalAnim, fractalAnim;

        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");


                    // spawn Portal 
                    GameObject newPortal = Instantiate(portalPrefab);
                    newPortal.transform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    newPortal.transform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);

                    portalAnim = newPortal.GetComponent<Animator>();

                    // spawn Fractal
                    GameObject newFractal = Instantiate(fractalPrefab);
                    newFractal.transform.position = newPortal.transform.position;
                    newFractal.transform.rotation = newPortal.transform.rotation;

                    fractalAnim = newFractal.GetComponent<Animator>();

                    /*
                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
                    Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                    */

                    return true;
                }
            }
            return false;
        }

        void Start()
        {
            //portalAnim = portalPrefab.GetComponent<Animator>();
            //fractalAnim = fractalPrefab.GetComponent<Animator>();
        }
		
		// Update is called once per frame
		void Update () {
			#if UNITY_EDITOR   //we will only use this script on the editor side, though there is nothing that would prevent it from working on device
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				//we'll try to hit one of the plane collider gameobjects that were generated by the plugin
				//effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
				if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayer)) {
                    //we're going to get the position from the contact point
                    if (m_HitTransform != null)
                    {
                        m_HitTransform.position = hit.point;
                        Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));

                        //and the rotation from the transform of the plane collider
                        m_HitTransform.rotation = hit.transform.rotation;
                    }
				}
			}

#else
			if (Input.touchCount > 0) // && m_HitTransform != null)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
				{
					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

                    // prioritize reults types
                    ARHitTestResultType[] resultTypes = {
						//ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingGeometry,
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        //ARHitTestResultType.ARHitTestResultTypeEstimatedHorizontalPlane, 
						//ARHitTestResultType.ARHitTestResultTypeEstimatedVerticalPlane, 
						//ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    }; 
					
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
                             portalAnim.Play ("portal_open_anim",0,0.0f);
							 fractalAnim.Play ("frac_show_anim",0,0.0f);
                        
                             return;
                        }
                    }
				}
			}
#endif

        }


    }
}

