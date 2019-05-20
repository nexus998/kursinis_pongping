using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PongPing
{
    public class SkinSelector
    {
        //the STL list which contains all preset skins.
        public static List<Skin> skins = new List<Skin>();
        //instantiates all preset skins.
        public void AddNewSkins()
        {
            skins.Add(new Skin("Common White", Color.white));
            skins.Add(new Skin("Common Green", Color.green));
            skins.Add(new Skin("Deep Blue", Color.blue));
            skins.Add(new Skin("Epic Yellow", Color.yellow, addonModule: new EpicPlatform()));
            skins.Add(new Skin("Rare Red", Color.red));
            skins.Add(new Skin("Legendary Purple", Color.magenta, addonModule: new LegendaryPlatform()));
            skins.Add(new Skin("Random Colors", Color.white, addonModule: new RainbowPlatform()));
        }
    }
    public class Skin
    {
        private readonly string name;
        private Color color;
        private readonly ParticleSystem particleSystem;
        private readonly PlatformAddon addonModule;

        public string GetName() => name;
        public Color GetColor() => color;
        public ParticleSystem GetParticleSystem() => particleSystem;
        public PlatformAddon GetAddonModule() { if (addonModule != null) return addonModule; return null; }

        public Skin(string name, Color color, ParticleSystem particleSystem = null, PlatformAddon addonModule = null)
        {
            this.name = name;
            this.color = color;
            if (particleSystem) this.particleSystem = particleSystem;
            if (addonModule != null)
            {
                this.addonModule = addonModule;
            }
        }
    }
}