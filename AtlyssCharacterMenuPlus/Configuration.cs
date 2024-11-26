using BepInEx.Configuration;

namespace AtlyssCharacterMenuPlus
{
    public class Configuration
    {
        public static readonly ConfigEntry<bool> UsePercentages = CharacterMenuPlus.GetConfig().Bind("General", "UsePercentages", false, "Display percentages instead of whole numbers for the slider labels.");
    }
}