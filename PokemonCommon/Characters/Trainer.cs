using PokemonCommon.Pokemons;

namespace PokemonCommon.Characters
{
    public class Trainer
    {
        public string Name { get; set; }
        public List<Pokemon> PokemonCollection { get; set; } = new List<Pokemon>();

        public Trainer(string name)
        {
            Name = name;
        }

        public void Catch(Pokemon pokemon)
        {
            PokemonCollection.Add(pokemon);
        }

        public void Release(Pokemon pokemon)
        {
            PokemonCollection.Remove(pokemon);
        }
    }
}
