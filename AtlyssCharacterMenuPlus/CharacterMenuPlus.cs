using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using BepInEx.Configuration;

namespace AtlyssCharacterMenuPlus
{
    
    [SuppressMessage("ReSharper", "UnusedMember.Local")] [SuppressMessage("ReSharper", "UnusedType.Global")]
    [BepInPlugin("net.portalsam.AtlyssCharacterMenuPlus", "CharacterMenuPlus", "1.0.5.0")]
    [BepInProcess("ATLYSS.exe")]
    public class CharacterMenuPlus : BaseUnityPlugin
    {

        internal static CharacterMenuPlus Instance = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;

        private void Awake()
        {
            Instance = this;
            Logger = base.Logger;
            
            Configuration.BindConfiguration();
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            
            Logger.LogInfo("CharacterMenuPlus has been initialized!");
        }
        
        internal static ConfigFile GetConfig() => Instance.Config;
        
    }
    
}
