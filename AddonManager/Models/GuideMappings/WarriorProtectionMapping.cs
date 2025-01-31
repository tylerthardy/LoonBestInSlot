﻿using AddonManager.Models;

namespace AddonManager.Models.GuideMappings;

internal class WarriorProtectionMapping : SpecMapping
{
    public override string UrlBase => "https://www.wowhead.com/cata/guide/classes/warrior/protection/";
    public override string Class => "Warrior";
    public override string Spec => "Protection";

    public override string Phase0Url => "tank-bis-gear-pre-raid";
    public override List<(string, GuideMapping)> Phase0 => new List<(string, GuideMapping)>
        {
            { ("Head", "#head-for-protection-warrior-tank") },
            { ("Shoulder", "#shoulders-for-protection-warrior-tank") },
            { ("Back", "#back-for-protection-warrior-tank") },
            { ("Chest", "#chest-for-protection-warrior-tank") },
            { ("Wrist", "#wrist-for-protection-warrior-tank") },
            { ("Hands", "#hands-for-protection-warrior-tank") },
            { ("Waist", "#waist-for-protection-warrior-tank") },
            { ("Legs", "#legs-for-protection-warrior-tank") },
            { ("Feet", "#feet-for-protection-warrior-tank") },
            { ("Neck", "#neck-for-protection-warrior-tank") },
            { ("Ring", "#rings-for-protection-warrior-tank") },
            { ("Trinket", "#trinkets-for-protection-warrior-tank") },
            { ("Main Hand", "#main-hand-weapons-for-protection-warrior-tank") },
            { ("Off Hand", "#shields-for-protection-warrior-tank") },
            { ("Ranged/Relic", "#ranged-weapons-for-protection-warrior-tank") }
        };

    public override string Phase1Url => "tank-bis-gear-pve";
    public override List<(string, GuideMapping)> Phase1 => new List<(string, GuideMapping)>
        {
            { ("Head", "#head-for-protection-warrior-tank") },
            { ("Shoulder", "#shoulders-for-protection-warrior-tank") },
            { ("Back", "#back-for-protection-warrior-tank") },
            { ("Chest", "#chest-for-protection-warrior-tank") },
            { ("Wrist", "#wrist-for-protection-warrior-tank") },
            { ("Hands", "#hands-for-protection-warrior-tank") },
            { ("Waist", "#waist-for-protection-warrior-tank") },
            { ("Legs", "#legs-for-protection-warrior-tank") },
            { ("Feet", "#feet-for-protection-warrior-tank") },
            { ("Neck", "#neck-for-protection-warrior-tank") },
            { ("Ring", "#rings-for-protection-warrior-tank") },
            { ("Trinket", "#trinkets-for-protection-warrior-tank") },
            { ("Main Hand", "#main-hand-weapons-for-protection-warrior-tank") },
            { ("Off Hand", "#shields-for-protection-warrior-tank") },
            { ("Ranged/Relic", "#ranged-weapons-for-protection-warrior-tank") }
        };

    public override string Phase2Url => "tank-bis-gear-pve-phase-2";
    public override List<(string, GuideMapping)> Phase2 => new List<(string, GuideMapping)>
        {
            { ("Head", "#head-for-protection-warrior-tank-in-phase-2") },
            { ("Shoulder", "#shoulders-for-protection-warrior-tank-in-phase-2") },
            { ("Back", "#back-for-protection-warrior-tank-in-phase-2") },
            { ("Chest", "#chest-for-protection-warrior-tank-in-phase-2") },
            { ("Wrist", "#wrist-for-protection-warrior-tank-in-phase-2") },
            { ("Hands", "#hands-for-protection-warrior-tank-in-phase-2") },
            { ("Waist", "#waist-for-protection-warrior-tank-in-phase-2") },
            { ("Legs", "#legs-for-protection-warrior-tank-in-phase-2") },
            { ("Feet", "#feet-for-protection-warrior-tank-in-phase-2") },
            { ("Neck", "#neck-for-protection-warrior-tank-in-phase-2") },
            { ("Ring", "#rings-for-protection-warrior-tank-in-phase-2") },
            { ("Trinket", "#trinkets-for-protection-warrior-tank-in-phase-2") },
            { ("Main Hand", "#one-handed-weapons-for-protection-warrior-tank-in-phase-2") },
            { ("Off Hand", "#shields-for-protection-warrior-tank-in-phase-2") },
            { ("Ranged/Relic", "#guns-and-bows-for-protection-warrior-tank-in-phase-2") }
        };

    public override string Phase3Url => "tank-bis-gear-pve-phase-3";
    public override List<(string, GuideMapping)> Phase3 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand") },
            { ("Off Hand", "#shield") },
            { ("Ranged/Relic", "#ranged") }
        };

    public override string Phase4Url => "tank-bis-gear-pve-phase-4";
    public override List<(string, GuideMapping)> Phase4 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand") },
            { ("Off Hand", "#shield") },
            { ("Ranged/Relic", "#ranged") }
        };

    public override string Phase5Url => "tank-bis-gear-pve-phase-5";
    public override List<(string, GuideMapping)> Phase5 => new List<(string, GuideMapping)>
        {
            { ("Main Hand", "#main-hand") },
            { ("Off Hand", "#shield") },
            { ("Ranged/Relic", "#ranged") }
        };

    public override string PrePatchUrl => "tank-bis-gear-pve";
    public override List<(string, GuideMapping)> PrePatch => new List<(string, GuideMapping)>
    {
        { ("Head", "#head-for-protection-warrior-tank")},
        { ("Shoulder", "#shoulders-for-protection-warrior-tank") },
        { ("Back", "#back-for-protection-warrior-tank") },
        { ("Chest", "#chest-for-protection-warrior-tank") },
        { ("Wrist", "#wrist-for-protection-warrior-tank") },
        { ("Hands", "#hands-for-protection-warrior-tank") },
        { ("Waist", "#waist-for-protection-warrior-tank") },
        { ("Legs", "#legs-for-protection-warrior-tank") },
        { ("Feet", "#feet-for-protection-warrior-tank") },
        { ("Neck", "#neck-for-protection-warrior-tank") },
        { ("Ring", "#rings-for-protection-warrior-tank") },
        { ("Trinket", "#trinkets-for-protection-warrior-tank") },
        { ("Main Hand", "#main-hand-weapons-for-protection-warrior-tank") },
        { ("Off Hand", "#shields-for-protection-warrior-tank") },
        { ("Ranged/Relic", "#ranged-weapons-for-protection-warrior-tank") },
    };

    public override string GemsEnchantsUrl => "tank-enchants-gems-pve";
    public override List<(string, GuideMapping)> GemsEnchants => new List<(string, GuideMapping)>
    {
        //Gems
        { ("Meta", ".box:nth-of-type(1)") },
        { ("Gem", ".box:nth-of-type(2)") },
        { ("Gem", ".box:nth-of-type(3)") },
        { ("Gem", ".box:nth-of-type(4)") },
        //Enchants
        { ("Head", ".box:nth-of-type(6)") },
        { ("Shoulder", ".box:nth-of-type(7)") },
        { ("Back", ".box:nth-of-type(8)") },
        { ("Chest", ".box:nth-of-type(9)") },
        { ("Wrist", ".box:nth-of-type(10)") },
        { ("Hands", ".box:nth-of-type(11)") },
        { ("Waist", ".box:nth-of-type(12)") },
        { ("Legs", ".box:nth-of-type(13)") },
        { ("Feet", ".box:nth-of-type(14)") },
        { ("Ring", ".box:nth-of-type(15)") },
        { ("Main Hand", ".box:nth-of-type(16)") },
        { ("Off Hand", ".box:nth-of-type(17)") },
        { ("Ranged/Relic", ".box:nth-of-type(18)") },
    };
}