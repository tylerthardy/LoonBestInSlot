﻿using AddonManager.Models;

namespace AddonManager.Models.GuideMappings;

internal class DruidRestoMapping : SpecMapping
{
    public override string UrlBase => "https://www.wowhead.com/cata/guide/classes/druid/restoration/";
    public override string Class => "Druid";
    public override string Spec => "Restoration";

    public override string Phase0Url => "healer-bis-gear-pre-raid";
    public override List<(string, GuideMapping)> Phase0 => new List<(string, GuideMapping)>
        {
            { ("Head", "#head-for-restoration-druid-healer") },
            { ("Shoulder", "#shoulders-for-restoration-druid-healer") },
            { ("Back", "#back-for-restoration-druid-healer") },
            { ("Chest", "#chest-for-restoration-druid-healer") },
            { ("Wrist", "#wrist-for-restoration-druid-healer") },
            { ("Hands", "#hands-for-restoration-druid-healer") },
            { ("Waist", "#waist-for-restoration-druid-healer") },
            { ("Legs", "#legs-for-restoration-druid-healer") },
            { ("Feet", "#feet-for-restoration-druid-healer") },
            { ("Neck", "#neck-for-restoration-druid-healer") },
            { ("Ring", "#rings-for-restoration-druid-healer") },
            { ("Trinket", "#trinkets-for-restoration-druid-healer") },
            { ("Main Hand", "#main-hand-weapons-for-restoration-druid-healer") },
            { ("Two Hand", "#staff-weapons-for-restoration-druid-healer") },
            { ("Off Hand", "#off-hands-for-restoration-druid-healer") },
            { ("Ranged/Relic", "#relics-for-restoration-druid-healer") }
        };

    public override string Phase1Url => "healer-bis-gear-pve";
    public override List<(string, GuideMapping)> Phase1 => new List<(string, GuideMapping)>
        {
            { ("Head", "#head-for-restoration-druid-healer") },
            { ("Shoulder", "#shoulders-for-restoration-druid-healer") },
            { ("Back", "#back-for-restoration-druid-healer") },
            { ("Chest", "#chest-for-restoration-druid-healer") },
            { ("Wrist", "#wrist-for-restoration-druid-healer") },
            { ("Hands", "#hands-for-restoration-druid-healer") },
            { ("Waist", "#waist-for-restoration-druid-healer") },
            { ("Legs", "#legs-for-restoration-druid-healer") },
            { ("Feet", "#feet-for-restoration-druid-healer") },
            { ("Neck", "#neck-for-restoration-druid-healer") },
            { ("Ring", "#rings-for-restoration-druid-healer") },
            { ("Trinket", "#trinkets-for-restoration-druid-healer") },
            { ("Main Hand", "#main-hand-weapons-for-restoration-druid-healer") },
            { ("Two Hand", "#staff-weapons-for-restoration-druid-healer") },
            { ("Off Hand", "#off-hands-for-restoration-druid-healer") },
            { ("Ranged/Relic", "#relics-for-restoration-druid-healer") }
        };

    public override string Phase2Url => "healer-bis-gear-pve-phase-2";
    public override List<(string, GuideMapping)> Phase2 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-weapons-for-restoration-druid-healer-phase-2") },
            { ("Two Hand", "#two-handed-weapons-for-restoration-druid-healer-phase-2") },
            { ("Off Hand", "#off-hand-weapons-for-restoration-druid-healer-phase-2") },
            { ("Ranged/Relic", "#idols-for-restoration-druid-healer-phase-2") }
        };

    public override string Phase3Url => "healer-bis-gear-pve-phase-3";
    public override List<(string, GuideMapping)> Phase3 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-weapons-for-restoration-druid-phase-3") },
            { ("Two Hand", "#two-handed-weapons-for-restoration-druid-healer-phase-3") },
            { ("Off Hand", "#off-hand-weapons-for-restoration-druid-healer-phase-3") },
            { ("Ranged/Relic", "#idols-for-restoration-druid-healer-phase-3") }
        };

    public override string Phase4Url => "healer-bis-gear-pve-phase-4";
    public override List<(string, GuideMapping)> Phase4 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-weapons-for-restoration-druid-healer-phase-4") },
            { ("Two Hand", "#two-handed-weapons-for-restoration-druid-healer-phase-4") },
            { ("Off Hand", "#off-hand-weapons-for-restoration-druid-healer-phase-4") },
            { ("Ranged/Relic", "#idols-for-restoration-druid-healer-phase-4") }
        };

    public override string Phase5Url => "healer-bis-gear-pve-phase-5";
    public override List<(string, GuideMapping)> Phase5 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand-weapons-for-restoration-druid-healer-phase-5") },
            { ("Two Hand", "#two-handed-weapons-for-restoration-druid-healer-phase-5") },
            { ("Off Hand", "#off-hand-weapons-for-restoration-druid-healer-phase-5") },
            { ("Ranged/Relic", "#idols-for-restoration-druid-healer-phase-5") }
        };

    public override string PrePatchUrl => "healer-bis-gear-pve";
    public override List<(string, GuideMapping)> PrePatch => new List<(string, GuideMapping)>
    {
        { ("Main Hand", "#main-hand-weapons-for-restoration-druid-healer")},
        { ("Off Hand", "#off-hand-weapons-for-restoration-druid-healer") },
        { ("Two Hand", "#two-handed-weapons-for-restoration-druid-healer") },
        { ("Ranged/Relic", "#idols-for-restoration-druid-healer") },
    };

    public override string GemsEnchantsUrl => "healer-enchants-gems-pve";

    public override List<(string, GuideMapping)> GemsEnchants => new List<(string, GuideMapping)>
    {
        //Gems
        { ("Meta", ".box:nth-of-type(1)") },
        { ("Gem", ".box:nth-of-type(2)") },
        { ("Gem", ".box:nth-of-type(3)") },
        { ("Gem", ".box:nth-of-type(4)") },

        { ("Head", ".box:nth-of-type(5)") },
        { ("Shoulder", ".box:nth-of-type(6)") },
        { ("Back", ".box:nth-of-type(7)") },
        { ("Chest", ".box:nth-of-type(8)") },
        { ("Wrist", ".box:nth-of-type(9)") },
        { ("Hands", ".box:nth-of-type(10)") },
        { ("Waist", ".box:nth-of-type(11)") },
        { ("Legs", ".box:nth-of-type(12)") },
        { ("Feet", ".box:nth-of-type(13)") },
        { ("Main Hand", ".box:nth-of-type(14)") },
        { ("Off Hand", ".box:nth-of-type(15)") },
        { ("Ring", ".box:nth-of-type(16)") },
    };
}