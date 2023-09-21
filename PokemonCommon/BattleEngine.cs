using PokemonCommon.Enums;
using PokemonCommon.Pokemons;
using PokemonCommon.Pokemons.Attacks;
using System.Data;

namespace PokemonCommon;

public static class BattleEngine
{
    public static void BattleSimulation(Pokemon firstMon, Pokemon secondMon)
    {
        
        (Pokemon attacker, Pokemon defender) roles = (firstMon,  secondMon);
        int roundTracker = 1;
        while (firstMon.HealthPoints > 0 && secondMon.HealthPoints > 0)
        {
            

            Console.WriteLine($"->[Round.{roundTracker}: {firstMon.Name} HP: {firstMon.HealthPoints} || {secondMon.Name} HP: {secondMon.HealthPoints}]<-");
            roundTracker++;

            Attack randomAttack = PickRandomAttack(roles.attacker);

            double monHp = roles.defender.HealthPoints;
            MakeAttack(roles.defender, randomAttack, roles.attacker.Name);

            double damage = monHp - roles.defender.HealthPoints;
            

             roles = RoleSwitcher(roles.attacker, roles.defender);
             Console.WriteLine("\n");
        }
        Console.WriteLine($"->[Round.{roundTracker}: {firstMon.Name} HP: {firstMon.HealthPoints} || {secondMon.Name} HP: {secondMon.HealthPoints}]<-");
        if (firstMon.HealthPoints > secondMon.HealthPoints)
        {
            Console.WriteLine($"{firstMon.Name} destroyed {secondMon.Name}");
        }
        else
        {
            Console.WriteLine($"{secondMon.Name} destroyed {firstMon.Name}");
        }

    }

    private static Attack PickRandomAttack(Pokemon attacker)
    {
        int numberOfAttacks = 0;
        foreach (Attack attack in attacker.Attacks)
        {
            if (attack != null)
            {
                numberOfAttacks++;
            }
        }
        Random rnd = new Random();
        int randomAttackIndex = rnd.Next(0, numberOfAttacks);

        return attacker.Attacks[randomAttackIndex];
    }

    private static (Pokemon attacker, Pokemon defender) RoleSwitcher(Pokemon attacker, Pokemon defender)
    {
        (Pokemon attacker, Pokemon defender) roles = (defender, attacker);

        return roles;
    }


    public static void MakeAttack(Pokemon target, Attack attack, string attacker)
    {
        Effectiveness effectiveness = CheckEffectiveness(attack.Type, target.Types.ToArray());

        BattleUi.DisplayDammageEffectiveness(effectiveness, attack.Name, attacker);

        double modifier = (double)effectiveness / 100.0;

        target.HealthPoints -= attack.Damage * modifier;
    }

    public static Effectiveness CheckEffectiveness(PokeTypes attackType, PokeTypes[] targetTypes)
    {
        switch (attackType)
        {
            case PokeTypes.Normal:
                return NormalAttackEffectiveness(targetTypes);
            case PokeTypes.Fire:
                return FireAttackEffectiveness(targetTypes);
            case PokeTypes.Water:
                return WaterAttackEffectiveness(targetTypes);
            case PokeTypes.Grass:
                return GrassAttackEffectiveness(targetTypes);
            case PokeTypes.Electric:
                return ElectricAttackEffectiveness(targetTypes);
            case PokeTypes.Ice:
                return IceAttackEffectiveness(targetTypes);
            case PokeTypes.Fighting:
                return FightingAttackEffectiveness(targetTypes);
            case PokeTypes.Poison:
                return PoisonAttackEffectiveness(targetTypes);
            case PokeTypes.Ground:
                return GroundAttackEffectiveness(targetTypes);
            case PokeTypes.Flying:
                return FlyingAttackEffectiveness(targetTypes);
            case PokeTypes.Psychic:
                return PsychicAttackEffectiveness(targetTypes);
            case PokeTypes.Bug:
                return BugAttackEffectiveness(targetTypes);
            case PokeTypes.Rock:
                return RockAttackEffectiveness(targetTypes);
            case PokeTypes.Ghost:
                return GhostAttackEffectiveness(targetTypes);
            case PokeTypes.Dragon:
                return DragonAttackEffectiveness(targetTypes);
            case PokeTypes.Dark:
                return DarkAttackEffectiveness(targetTypes);
            case PokeTypes.Steel:
                return SteelAttackEffectiveness(targetTypes);
            case PokeTypes.Fairy:
                return FairyAttackEffectiveness(targetTypes);
            default:
                return Effectiveness.Normal;
        }
    }

    #region EffectivenessChecks
    private static Effectiveness FairyAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fighting))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Dark))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness SteelAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Water))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Electric))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Ice))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Fairy))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness DarkAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fighting))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fairy))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Psychic))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness DragonAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fairy))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness GhostAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Normal))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Dark))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Psychic))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness RockAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fighting))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Ground))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ice))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Bug))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness BugAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fighting))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fairy))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Psychic))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Dark))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness PsychicAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Dark))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Psychic))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fighting))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness FlyingAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Electric))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Fighting))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Bug))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness GroundAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Bug))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Electric))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness PoisonAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Ground))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Fairy))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness FightingAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Psychic))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Bug))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fairy))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Normal))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ice))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Dark))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness IceAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Water))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Ice))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ground))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness ElectricAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Ground))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Electric))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Water))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness GrassAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Poison))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Flying))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Bug))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Water))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ground))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness WaterAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Water))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ground))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness FireAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Fire))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Water))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Dragon))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Grass))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Ice))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Bug))
            return Effectiveness.Super;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.Super;

        return Effectiveness.Normal;
    }

    private static Effectiveness NormalAttackEffectiveness(PokeTypes[] targetTypes)
    {
        if (targetTypes.Contains(PokeTypes.Ghost))
            return Effectiveness.None;
        if (targetTypes.Contains(PokeTypes.Rock))
            return Effectiveness.NotVery;
        if (targetTypes.Contains(PokeTypes.Steel))
            return Effectiveness.NotVery;

        return Effectiveness.Normal;
    }

    #endregion
}