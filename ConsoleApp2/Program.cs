using PokemonCommon;
using PokemonCommon.Characters;
using PokemonCommon.Enums;
using PokemonCommon.Pokemons;
using PokemonCommon.Pokemons.Attacks;


Trainer ash = new Trainer("Ash");

Pokemon sobble = new Pokemon("Sobble", PokeTypes.Water);
Pokemon charmander = new Pokemon("Charmander", PokeTypes.Fire);

Ember ember = new Ember();
charmander.LearnAttack(ember, 0);

WaterGun waterGun = new WaterGun();
sobble.LearnAttack(waterGun, 0);

Console.WriteLine("--------------------------");

Console.WriteLine(sobble.HealthPoints);

BattleEngine.MakeAttack(sobble, charmander.Attacks[0]);

Console.WriteLine(sobble.HealthPoints);