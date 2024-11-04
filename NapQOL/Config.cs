using System.Text.Json.Serialization;

namespace NapQOL;

public class Config {
    [JsonInclude] public bool TotalInventoryValue = true;
    [JsonInclude] public bool FasterFishing = true;
    //[JsonInclude] public bool GamblingTeleport = true;
}
