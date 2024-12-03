using BepInEx.Configuration;

namespace AtlyssCharacterMenuPlus
{
    internal static class Configuration
    {
        
        public static ConfigEntry<bool> UsePercentages = null!;
        
        internal static void BindConfiguration() 
        {
            UsePercentages = CharacterMenuPlus.GetConfig().Bind("General", "UsePercentages", false, "Display percentages instead of whole numbers for the slider labels.");
        }
        
    }
}
