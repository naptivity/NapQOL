using System.Text.Json.Serialization;

namespace NapQOL;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
