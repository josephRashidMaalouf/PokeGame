using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonCommon.Enums;

namespace PokemonCommon.Pokemons.Attacks
{
    internal class FreezeDry : Attack
    {
        public FreezeDry() : base(70, "Freeze-Dry", PokeTypes.Ice)
        {
        }
    }
}
