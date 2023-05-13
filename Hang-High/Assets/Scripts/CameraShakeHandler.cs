using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MainGame.Cameras
{
	//Makes camera shaking functionality accessible from other classes. -- Cached references weren't working was forced to use getcomp each time
	public class CameraShakeHandler : Singleton<CameraShakeHandler>
	{
		public CameraShakeHandler instance;

		//cameras
		//public CinemachineFreeLook freeCam;
		public CinemachineVirtualCamera virtualCam;
        public float cameraShakeVal = 10f;
        public float shakeDuration = 0.03f;
        /*
		private List<CinemachineBasicMultiChannelPerlin> freeCamRigs;
		private CinemachineBasicMultiChannelPerlin vCamRig;
		*/

        private void Awake()
		{

			/*
			//Get Rigs
			for(int i = 0; i < 3; i++)
			{
				freeCamRigs.Add(freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
			}

			vCamRig = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
			*/
		}

		//preset camera shake with simple values
		public void BasicShake()
		{
			//Set all Free Cam rig vals
			//for(int i = 0; i < 3; i++)
			//{
				//freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 5f;
				//freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 5f;

				//freeCamRigs[i].m_AmplitudeGain = 10f;
				//freeCamRigs[i].m_FrequencyGain = 10f;
			//}

			//Set Virtual Cam Vals


			//vCamRig.m_AmplitudeGain = 10f;
			//vCamRig.m_FrequencyGain = 10f;

			virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = cameraShakeVal / 2;
			virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = cameraShakeVal;

			StartCoroutine(resetShakeAfterDuration(shakeDuration));
		}

		//Wait x time then turn all values back to 0
		private IEnumerator resetShakeAfterDuration(float _duration)
		{
			yield return new WaitForSeconds(_duration);

			//Set all Free Cam rig vals
			//for (int i = 0; i < 3; i++)
			//{
			//	freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
			//	freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;

				//freeCamRigs[i].m_AmplitudeGain = 0;
				//freeCamRigs[i].m_FrequencyGain = 0;
			//}

			//Reset Virtual Cam
			virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
			virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;

			//vCamRig.m_AmplitudeGain = 0;
			//vCamRig.m_FrequencyGain = 0;
		}

		public void CustomShake(float amp, float freq, float duration)
		{
			//Set all Free Cam rig vals
			//for (int i = 0; i < 3; i++)
			//{
			//	freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amp;
			//	freeCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freq;

				//freeCamRigs[i].m_AmplitudeGain = amp;
				//freeCamRigs[i].m_FrequencyGain = freq;
			//}

			//Set Virtual Cam Vals
			virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amp;
			virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freq;

			//vCamRig.m_AmplitudeGain = amp;
			//vCamRig.m_FrequencyGain = freq;

			StartCoroutine(resetShakeAfterDuration(duration));
		}
	}
}
