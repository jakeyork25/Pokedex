using Pokédex.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokédex.Services
{
    public class PokemonServices
    {
        public List<PokemonModel> GetPokemonNames(string type, int number)
        {
            var pokemonList = new List<PokemonModel>();
            var nameList = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36");
                System.Threading.Thread.Sleep(1000);
                var html = client.GetStringAsync("https://pokemondb.net/type/" + type).Result;
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                for (var i = 1; i < number + 1; i++)
                {
                    var result = doc.DocumentNode.SelectSingleNode("//*[h1]/div[7]/div[" + i + "]/span[2]/a").InnerText;
                    var alolan = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[7]/div[" + i + "]/span[2]/small[1]").InnerText;
                    var firstType = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[7]/div[" + i + "]/span[2]/small/a[1]").InnerText.ToLower();
                    if (nameList.Contains(result) || alolan.Contains("Alolan") || alolan.Contains("Galarian") || alolan.Contains("Mega")) number++;
                    else if (!firstType.Equals(type)) number++;
                    else
                    {
                        var newModel = new PokemonModel();
                        nameList.Add(result);
                        newModel.name = result;
                        pokemonList.Add(newModel);
                    }
                }
            }
            return pokemonList;
        }

        public CssModel GetCssModel(string type)
        {
            var model = new CssModel();
            switch(type)
            {
                case "bug":
                    model.lightColor = "#B4FF01";
                    model.darkColor = "#9FD225";
                    model.dullColor = "#98CE63";
                    break;
                case "electric":
                    model.lightColor = "#DEFF00";
                    model.darkColor = "#C2DA2A";
                    model.dullColor = "#D8E474";
                    break;
                case "fighting":
                    model.lightColor = "#FFB300";
                    model.darkColor = "#DCA932";
                    model.dullColor = "#D1AA2B";
                    break;
                case "fire":
                    model.lightColor = "#FD7600";
                    model.darkColor = "#C03C21";
                    model.dullColor = "#F69E46";
                    break;
                case "flying":
                    model.lightColor = "#996BDF";
                    model.darkColor = "#724DAB";
                    model.dullColor = "#945BE9";
                    break;
                case "ghost":
                    model.lightColor = "#5716B9";
                    model.darkColor = "#47227F";
                    model.dullColor = "#5F4A7F";
                    break;
                case "grass":
                    model.lightColor = "#2BBD14";
                    model.darkColor = "#37792D";
                    model.dullColor = "#69AC6E";
                    break;
                case "ground":
                    model.lightColor = "#8B7341";
                    model.darkColor = "#8B7341";
                    model.dullColor = "#AC9C69";
                    break;
                case "ice":
                    model.lightColor = "#39D9EB";
                    model.darkColor = "#419EA8";
                    model.dullColor = "#69A8AC";
                    break;
                case "normal":
                    model.lightColor = "#97AEB0";
                    model.darkColor = "#65787A";
                    model.dullColor = "#898C97";
                    break;
                case "poison":
                    model.lightColor = "#C51EF7";
                    model.darkColor = "#9A30BA";
                    model.dullColor = "#8F69AC";
                    break;
                case "psychic":
                    model.lightColor = "#FF25EA";
                    model.darkColor = "#D24BC5";
                    model.dullColor = "#C075B9";
                    break;
                case "rock":
                    model.lightColor = "#A9A178";
                    model.darkColor = "#767158";
                    model.dullColor = "#7A7565";
                    break;
                case "water":
                    model.lightColor = "#0033DB";
                    model.darkColor = "#2B428E";
                    model.dullColor = "#6A77B6";
                    break;

            }
            return model;
        }

        public StatModel GetPokemonStats(List<PokemonModel> pokemonList)
        {
            var model = new StatModel();
            var newModelList = new List<PokemonModel>();
            var moveList = new List<string>();
            var abilityList = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36");
                System.Threading.Thread.Sleep(1000);
                foreach (var pokemon in pokemonList)
                {
                    var name = pokemon.name.ToLower();
                    if (name.Equals("mr. mime")) name = "mr-mime";
                    if (name.Contains("♀")) name = name.Replace("♀", "-f");
                    if (name.Contains("♂")) name = name.Replace("♂", "-m");
                    var html = client.GetStringAsync("https://pokemondb.net/pokedex/" + name).Result;
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    var type = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[2]/table/tbody/tr[2]/td/a").InnerText;
                    pokemon.type = char.ToUpper(type[0]) + type.Substring(1);
                    pokemon.number = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[2]/table/tbody/tr[1]/td/strong").InnerText;
                    pokemon.species = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[2]/table/tbody/tr[3]/td").InnerText;
                    var height = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[2]/table/tbody/tr[4]/td").InnerText;
                    height = height.Replace("&#8242;", "'").Replace("&#8243;", "\"").Replace("&nbsp;m", "");
                    pokemon.height = height;
                    pokemon.weight = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[2]/table/tbody/tr[5]/td").InnerText.Replace("&nbsp;", "");
                    var ability = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[2]/table/tbody/tr[6]/td/span/a").InnerText;
                    pokemon.ability = ability;
                    if (!abilityList.Contains(ability)) abilityList.Add(ability);
                    pokemon.evYield = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[3]/div/div[1]/table/tbody/tr[1]/td").InnerText;
                    pokemon.growthRate = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div/div/div[3]/div/div[1]/table/tbody/tr[5]/td").InnerText;

                    var hpStat = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[1]/td[1]").InnerText;
                    pokemon.hpStat = hpStat;
                    var attackStat = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[2]/td[1]").InnerText;
                    pokemon.attackStat = attackStat;
                    var defenseStat = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[3]/td[1]").InnerText;
                    pokemon.defenseStat = defenseStat;
                    var spAtkStat = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[4]/td[1]").InnerText;
                    pokemon.spAtkStat = spAtkStat;
                    var spDefStat = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[5]/td[1]").InnerText;
                    pokemon.spDefStat = spDefStat;
                    var speedStat = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[6]/td[1]").InnerText;
                    pokemon.speedStat = speedStat;

                    pokemon.hpStatMin = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[1]/td[3]").InnerText;
                    pokemon.attackStatMin = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[2]/td[3]").InnerText;
                    pokemon.defenseStatMin = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[3]/td[3]").InnerText;
                    pokemon.spAtkStatMin = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[4]/td[3]").InnerText;
                    pokemon.spDefStatMin = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[5]/td[3]").InnerText;
                    pokemon.speedStatMin = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[6]/td[3]").InnerText;

                    pokemon.hpStatMax = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[1]/td[4]").InnerText;
                    pokemon.attackStatMax = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[2]/td[4]").InnerText;
                    pokemon.defenseStatMax = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[3]/td[4]").InnerText;
                    pokemon.spAtkStatMax = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[4]/td[4]").InnerText;
                    pokemon.spDefStatMax = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[5]/td[4]").InnerText;
                    pokemon.speedStatMax = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[3]/div[2]/div[1]/div[2]/div/div[2]/table/tbody/tr[6]/td[4]").InnerText;

                    pokemon.hpBar = Convert.ToInt32(((Convert.ToDecimal(hpStat) / 180) * 100));
                    pokemon.attackBar = Convert.ToInt32(((Convert.ToDecimal(attackStat) / 180) * 100));
                    pokemon.defenseBar = Convert.ToInt32(((Convert.ToDecimal(defenseStat) / 180) * 100));
                    pokemon.spAtkBar = Convert.ToInt32(((Convert.ToDecimal(spAtkStat) / 180) * 100));
                    pokemon.spDefBar = Convert.ToInt32(((Convert.ToDecimal(spDefStat) / 180) * 100));
                    pokemon.speedBar = Convert.ToInt32(((Convert.ToDecimal(speedStat) / 180) * 100));
                    var typeCap = char.ToUpper(type[0]) + type.Substring(1).ToLower();
                    for(var i = 1; i < 30; i++)
                    {
                        var moveNameNode = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[10]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[" + i + "]/td[2]/a");
                        if (moveNameNode != null)
                        {
                            var moveName = moveNameNode.InnerText;
                            if (moveList.Contains(moveName)) continue;
                            var moveType = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div[10]/div[2]/div[1]/div/div[1]/div[1]/table/tbody/tr[" + i + "]/td[3]/a").InnerText;
                            if (moveType != typeCap) continue;
                            moveList.Add(moveName);
                        } else
                        {
                            break;
                        }          
                    }

                    newModelList.Add(pokemon);
                }
                model.pokemonModelList = newModelList;
                model.moveList = moveList;
                model.abilityList = abilityList;
            }
            return model;
        }

        public StatModel GetModelList(string type, int number)
        {
            var statModel = new StatModel();
            statModel.pokemonModelList = GetPokemonNames(type, number);
            var updatedModel = GetPokemonStats(statModel.pokemonModelList);
            statModel.pokemonModelList = updatedModel.pokemonModelList;
            statModel.moveList = updatedModel.moveList;
            statModel.abilityList = updatedModel.abilityList;
            return statModel;
        }

        public List<string> CreateIdList(int count)
        {
            var idList = new List<string>();
            for(var i = 1; i < count + 1; i++)
            {
                idList.Add("panel-" + i.ToString());
            }
            return idList;
        }

        public PageModel GetPageModel(string type, int number)
        {
            var model = new PageModel();
            var statModel = GetModelList(type, number);
            model.pokemonList = statModel.pokemonModelList;
            model.moveList = statModel.moveList;
            model.abilityList = statModel.abilityList;
            model.cssProperties = GetCssModel(type);
            model.idList = CreateIdList(statModel.pokemonModelList.Count);
            return model;
        }

        public PageModel GetSpecialPageModel()
        {
            var model = new PageModel();
            var dragonModel = GetModelList("dragon", 3);
            var iceModel = GetModelList("ice", 2);
            var ghostModel = GetModelList("ghost", 3);
            model.pokemonList = dragonModel.pokemonModelList;
            model.pokemonList.AddRange(iceModel.pokemonModelList);
            model.pokemonList.AddRange(ghostModel.pokemonModelList);
            model.moveList = dragonModel.moveList;
            model.moveList.AddRange(iceModel.moveList);
            model.moveList.AddRange(ghostModel.moveList);
            model.abilityList = dragonModel.abilityList;
            model.abilityList.AddRange(iceModel.abilityList);
            model.abilityList.AddRange(ghostModel.abilityList);
            var cssModel = new CssModel();
            cssModel.lightColor = "#7A23FD";
            cssModel.darkColor = "#65419B";
            cssModel.dullColor = "#9584AF";
            model.cssProperties = cssModel;
            model.idList = CreateIdList(model.pokemonList.Count);
            return model;
        }
    }
}
