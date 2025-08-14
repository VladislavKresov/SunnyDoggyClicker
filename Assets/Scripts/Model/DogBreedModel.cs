using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class DogBreed {
    public string Id;
    public string Type;
    public string Name;
    public string Description;
    public bool Hypoallergenic;
    public LifeSpan Life;
    public Weight MaleWeight;
    public Weight FemaleWeight;

    public class LifeSpan {
        public int Min;
        public int Max;
    }

    public class Weight {
        public int Min;
        public int Max;
    }

    public static DogBreed GetBreedFromJson(string json) {
        var jObject = JObject.Parse(json);
        var data = jObject["data"];
        var attributes = data["attributes"];

        return new DogBreed {
            Id = (string)data["id"],
            Type = (string)data["type"],
            Name = (string)attributes["name"],
            Description = (string)attributes["description"],
            Hypoallergenic = (bool)attributes["hypoallergenic"],
            Life = new DogBreed.LifeSpan {
                Min = (int)attributes["life"]["min"],
                Max = (int)attributes["life"]["max"]
            },
            MaleWeight = new DogBreed.Weight {
                Min = (int)attributes["male_weight"]["min"],
                Max = (int)attributes["male_weight"]["max"]
            },
            FemaleWeight = new DogBreed.Weight {
                Min = (int)attributes["female_weight"]["min"],
                Max = (int)attributes["female_weight"]["max"]
            }
        };
    }

    public static DogBreed[] GetBreedsFromJson(string json) {
        var breedsList = new List<DogBreed>();
        var root = JObject.Parse(json);
        var dataArray = (JArray)root["data"];

        foreach (var item in dataArray) {
            var breed = new DogBreed {
                Id = (string)item["id"],
                Type = (string)item["type"],
                Name = (string)item["attributes"]["name"],
                Description = (string)item["attributes"]["description"],
                Hypoallergenic = (bool)item["attributes"]["hypoallergenic"],
                Life = new LifeSpan {
                    Min = (int)item["attributes"]["life"]["min"],
                    Max = (int)item["attributes"]["life"]["max"]
                },
                MaleWeight = new Weight {
                    Min = (int)item["attributes"]["male_weight"]["min"],
                    Max = (int)item["attributes"]["male_weight"]["max"]
                },
                FemaleWeight = new Weight {
                    Min = (int)item["attributes"]["female_weight"]["min"],
                    Max = (int)item["attributes"]["female_weight"]["max"]
                }
            };

            breedsList.Add(breed);
        }

        return breedsList.ToArray();
    }
}