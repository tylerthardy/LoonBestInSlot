﻿using AddonManager.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace AddonManager.Importers;

public class DungeonImporter : LootImporter
{
    private Dictionary<string, string> dungeonUriList = new Dictionary<string, string>
        {
            { @"https://www.wowhead.com/classic/guide/ragefire-chasm-dungeon-strategy-wow-classic", "Ragefire Chasm" },
            { @"https://www.wowhead.com/classic/guide/wailing-caverns-dungeon-strategy-wow-classic", "Wailing Caverns" },
            { @"https://www.wowhead.com/classic/guide/deadmines-dungeon-strategy-wow-classic", "The Deadmines" },
            { @"https://www.wowhead.com/classic/guide/shadowfang-keep-dungeon-strategy-wow-classic", "Shadowfang Keep" },
            { @"https://www.wowhead.com/classic/guide/the-stockade-dungeon-strategy-wow-classic", "The Stockade" },
            { @"https://www.wowhead.com/classic/guide/razorfen-kraul-dungeon-strategy-wow-classic", "Razorfen Kraul" },
            { @"https://www.wowhead.com/classic/guide/scarlet-monastery-dungeon-strategy-wow-classic", "Scarlet Monastery" },
            { @"https://www.wowhead.com/classic/guide/razorfen-downs-dungeon-strategy-wow-classic", "Razorfen Downs" },
            { @"https://www.wowhead.com/classic/guide/uldaman-dungeon-strategy-wow-classic", "Uldaman"},
            { @"https://www.wowhead.com/classic/guide/zulfarrak-dungeon-strategy-wow-classic", "Zul'Farrak"}
        };

    internal override string FileName { get => "DungeonItemList"; }
    internal override async Task<DatabaseItems> InnerConvert(DatabaseItems items, Action<string> writeToLog)
    {
        items.Items.Clear();

        await Common.LoadFromWebPages(dungeonUriList.Keys.ToList(), (uri, doc) =>
        {
            var bossElements = doc.QuerySelectorAll("h3 .q10");
            
            foreach (var bossElement in bossElements)
            {
                var bossName = bossElement.TextContent;
                foreach(var replaceString in _bossNameRemovals)
                    bossName = bossName.Replace(replaceString, "");

                var element = bossElement.ParentElement;                
                while(element.ParentElement != null && element.ParentElement.Id != "guide-body")
                    element = element.ParentElement;

                AddLootItems(element, dungeonUriList[uri], bossName, items);
            }
        }, writeToLog);
        return items;
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

    private List<string> _bossNameRemovals = new List<string> 
    {
        " <Scarlet Champion>",
        " (Bonus Boss)"
    };

    private void AddLootItems(IElement htmlElement, string dungeonName, string bossName, DatabaseItems items)
    {
        var foundLoot = false;   
        IElement element = htmlElement;
        while (!foundLoot)
        {
            foundLoot = true;

            if (element == null)
                return;

            if (element.TextContent.Contains("Loot"))
            {
                foreach(var lootElement in element.NextElementSibling.ChildNodes)
                {
                    var anchor = RecursivelyFindFirstAnchor(lootElement as IElement);

                    if (anchor != null)
                    {
                        var sourceFaction = "B";
                        if (element.TextContent.Contains("Horde"))
                            sourceFaction = "H";
                        else if (element.TextContent.Contains("Alliance"))
                            sourceFaction = "A";

                        var item = anchor.PathName.Replace("/classic", "").Replace("/item=", "");

                        var itemIdIndex = item.IndexOf("/");
                        if (itemIdIndex == -1)
                            itemIdIndex = item.IndexOf("&");

                        int itemId = -999;
                        item = item.Substring(0, itemIdIndex);
                        int.TryParse(item, out itemId);
                        var name = anchor.TextContent.Trim();

                        items.AddItem(itemId, new DatabaseItem 
                        {
                            Name = name,
                            Source = bossName,
                            SourceType = "Drop",
                            SourceNumber = "0",
                            SourceLocation = dungeonName,
                            SourceFaction = sourceFaction
                        });
                    } else {
                        Console.WriteLine($"anchor is null for {bossName}");
                    }
                }
            }
            else
                foundLoot = false;

            if (!foundLoot)
                element = element.NextElementSibling;
        }
    }
}
