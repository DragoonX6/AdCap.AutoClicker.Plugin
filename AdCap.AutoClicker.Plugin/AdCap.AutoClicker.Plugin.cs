using UnityEngine;
using UnityEngine.UI;
using UnityInjector;
using UnityInjector.Attributes;
using System;
using System.Collections;
using System.Reflection;

namespace AdCap.AutoClicker.Plugin
{
	[PluginName("AdCap.AutoClicker.Plugin"), PluginVersion("0.0.0.0")]
    public class AutoClickerPlugin: PluginBase
    {
		public static string PluginName = "AdCap.AutoClicker.Plugin";

		private bool foundMarsBooster = false;

		private ProfitBooster booster;
		private MethodInfo OnFightBack;

		private bool foundMicroManager = false;
		private Button.ButtonClickedEvent onMicroManagerClicked;
		
		private float lastClick = 0.0f;

		public void Awake()
		{
			DontDestroyOnLoad(this);
			Console.WriteLine("In Awake()");
		}

		public void OnLevelWasLoaded(int level)
		{
			Console.WriteLine("In OnLevelWasLoaded(int level)");
			Console.WriteLine("Starting coroutine to find Mars booster");
			StartCoroutine(FindMarsBooster());

			Console.WriteLine("Starting coroutine to find micromanager.");
			StartCoroutine(FindMicroManager());
			
			lastClick = 0.0f;
		}

		private IEnumerator FindMarsBooster()
		{
			foundMarsBooster = false;
			yield return new WaitForEndOfFrame();

			Console.WriteLine("Trying to find Mars profit booster.");
			ProfitBoosterView boosterView = FindObjectOfType<ProfitBoosterView>();
			if(boosterView)
			{
				Console.WriteLine("Found Mars profit booster");
				booster = boosterView.Booster.Value;
				if(booster != null)
				{
					Console.WriteLine("Got reactive property!");
				}

				OnFightBack = booster.GetType().GetMethod("OnFightBack",
					BindingFlags.NonPublic | BindingFlags.Instance);

				if(OnFightBack != null)
				{
					Console.WriteLine("Got method!");
					foundMarsBooster = true;
				}
				else
				{
					Console.WriteLine("Didn't get method.");
					foundMarsBooster = false;
				}

				yield break;
			}

			foundMarsBooster = false;
		}

		private IEnumerator FindMicroManager()
		{
			foundMicroManager = false;
			yield return new WaitForEndOfFrame();

			Console.WriteLine("Trying to find a micromanager.");
			MicroManagerView view = FindObjectOfType<MicroManagerView>();
			if(view)
			{
				Console.WriteLine("Found a micromanager!");
				onMicroManagerClicked = view.RunButton.onClick;
				foundMicroManager = true;
			}
			else
			{
				Console.WriteLine("No micromanager found.");
				foundMicroManager = false;
			}
		}

		public void Update()
		{
			if(lastClick >= 0.2f)
			{
				if(foundMarsBooster)
				{
					OnFightBack.Invoke(booster, null);
				}

				if(foundMicroManager)
				{
					onMicroManagerClicked.Invoke();
				}
				
				lastClick = 0.0f;
			}
			
			lastClick += Time.deltaTime;
		}
	}
}
