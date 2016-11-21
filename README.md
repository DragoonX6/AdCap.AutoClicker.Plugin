# AdCap.AutoClicker.Plugin
An Adventure Capitalist micro manager and Mars profit booster auto clicker.
This mod is a plugin written for UnityInjector by Usagirei from the Hongfire forums.
By the time of writing this, he deleted (or made private) the repositories for both UnityInjector and ReiPatcher.
The necessary (binary) files can be found on Hongfire: [ReiPatcher](http://www.hongfire.com/forum/forum/hentai-lair/hf-modding-translation/custom-maid-3d-2-mods/414074-reipatcher-general-purpose-net-assembly-patcher) and [UnityInjector](http://www.hongfire.com/forum/forum/hentai-lair/hf-modding-translation/custom-maid-3d-2-mods/414075-unityinjector-plugin-powered-unity-code-injector).

# How to build
## Prerequisites
- [.Net Framework 3.5](https://www.microsoft.com/en-us/download/details.aspx?id=25150)
- [.Net Framework 4.0 (Required for MSBuild)](https://www.microsoft.com/en-us/download/details.aspx?id=17851)
- [Adventure Capitalist](http://store.steampowered.com/app/346900/)
- [UnityInjector](http://www.hongfire.com/forum/forum/hentai-lair/hf-modding-translation/custom-maid-3d-2-mods/414075-unityinjector-plugin-powered-unity-code-injector)

## How to build (for real this time)
1. Make sure you downloaded and installed all the prerequisites.
2. Navigate to the AdCap managed folder (commonly `C:\Program Files (x86)\Steam\steamapps\common\AdVenture Capitalist\adventure-capitalist_Data\Managed`) and copy `Assembly-CSharp.dll`, `Assembly-CSharp-firstpass.dll`, `UnityEngine.dll`, `UnityEngine.UI.dll`, and `UnityInjector.dll` to the References folder.
3. Copy UnityInjector.dll to the References folder.
4. Open a commandprompt and execute the following command `C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe AdCap.AutoClicker.Plugin.sln /p:Configuration=Release /p:Platform="X86"`
5. The resulting plugin can be found in `AdCap.AutoClicker.Plugin\bin\Release`
6. (Optional) Instead of compiling from the commandline in step 4, you can also use a recent Visual Studio version (with C# support) to edit and build. (This takes at least 7GB on the system, hence I present you the commandline option.)

# How to install / run
1. Navigate to the install folder of AdCap.
2. Extract the ReiPatcher archive to a folder called `ReiPatcher`.
3. Enter the `ReiPatcher` folder and create a new folder called `Patches`.
4. From the UnityInjector archive, drop `ReiPatcher\UnityInjector.Patcher.dll` in the `Patches` folder.
5. In the `ReiPatcher` folder, create a new file called `AdCap.ini` and put the following contents in the file:

		;Adventure Capitalist - ReiPatcher Configuration File
		;
		;InstallPath
		;@adcap=..\
		;
		[ReiPatcher]
		;Directory to search for Patches
		PatchesDir=Patches
		;Directory to Look for Assemblies to Patch
		AssembliesDir=%adcap%\adventure-capitalist_Data\Managed
		
		[Launch]
		Executable=adventure-capitalist.exe
		Arguments=
		Directory=%adcap%
		
		;UnityInjector Patch Entry Point
		[UnityInjector]
		Class=LoadingScreenUI
		Method=Awake
		Assembly=Assembly-CSharp
		
		[Assemblies]
		Assembly-CSharp=Assembly-CSharp.dll
6. Create another file called `__Patch.bat` and put the following contents in the file:

		@echo off
		ReiPatcher.exe -c AdCap
		pause
7. Back in the install folder of AdCap navigate to the `Managed` folder and place both `UnityInjector.dll`, and `ExIni.dll` in there.
8. Back in the install folder of Adcap again, create a folder called `UnityInjector`, place `AdCap.AutoClicker.Plugin.dll` in here.
9. Run `ReiPatcher\__Patch.bat` and it'll automatically start AdCap. You don't need to patch AdCap again unless you want to add another ReiPatcher plugin (not to be confused with UnityInjector plugins) or Steam has updated AdCap.