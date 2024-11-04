using GDWeave;

namespace NapQOL;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        if (Config.TotalInventoryValue)
        {
            modInterface.RegisterScriptMod(new TotalInventoryValue());
            modInterface.Logger.Information("[NapQOL] TotalInventoryValue loaded!");
        }
        //if (Config.GamblingTeleport)
        //{
        //    modInterface.RegisterScriptMod(new GamblingTeleport());
        //    modInterface.Logger.Information("[NapQOL] GamblingTeleport loaded!");
        //}
        if (Config.FasterReel)
        {
            modInterface.RegisterScriptMod(new FasterReel());
            modInterface.Logger.Information("[NapQOL] FasterReel loaded!");
        }
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
