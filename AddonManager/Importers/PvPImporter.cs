﻿using AddonManager.Models;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using static System.Net.WebRequestMethods;

namespace AddonManager.Importers;

public class PvPImporter : LootImporter
{
    private Dictionary<string, string> wowheadUriList = new Dictionary<string, string>
    {
        { @"https://www.wowhead.com/classic/npc=12799/sergeant-basha#sells", "H" },
        { @"https://www.wowhead.com/classic/npc=12805/officer-areyn#sells", "A" },
        { @"https://www.wowhead.com/classic/npc=14754/kelm-hargunth#sells", "H"},
        { @"https://www.wowhead.com/classic/npc=14753/illiyana-moonblaze#sells", "A"}
    };

    internal override string FileName { get => "PvPItemList"; }

    internal override async Task<DatabaseItems> InnerConvert(DatabaseItems items, Action<string> writeToLog)
    {
        items.Items.Clear();

        var total = wowheadUriList.Count;
        var count = 0;
        await Common.LoadFromWebPages(wowheadUriList.Keys.ToList(), async (uri, content) =>
        {
            writeToLog($"Reading from {uri} ({++count}/{total})");
            var parser = new HtmlParser();
            var doc = default(IHtmlDocument);
            doc = await parser.ParseDocumentAsync(content);

            Common.ReadWowheadSellsList(doc, uri, (uri, row, itemId, item) =>
            {
                var currencySource = "PvP Vendor";
                var currencyNumber = "";
                var currencySourceLocation = "Unknown Rank";
                var itemName = item.TextContent;

                Int32.TryParse(row.Children[3].TextContent, out int itemLevel);

                // var nameSplit = wowheadUriList[uri].Item1.Split(",");
                // var levelSplit = nameSplit.Select(n => n.Split('>'));

                // if (!levelSplit.Any(i => itemName.Contains(i[0].Trim()) && (i.Length < 2 || Int32.Parse(i[1]) < itemLevel)))
                //     return;

                foreach(var currency in row.Children[10].Children)
                {
                    if (currency.ClassName == "moneygold") 
                    {
                        currencyNumber += $"{currency.TextContent.Trim()}g";
                    } 
                    else if (currency.ClassName == "moneysilver")
                    {
                        currencyNumber += $"{currency.TextContent.Trim()}s";
                    } 
                    else if (currency.ClassName == "moneycopper") 
                    {
                        currencyNumber += $"{currency.TextContent.Trim()}c";
                    }
                }

                if (!items.Items.ContainsKey(itemId))
                {                   
                    var successfulAdd = items.Items.TryAdd(itemId, new DatabaseItem
                    {
                        Name = itemName,
                        SourceNumber = currencyNumber,
                        Source = currencySource,
                        SourceLocation = currencySourceLocation,
                        SourceType = "PvP"
                    });
                }
            });
        });

        return items;
    }
}
