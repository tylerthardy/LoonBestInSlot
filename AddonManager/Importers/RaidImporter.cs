﻿using AddonManager.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using static System.Net.WebRequestMethods;

namespace AddonManager.Importers;

public class RaidImporter : LootImporter
{
    private Dictionary<string, string> wowheadUriList = new Dictionary<string, string>
    {
        //Throne of the Four Winds
        // { @"https://www.wowhead.com/cata/npc=45871/nezir#drops;mode:n10", "Conclave of Wind, Throne of the Four Winds" },
        // { @"https://www.wowhead.com/cata/npc=45871/nezir#drops;mode:h10", "Conclave of Wind, Throne of the Four Winds (Heroic)" },
        // { @"https://www.wowhead.com/cata/npc=46753/alakir#drops;mode:n10", "Al'Akir, Throne of the Four Winds" },
        // { @"https://www.wowhead.com/cata/npc=46753/alakir#drops;mode:h10", "Al'Akir, Throne of the Four Winds (Heroic)" },

        // //Blackwing Descent
        // { @"https://www.wowhead.com/cata/npc=41570/magmaw#drops;mode:n10", "Magmaw, Blackwing Descent"},
        // { @"https://www.wowhead.com/cata/npc=41570/magmaw#drops;mode:h10", "Magmaw, Blackwing Descent (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=42166/arcanotron#drops;mode:n10", "Arcanotron, Blackwing Descent"},
        // { @"https://www.wowhead.com/cata/npc=42166/arcanotron#drops;mode:h10", "Arcanotron, Blackwing Descent (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=41378/maloriak#drops;mode:n10", "Maloriak, Blackwing Descent"},
        // { @"https://www.wowhead.com/cata/npc=41378/maloriak#drops;mode:h10", "Maloriak, Blackwing Descent (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=41442/atramedes#drops;mode:n10", "Atramedes, Blackwing Descent"},
        // { @"https://www.wowhead.com/cata/npc=41442/atramedes#drops;mode:h10", "Atramedes, Blackwing Descent (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=43296/chimaeron#drops;mode:n10", "Chimaeron, Blackwing Descent"},
        // { @"https://www.wowhead.com/cata/npc=43296/chimaeron#drops;mode:h10", "Chimaeron, Blackwing Descent (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=41376/nefarian#drops;mode:n10", "Nefarian, Blackwing Descent"},
        // { @"https://www.wowhead.com/cata/npc=41376/nefarian#drops;mode:h10", "Nefarian, Blackwing Descent (Heroic)"},

        // //Bastion of Twilight
        // { @"https://www.wowhead.com/cata/npc=44600/halfus-wyrmbreaker#drops;mode:n10", "Halfus Wyrmbreaker, The Bastion of Twilight"},
        // { @"https://www.wowhead.com/cata/npc=44600/halfus-wyrmbreaker#drops;mode:h10", "Halfus Wyrmbreaker, The Bastion of Twilight (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=45992/valiona#drops;mode:n10", "Valiona & Theralion, The Bastion of Twilight"},
        // { @"https://www.wowhead.com/cata/npc=45992/valion#drops;mode:h10", "Valiona & Theralion, The Bastion of Twilight (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=43687/feludius#drops;mode:n10", "Ascendant Council, The Bastion of Twilight"},
        // { @"https://www.wowhead.com/cata/npc=43687/feludius#drops;mode:h10", "Ascendant Council, The Bastion of Twilight (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=43324/chogall#drops;mode:n10", "Cho'gall, The Bastion of Twilight"},
        // { @"https://www.wowhead.com/cata/npc=43324/chogall#drops;mode:h10", "Cho'gall, The Bastion of Twilight (Heroic)"},
        // { @"https://www.wowhead.com/cata/npc=45213/sinestra#drops;mode:h10", "Sinestra, The Bastion of Twilight (Heroic)"},

        //Baradin Hold
        { @"https://www.wowhead.com/cata/npc=47120/argaloth#drops;mode:n10", "Argaloth, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/argaloth#drops;mode:n10;50", "Argaloth, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=47120/argaloth#drops;mode:n10;100", "Argaloth, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=52363/occuthar#drops;mode:n10", "Occu'thar, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/occuthar#drops;mode:n10;50", "Occu'thar, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/occuthar#drops;mode:n10;100", "Occu'thar, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/occuthar#drops;mode:n10;150", "Occu'thar, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/alizabal#drops;mode:n10", "Alizabal, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/alizabal#drops;mode:n10;50", "Alizabal, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/alizabal#drops;mode:n10;100", "Alizabal, Baradin Hold"},
        { @"https://www.wowhead.com/cata/npc=55869/alizabal#drops;mode:n10;150", "Alizabal, Baradin Hold"},


    };
    private Dictionary<string, string> wowheadContainsUriList = new Dictionary<string, string>
    {
        //{ @"https://www.wowhead.com/cata/object=194201/rare-cache-of-winter", "Hodir, Ulduar (25)" },
    };

    private Dictionary<int, DatabaseItem> trashDrops = new Dictionary<int, DatabaseItem>()
    {
    };

    private List<string> excludedWords = new List<string>()
    {
        "Reins of the",
        "Plans: ",
        "Pattern: ", 
        "Formula: ",
        "Trophy of the Crusade",
        "Large Satchel",
        "Dragon Hide Bag",
        "Shadowfrost Shard"
    };

    public RaidImporter(CancellationToken cancellationToken) : base(cancellationToken)
    {
    }

    internal override string FileName { get => "RaidItemList"; }
    internal override async Task<DatabaseItems> InnerConvert(DatabaseItems items, Action<string> writeToLog)
    {
        //items.Items.Clear();

        await Common.ReadWowheadDropsList(wowheadUriList.Keys.ToList(), (webAddress, row, itemId, item) =>
        {
            Int32.TryParse(row.Children[4].TextContent, out int itemLevel);
            InternalItemsParse(wowheadUriList, webAddress, row, itemId, itemLevel, item, items);
        }, writeToLog, _importCancelToken);

        await Common.ReadWowheadContainsList(wowheadContainsUriList.Keys.ToList(), (webAddress, row, itemId, item) =>
        {
            Int32.TryParse(row.Children[3].TextContent, out int itemLevel);
            InternalItemsParse(wowheadContainsUriList, webAddress, row, itemId, itemLevel, item, items);
        }, writeToLog, _importCancelToken);

        foreach (var trashDrop in trashDrops)
        {
            items.AddItem(trashDrop.Key, trashDrop.Value);
        }

        return items;
    }

    private void InternalItemsParse(Dictionary<string, string> uriList, string webAddress, IElement row, int itemId, int itemLevel, IElement item, DatabaseItems items)
    {
        var itemName = item.TextContent;
        var isPurple = (item.ClassName?.Contains("q4") ?? false) || (item.ClassName?.Contains("q5") ?? false);
        if (!isPurple) return;
        if (excludedWords.Any(w => itemName.Contains(w))) return;

        var sourceFaction = "B";
        if (row.Children[7].Children.Count() > 0)
        {
            var factionColumn = (IElement)row.Children[7].ChildNodes[0];
            if (factionColumn?.ClassName == "icon-horde")
                sourceFaction = "H";
            else if (factionColumn?.ClassName == "icon-alliance")
                sourceFaction = "A";
        }

        var sourceSplit = uriList[webAddress].Split(",");
        var sourceName = sourceSplit[0].Trim();

        items.AddItem(itemId, new DatabaseItem
        {
            Name = itemName,
            SourceNumber = "0",
            Source = sourceName,
            SourceLocation = sourceSplit[1].Trim(),
            SourceType = "Drop",
            SourceFaction = sourceFaction
        });
    }

    private IHtmlAnchorElement? RecursivelyFindFirstAnchor(IElement element)
    {
        IHtmlAnchorElement? result = null;
        if (element is IHtmlAnchorElement && element.ClassName != "toggler-off")
            result = element as IHtmlAnchorElement;
        else
        {
            foreach (var child in element.Children)
            {
                if (result == null)
                    result = RecursivelyFindFirstAnchor(child);
            }
        }
        return result;
    }
}
