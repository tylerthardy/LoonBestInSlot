﻿using AddonManager.Models;

namespace AddonManager.Models.GuideMappings;

internal class MageFrostMapping : SpecMapping
{
    public override string UrlBase => "https://www.wowhead.com/wotlk/guide/classes/mage/frost/";
    public override string Class => "Mage";
    public override string Spec => "Frost";

    public override string GemsUrl => "dps-enchants-gems-pve";
    public override List<(string, GuideMapping)> Gems => new List<(string, GuideMapping)>
        {
            { ("Meta", "h2#best-gems+.box") },
            { ("Gem", "h2#best-gems+.box~.box") },
            { ("Gem", "h2#best-gems+.box~.box~.box") },
            { ("Gem", "h2#best-gems+.box~.box~.box~.box") },
            { ("Gem", "h2#best-gems+.box~.box~.box~.box~.box") },
            { ("Gem", "h2#best-gems+.box~.box~.box~.box~.box~.box") },

            { ("Head", "h2#best-enchants~.box") },
            { ("Shoulder", "h2#best-enchants~.box~.box") },
            { ("Back", "h2#best-enchants~.box~.box~.box") },
            { ("Chest", "h2#best-enchants~.box~.box~.box~.box") },
            { ("Waist", "h2#best-enchants~.box~.box~.box~.box~.box") },
            { ("Wrist", "h2#best-enchants~.box~.box~.box~.box~.box~.box") },
            { ("Hands", "h2#best-enchants~.box~.box~.box~.box~.box~.box~.box") },
            { ("Legs", "h2#best-enchants~.box~.box~.box~.box~.box~.box~.box~.box") },
            { ("Feet", "h2#best-enchants~.box~.box~.box~.box~.box~.box~.box~.box~.box") },
            { ("Main Hand", "h2#best-enchants~.box~.box~.box~.box~.box~.box~.box~.box~.box~.box") },
            { ("Ring", "h2#best-enchants~.box~.box~.box~.box~.box~.box~.box~.box~.box~.box~.box") },
        };

    public override string Phase0Url => "dps-bis-gear-pre-raid-pve-p3";
    public override List<(string, GuideMapping)> Phase0 => new List<(string, GuideMapping)>
        {
            { ("Head", "#head-for-frost-mage-dps-pre-raid") },
            { ("Shoulder", "#shoulders-for-frost-mage-dps-pre-raid") },
            { ("Back", "#back-for-frost-mage-dps-pre-raid") },
            { ("Chest", "#chest-for-frost-mage-dps-pre-raid") },
            { ("Wrist", "#wrist-for-frost-mage-dps-pre-raid") },
            { ("Hands", "#hands-for-frost-mage-dps-pre-raid") },
            { ("Waist", "#waist-for-frost-mage-dps-pre-raid") },
            { ("Legs", "#legs-for-frost-mage-dps-pre-raid") },
            { ("Feet", "#feet-for-frost-mage-dps-pre-raid") },
            { ("Neck", "#neck-for-frost-mage-dps-pre-raid") },
            { ("Ring", "#rings-for-frost-mage-dps-pre-raid") },
            { ("Trinket", "#trinkets-for-frost-mage-dps-pre-raid") },
            { ("Main Hand", "#main-hand-and-two-handed-weapons-for-frost-mage-dps-pre-raid") },
            { ("Off Hand", "#off-hand-weapons-for-frost-mage-dps-pre-raid") },
            { ("Ranged/Relic", "#wands-for-frost-mage-dps-pre-raid") }
        };

    public override string Phase1Url => "dps-bis-gear-pve-phase-1";
    public override List<(string, GuideMapping)> Phase1 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-and-two-handed-weapons-for-frost-mage-dps-phase-1") },
            { ("Off Hand", "#off-hand-weapons-for-frost-mage-dps-phase-1") },
            { ("Ranged/Relic", "#wands-for-frost-mage-dps-phase-1") }
        };

    public override string Phase2Url => "dps-bis-gear-pve-phase-2";
    public override List<(string, GuideMapping)> Phase2 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-and-two-handed-weapons-for-frost-mage-dps-phase-2") },
            { ("Off Hand", "#off-hand-weapons-for-frost-mage-dps-phase-2") },
            { ("Ranged/Relic", "#wands-for-frost-mage-dps-phase-2") }
        };

    public override string Phase3Url => "dps-bis-gear-pve-phase-3";
    public override List<(string, GuideMapping)> Phase3 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-and-two-handed-weapons-for-frost-mage-dps-phase-3") },
            { ("Off Hand", "#off-hand-weapons-for-frost-mage-dps-phase-3") },
            { ("Ranged/Relic", "#wands-for-frost-mage-dps-phase-3") }
        };

    public override string Phase4Url => "dps-bis-gear-pve-phase-4";
    public override List<(string, GuideMapping)> Phase4 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-and-two-handed-weapons-for-frost-mage-dps-phase-4") },
            { ("Off Hand", "#off-hand-weapons-for-frost-mage-dps-phase-4") },
            { ("Ranged/Relic", "#wands-for-frost-mage-dps-phase-4") }
        };

    public override string Phase5Url => "dps-bis-gear-pve-phase-5";
    public override List<(string, GuideMapping)> Phase5 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-and-two-handed-weapons-for-frost-mage-dps-phase-5") },
            { ("Off Hand", "#off-hand-weapons-for-frost-mage-dps-phase-5") },
            { ("Ranged/Relic", "#wands-for-frost-mage-dps-phase-5") }
        };

}