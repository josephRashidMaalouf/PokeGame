using PokemonCommon.Enums;

namespace PokemonCommon;

public static class BattleUi
{
    private static Dictionary<Effectiveness, string> messages = new Dictionary<Effectiveness, string>()
    {
        { Effectiveness.None, "It has no effect." },
        { Effectiveness.NotVery, "It is not very effective.." },
        { Effectiveness.Normal, "" },
        { Effectiveness.Super, "It is super effective!" }
    };

    public static void DisplayDammageEffectiveness(Effectiveness effectiveness, string attackName, string attacker)
    {
        Console.WriteLine($"{attacker} used {attackName}. {messages[effectiveness]}");
    }
    
}