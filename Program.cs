using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace PokemonApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/");

            Console.WriteLine("Nome do Pokémon: ");
            string pokemon = Console.ReadLine();

            var request = new RestRequest("pokemon/" + pokemon, DataFormat.Json);

            var response = client.Get(request);

            var jObject = JObject.Parse(response.Content);

            string pokemonName = (string)jObject.GetValue("name");
            int pokemonId = (int)jObject.GetValue("id");
            double pokemonAltura = (double)jObject.GetValue("height");
            double pokemonPeso = (double)jObject.GetValue("weight");
            List<string> pokemonTipos = new List<string>();

            JArray tipos = (JArray)jObject["types"];

            foreach (var item in tipos)
            {
                pokemonTipos.Add((string)item.SelectToken("type.name"));
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Nome: " + pokemonName);
            Console.WriteLine("Pokédex Id: " + pokemonId);
            Console.Write("Tipos: ");
            
            foreach (var item in pokemonTipos)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Altura: " + pokemonAltura / 10 + " m");
            Console.WriteLine("Peso: " + pokemonPeso / 10 + " kg");
            Console.WriteLine("------------------------------------------------------------");
            
        }
    }
}
