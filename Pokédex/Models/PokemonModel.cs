using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Pokédex.Models
{
    public class PokemonModel
    {
        public string type { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string species { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string ability { get; set; }
        public string evYield { get; set; }
        public string growthRate { get; set; }


        public string hpStat { get; set; }
        public string attackStat { get; set; }
        public string defenseStat { get; set; }
        public string spAtkStat { get; set; }
        public string spDefStat { get; set; }
        public string speedStat { get; set; }
        public string hpStatMin { get; set; }
        public string attackStatMin { get; set; }
        public string defenseStatMin { get; set; }
        public string spAtkStatMin { get; set; }
        public string spDefStatMin { get; set; }
        public string speedStatMin { get; set; }
        public string hpStatMax { get; set; }
        public string attackStatMax { get; set; }
        public string defenseStatMax { get; set; }
        public string spAtkStatMax { get; set; }
        public string spDefStatMax { get; set; }
        public string speedStatMax { get; set; }

        public int hpBar { get; set; }
        public int attackBar { get; set; }
        public int defenseBar { get; set; }
        public int spAtkBar { get; set; }
        public int spDefBar { get; set; }
        public int speedBar { get; set; }
    }

    public class CssModel
    {
        public string darkColor { get; set; }
        public string lightColor { get; set; }
        public string dullColor { get; set; }
    }

    public class StatModel
    {
        public List<PokemonModel> pokemonModelList { get; set; }
        public List<string> moveList { get; set; }
        public List<string> abilityList { get; set; }
    }
    public class PageModel
    {
        public List<PokemonModel> pokemonList { get; set; }
        public CssModel cssProperties { get; set; }
        public List<string> moveList { get; set; }
        public List<string> abilityList { get; set; }
        public List<string> idList { get; set; }
    }

}
