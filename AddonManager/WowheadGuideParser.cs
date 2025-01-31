﻿using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Xml.Serialization;
using AddonManager.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace AddonManager;

public class WowheadGuideParser
{
    private static readonly string[] excludedItemNames = { "of Shadow Wrath", "of Healing", "of Nature's Wrath", "of Nature Protection",
                                                            "of the Tiger", "of Agility", "of the Squire", "Stolen Silver", "Rocket Fuel Leak" };

    private Random _rand = new Random(DateTime.Now.Millisecond);

    private Dictionary<int, int> _gemSwaps = new Dictionary<int, int>()
    {
        { 4013, 40133 },
        { 32206, 68778 },
    };

    private Dictionary<int, int> _gemPhases = new Dictionary<int, int>()
    {
        { 40112, 3 },
        { 40113, 3 },
        { 40114, 3 },
        { 40119, 3 },
        { 40123, 3 },
        { 40125, 3 },
        { 40126, 3 },
        { 40128, 3 },
        { 40129, 3 },
        { 40133, 3 },
        { 40141, 3 },
        { 40148, 3 },
        { 40150, 3 },
        { 40153, 3 },
        { 40155, 3 },
        { 40157, 3 },
        { 40159, 3 },
        { 40162, 3 },
        { 40166, 3 },
        { 40167, 3 },
        { 45880, 3 },
    };

    private Dictionary<int, int> _enchantSwaps = new Dictionary<int, int>()
    {
        { 2892, 2823}, //Deadly Poison
        { 6947, 8679 }, //Instant Poison
        { 38967, 44529 }, //Enchant Gloves - Major Agility
        { 39300, 52639 }, //Spring Loaded Cloak Expander
        { 44457, 60663 }, //Enchant Cloak - Major Agility
        { 52743, 74189 }, //Enchant Boots - Earthen Vitality
        { 52749, 74198 }, //Enchant Gloves - Haste
        { 52757, 74213 }, //Enchant Boots - Major Agility
        { 52768, 74235 }, //Enchant Off-Hand - Superior Intellect
        { 52769, 74236 }, //Enchant Boots - Precision
        { 52773, 74240 }, //Enchant Cloak - Greater Intellect
        { 52774, 74242 }, //Enchant Weapon - Power Torrent
        { 52776, 74246 }, //Enchant Weapon - Landslide
        { 52777, 74247 }, //Enchant Cloak - Greater Critical Strike
        { 52779, 74250 }, //Enchant Chest - Peerless Stats
        { 52780, 74251 }, //Enchant Chest - Greater Stamina
        { 52781, 74252 }, //Enchant Boots - Assassin's Step
        { 52782, 74253 }, //Enchant Boots - Lavawalker
        { 52784, 74255 }, //Enchant Gloves - Greater Mastery
        { 54448, 75152 }, //Powerful Enchanted Spellthread
        { 54450, 75150 }, //Powerful Ghostly Spellthread
        { 55054, 76168 }, //Ebonsteel Belt Buckle
        { 56517, 78166 }, //Heavy Savage Armor Kit
        { 56550, 78171 }, //Dragonscale Leg Armor
        { 59595, 81933 }, //R19 Threatfinder
        { 62333, 86854 }, //Greater Inscription of Unbreakable Quartz
        { 62343, 86899 }, //Greater Inscription of Charged Lodestone
        { 62345, 86901 }, //Greater Inscription of Jagged Stone
        { 62346, 86907 }, //Greater Inscription of Shattered Crystal
        { 62366, 86931 }, //Arcanum of the Earthen Ring
        { 62367, 86932 }, //Arcanum of Hyjal
        { 62368, 86933 }, //Arcanum of the Dragonmaw
        { 62369, 86934 }, //Arcanum of the Ramkahen
        { 62422, 86933 }, //Arcanum of the Wildhammer
        { 68134, 95471 }, //Enchant 2H Weapon - Mighty Agility
        { 68784, 96264 }, //Enchant Bracer - Agility
        { 68786, 96262 }, //Enchant Bracer - Mighty Intellect
        { 71720, 101598 }, //Drakehide Leg Armor
        { 59594, 81932 }, //Gnomish X-Ray Scope
        { 41111, 55002 }, //Flexweave Underlay
        { 41118, 55016 }, //Nitro Boosts
        { 52750, 74199 }, //Enchant Boots - Haste
        { 38956, 44494 }, //Enchant Cloak - Superior Nature Resistance
        { 44947, 62256 }, //Enchant Bracer - Major Stamina
        { 56551, 78172 }, //Charscale Leg Armor
        { 56502, 78169 }, //Scorched Leg Armor
        { 52744, 74191 }, //Enchant Chest - Mighty Stats
        { 44815, 44575 }, //Enchant Bracer - Greater Assault
        { 52760, 74223 }, //Enchant Weapon - Hurricane
        { 44465, 60692 }, //Enchant Chest - Powerful Stats
        { 52785, 74256 }, //Enchant Bracer - Greater Speed
        { 52766, 74232 }, //Enchant Bracer - Precision
        { 52687, 74132 }, //Enchant Gloves - Mastery
        { 44493, 59621 }, //Enchant Weapon - Berserking
        { 52772, 74239 }, //Enchant Bracer - Greater Expertise
        { 68763, 86933 }, //Arcanum of the Dragonmaw
        { 68716, 86901 }, //Greater Inscription of Jagged Stone
     };

    private Dictionary<int, string> _itemSwaps = new Dictionary<int, string>()
    {
        { 5000, "11994" }, //Coral Band
        { 58186, "56310" }, //Skullcracker Ring
        { 68712, "62464,62469"}, //Impatience of Youth
        { 68710, "62465,62470" }, //Stump of Time
        { 68709, "62463,62468" }, //Unsolvable Riddle
    };

    private Dictionary<string, string> _altModifierTextSwaps = new Dictionary<string, string>()
    {
        { "stam", "Stam" },
        { "mitigation", "Mit" },
        { "def", "Mit" },
        { "armor", "Mit" },
        { "dodge", "Mit" },
        { "parry", "Mit" },
        { "threat", "Thrt" },
        { "ffb", "FFB" },
        { "melee", "Melee" },
        { "ranged", "Ranged" }
    };

    private Dictionary<string, string> _altModifierNotSwaps = new Dictionary<string, string>()
    {
        { "armor", "armor pen" },
    };

    private Dictionary<int, int> _duplicateItemIds = new Dictionary<int, int>() 
    {
        {213087, 213088},
        {213088, 213087}
    };

    private List<string> _bisTextSwaps = new()
    {
        "bis",
        "recommended",
        "recommended",
        "best in slot",
        "best"
    };
    private List<string> _altTextSwaps = new()
    {
        "prebis",
        "tbc",
        "pre-raid",
        "pre-bis",
        "phase 1",
        "p1",
        "phase 2",
        "p2",
        "phase 3",
        "p3",
        "phase 4",
        "p4",
        "alt",
        "10-man",
        "10 man",
        "potentially bis"
    };

    private class SlotSwaps
    {
        private Dictionary<string, string> _slotSwaps = new Dictionary<string, string>()
        {
            { "Helm", "Head" },
            { "Boots", "Feet" },
            { "Belt", "Waist" },
            { "Finger", "Ring" },
            { "Bracers", "Wrist" },
            { "Shoulders", "Shoulder" },
            { "Cloak", "Back" },
            { "Main-Hand", "Main Hand" },
            { "Main-Hand Weapon", "Main Hand" },
            { "Off-Hand Weapon", "Off Hand" },
            { "Off-Hand weapon", "Off Hand" },
            { "Off-Hand", "Off Hand" },
            { "Shield", "Off Hand" },
            { "Weapon", "Two Hand" },
            { "Two-Hand Weapon", "Two Hand" },
            { "Two Hand Weapon", "Two Hand" },
            { "Ranged Weapon", "Ranged/Relic" },
            { "Sigil", "Ranged/Relic" },
            { "Relic", "Ranged/Relic" },
            { "Libram", "Ranged/Relic" },
            { "Idol", "Ranged/Relic" },
            { "Wand", "Ranged/Relic" },
            { "Ranged", "Ranged/Relic" },
            { "Trinket - Throughput", "Trinket" },
            { "Trinket - Sustain", "Trinket" },
            { "Feet - Alternative", "Feet" },
            { "Legs - Alternative", "Feet" }
        };
        // Setting up indexers
        public string this[string i]
        {
            // get indexer allows square brackets to read data
            get
            {
                if (this._slotSwaps.ContainsKey(i))
                    return _slotSwaps[i];
                return i;
            }
        }
    }

    class MyFormatter : IMarkupFormatter
    {
        public string CloseTag(IElement element, bool selfClosing)
        {

            string closeTag = HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing);

            if (closeTag == "</tr>" ||
                closeTag == "</table>")
                closeTag += "\n";

            return closeTag;
        }
        public string Comment(IComment comment) => HtmlMarkupFormatter.Instance.Comment(comment);
        public string Doctype(IDocumentType doctype) => HtmlMarkupFormatter.Instance.Doctype(doctype);
        public string LiteralText(ICharacterData text) => HtmlMarkupFormatter.Instance.LiteralText(text);
        public string OpenTag(IElement element, bool selfClosing) => HtmlMarkupFormatter.Instance.OpenTag(element, selfClosing);
        public string Processing(IProcessingInstruction processing) => HtmlMarkupFormatter.Instance.Processing(processing);
        public string Text(ICharacterData text) => text.Data;
    }

public (Dictionary<int, GemSpec>, Dictionary<int, EnchantSpec>, Dictionary<int, ItemSpec>) ParseWowheadGuide(ClassGuideMapping classGuide, IHtmlDocument doc, Action<string> logFunc)
    {
        var items = new Dictionary<int, ItemSpec>();
        var enchants = new Dictionary<int, EnchantSpec>();
        var gems = new Dictionary<int, GemSpec>();

        bool enchantsAndGems = int.Parse(classGuide.Phase.Replace("Phase", "")) == Constants.CurrentPhase;

        LoopThroughMappings(doc, classGuide, 
            (enchantAnchor, slot) =>
            {
                if (enchantsAndGems)
                    ParseEnchant(enchantAnchor, slot, enchants);
            },
            (table, slot, htmlId) =>
            {
                bool first = true;
                LoopThroughTable(table, async (tableRow, itemChild, itemOrderIndex, isTierList) =>
                {
                    string htmlBisText = string.Empty, rankText = string.Empty;
                    if (isTierList)
                        rankText = tableRow?.ChildNodes[1].TextContent.Trim() ?? string.Empty;
                    htmlBisText = tableRow?.ChildNodes[0].TextContent.Trim() ?? string.Empty;
                    var bisStatus = GetBisStatus(htmlBisText, rankText, isTierList, first, classGuide.Phase);
                    if (itemChild != null)
                    {
                        ParseItemCell(itemChild, bisStatus, GetSlot(slot, htmlBisText, itemChild), items, itemOrderIndex);
                        if (enchantsAndGems)
                            ParseGemCell(tableRow, gems, logFunc);
                    }
                    first = false;
                });
            });

        return (gems, enchants, items);
    }

    private void ParseGemCell(INode? tableRow, Dictionary<int, GemSpec> gems, Action<string> logFunc)
    {
        var gemCell = tableRow?.ChildNodes[2];
        if (gemCell != null)
        {
            Common.RecursiveBoxSearch((IElement)gemCell, (anchorElement) => 
            {
                if (anchorElement.PathName.Contains("/item="))
                {
                    var item = anchorElement.PathName.Replace("/wotlk", "").Replace("/cata/", "/").Replace("/item=", "");
                    var itemIdIndex = item.IndexOf("/");
                    if (itemIdIndex == -1)
                        itemIdIndex = item.IndexOf("&");
                    if (itemIdIndex != -1)
                        item = item.Substring(0, itemIdIndex);
                    var gemId = Int32.Parse(item);
                    if (_gemSwaps.ContainsKey(gemId))
                    {
                        gemId = _gemSwaps[gemId];
                    }
                    if (!gems.ContainsKey(gemId))
                    {
                        gems.Add(gemId, new GemSpec {
                            GemId = gemId,
                            Phase = 0
                        });
                    }
                }
                return false;
            });
        }
    }

    private void ParseEnchant(IHtmlAnchorElement enchantAnchor, string slot, Dictionary<int, EnchantSpec> enchants)
    {
        bool isSpell = false;
        if (enchantAnchor.PathName.Contains("/item="))
            isSpell = false;
        else if (enchantAnchor.PathName.Contains("/spell="))
            isSpell = true;
        else
            return;

        var item = enchantAnchor.PathName.Replace("/wotlk", "").Replace("/cata/", "/").Replace("/item=", "").Replace("/spell=", "");
        var itemIdIndex = item.IndexOf("/");
        if (itemIdIndex == -1)
            itemIdIndex = item.IndexOf("&");

        if (itemIdIndex > -1)
        {
            item = item.Substring(0, itemIdIndex);
            var itemName = enchantAnchor.TextContent.Trim();
            var itemId = Int32.Parse(item);
            bool skippedItem = false;
            foreach (var excludedName in excludedItemNames)
                if (itemName.EndsWith(excludedName))
                    skippedItem = true;
            if (!skippedItem)
            {
                var textureId = "";
                if (isSpell == false && _enchantSwaps.ContainsKey(itemId))
                {
                    textureId = itemId.ToString();
                    itemId = _enchantSwaps[itemId];
                }
                if (!enchants.ContainsKey(itemId))
                {
                    enchants.Add(itemId, new EnchantSpec
                    {
                        EnchantId = itemId,
                        Name = itemName ?? "unknown",
                        Slot = slot,
                        TextureId = textureId
                    });
                }
                else
                {
                    var slotList = enchants[itemId].Slot.Split("~").ToList();
                    slotList.Add(slot);
                    enchants[itemId].Slot = string.Join("~", slotList.Distinct());
                }
            }
        }
    }

    internal (Dictionary<int, GemSpec>, Dictionary<int, EnchantSpec>) ParseGemEnchantsWowheadGuide(ClassGuideMapping classGuide, IHtmlDocument doc)
    {
        var gems = new Dictionary<int, GemSpec>();
        var enchants = new Dictionary<int, EnchantSpec>();

        foreach (var heading in classGuide.GuideMappings)
        {
            foreach (var htmlMapping in heading.Value.SlotHtmlId.Split(";"))
            {
                var headerElement = doc.QuerySelector(htmlMapping);
                if (headerElement != null)
                {
                    if (heading.Key == "Meta" || heading.Key == "Gem")
                    {
                        ParseGems(headerElement, heading.Key, gems);
                    }
                    else
                    {
                        ParseEnchants(headerElement, heading.Key, enchants);
                    }
                }
            }
        }

        return (gems, enchants);

    }

    private void ParseGems(IElement gemBox, string gemType, Dictionary<int, GemSpec> gems)
    {
        Common.RecursiveBoxSearch(gemBox, (anchorElement) => 
        {
            if (anchorElement.PathName.Contains("/item="))
            {
                var item = anchorElement.PathName.Replace("/wotlk", "").Replace("/cata/", "/").Replace("/item=", "");

                var itemIdIndex = item.IndexOf("/");
                if (itemIdIndex == -1)
                    itemIdIndex = item.IndexOf("&");
                if (itemIdIndex != -1)
                    item = item.Substring(0, itemIdIndex);

                var gemId = Int32.Parse(item);

                if (_gemSwaps.ContainsKey(gemId))
                {
                    gemId = _gemSwaps[gemId];
                }

                if (!gems.ContainsKey(gemId))
                {
                    int itemQuality = 0;
                    if (anchorElement.ClassName?.Contains("q1") ?? false)
                        itemQuality = 1;
                    else if (anchorElement.ClassName?.Contains("q2") ?? false)
                        itemQuality = 2;
                    else if (anchorElement.ClassName?.Contains("q3") ?? false)
                        itemQuality = 3;
                    else if (anchorElement.ClassName?.Contains("q4") ?? false)
                        itemQuality = 4;

                    gems.Add(gemId, new GemSpec
                    {
                        GemId = gemId,
                        Name = anchorElement.TextContent.Trim() ?? "unknown",
                        Phase = _gemPhases.ContainsKey(gemId) ? _gemPhases[gemId] : 1,
                        Quality = itemQuality,
                        IsMeta = gemType == "Meta"
                    });
                }
            }
            return false;
        });
    }

    private void ParseEnchants(IElement enchantBox, string slot, Dictionary<int, EnchantSpec> enchants)
    {   
        Common.RecursiveBoxSearch(enchantBox, (enchantAnchor) => 
        {
            bool isSpell = false;
            if (enchantAnchor.PathName.Contains("/item="))
                isSpell = false;
            else if (enchantAnchor.PathName.Contains("/spell="))
                isSpell = true;
            else
                return false;

            var item = enchantAnchor.PathName.Replace("/wotlk", "").Replace("/cata/", "/").Replace("/item=", "").Replace("/spell=", "");
            var itemIdIndex = item.IndexOf("/");
            if (itemIdIndex == -1)
                itemIdIndex = item.IndexOf("&");

            if (itemIdIndex > -1)
            {
                item = item.Substring(0, itemIdIndex);
                var itemName = enchantAnchor.TextContent.Trim();
                var itemId = Int32.Parse(item);

                bool skippedItem = false;
                foreach (var excludedName in excludedItemNames)
                    if (itemName.EndsWith(excludedName))
                        skippedItem = true;

                if (!skippedItem)
                {
                    var textureId = "";
                    if (isSpell == false && _enchantSwaps.ContainsKey(itemId))
                    {
                        textureId = itemId.ToString();
                        itemId = _enchantSwaps[itemId];
                    } 
                    else if (isSpell == false)
                    {
                        throw new Exception($"Couldn't find spell for enchant: {itemName}");
                    }

                    if (!enchants.ContainsKey(itemId))
                    {
                        enchants.Add(itemId, new EnchantSpec
                        {
                            EnchantId = itemId,
                            Name = itemName ?? "unknown",
                            Slot = slot,
                            TextureId = textureId
                        });
                    }
                    else
                    {
                        var slotList = enchants[itemId].Slot.Split("~").ToList();
                        slotList.Add(slot);
                        enchants[itemId].Slot = string.Join("~", slotList.Distinct());
                    }
                }
            }
            return false;
        });
    }

    private string GetSlot(string slot, string bisStatus, IElement itemChild)
    {
        if (slot == "Main Hand" && bisStatus.ToUpper().Contains("OH") && !bisStatus.Contains("MH"))
            return "Off Hand";
        else if (slot == "Main Hand" && bisStatus.ToUpper().Contains("2H") && !bisStatus.Contains("MH") || itemChild.TextContent.Contains("Staff"))
            return "Two Hand";

        return slot;
    }

    private string GetBisStatus(string htmlBisText, string rankText, bool isTierList, bool first, string phase)
    {

        var bisText = string.Empty;
        if (first)
            bisText = "BIS";
        else if (isTierList)
        {
            bisText = rankText.Contains("S") ? "BIS" : "Alt";
        }
        else
        {
            if (_altTextSwaps.Any((s) =>
            {
                if (phase == "Phase1" && (s.ToLower() == "phase 1" || s.ToLower() == "p1"))
                    return false;
                if (phase == "Phase2" && (s.ToLower() == "phase 2" || s.ToLower() == "p2"))
                    return false;
                else if (phase == "Phase3" && (s.ToLower() == "phase 3" || s.ToLower() == "p3"))
                    return false;
                else if (phase == "Phase4" && (s.ToLower() == "phase 4" || s.ToLower() == "p4"))
                    return false;

                return htmlBisText?.ToLower().Contains(s) ?? false;
            }))
            {
                bisText = "Alt";
            }
            else
            {
                bisText = _bisTextSwaps.Any(s => htmlBisText?.ToLower().Contains(s) ?? false) ? "BIS" : "Alt";
            }
        }

        var altText = string.Empty;
        foreach (var tankSwap in _altModifierTextSwaps)
            if ((!htmlBisText?.ToLower().Contains("no") ?? false) &&
                (htmlBisText?.ToLower().Contains(tankSwap.Key) ?? false))
            {
                if (!_altModifierNotSwaps.ContainsKey(tankSwap.Key) ||
                    (!htmlBisText?.ToLower().Contains(_altModifierNotSwaps[tankSwap.Key]) ?? false))
                {
                    altText = $" {tankSwap.Value}";
                    break;
                }
            }
        return bisText.Trim() + altText;
    }

    private List<int> ParseItemCell(IElement itemChild, string bisStatus, string slot, Dictionary<int, ItemSpec> items, int itemOrderIndex)
    {
        bool foundAnchor = false;

        List<int> itemIds = new List<int>();
        Common.RecursiveBoxSearch(itemChild, (child) =>
        {
            foundAnchor = true;
            bool foundItem = false;

            if (child.PathName.Contains("/item="))
            {
                var item = child.PathName.Replace("/wotlk", "").Replace("/cata/", "/").Replace("/item=", "");

                var itemIdIndex = item.IndexOf("/");
                if (itemIdIndex == -1)
                    itemIdIndex = item.IndexOf("&");
                if (itemIdIndex != -1)
                    item = item.Substring(0, itemIdIndex);
                    
                var itemName = child.TextContent.Trim();

                bool skippedItem = false;
                foreach (var excludedName in excludedItemNames)
                    if ((child.NextSibling?.TextContent.Trim().EndsWith(excludedName) ?? false) ||
                        (child.NextSibling?.NextSibling?.TextContent.Trim().EndsWith(excludedName) ?? false) ||
                        itemName.EndsWith(excludedName))
                        skippedItem = true;

                if (!skippedItem)
                {
                    int guideItemId = -99999;
                    Int32.TryParse(item, out guideItemId);
                    List<int> guideItemIds = new List<int> { guideItemId };

                    if (_itemSwaps.ContainsKey(guideItemId))
                    {
                        guideItemIds = _itemSwaps[guideItemId].Split(',').Select(i => int.Parse(i.Trim())).ToList();
                    }

                    foreach(var itemId in guideItemIds)
                    {
                        if (!items.ContainsKey(itemId))
                        {
                            items.Add(itemId, new ItemSpec
                            {
                                ItemId = itemId,
                                Name = itemName ?? "unknown",
                                BisStatus = bisStatus ?? "unknown",
                                Slot = slot,
                                ItemOrder = itemOrderIndex
                            });
                            if (_duplicateItemIds.ContainsKey(itemId) && !items.ContainsKey(_duplicateItemIds[itemId]))
                            {
                                items.Add(_duplicateItemIds[itemId], new ItemSpec
                                {
                                    ItemId = _duplicateItemIds[itemId],
                                    Name = itemName ?? "unknown",
                                    BisStatus = bisStatus ?? "unknown",
                                    Slot = slot,
                                    ItemOrder = itemOrderIndex
                                });
                            }
                        }
                        else
                        {
                            if (!items[itemId].Slot.Contains(slot))
                            {
                                items[itemId].Slot = $"{items[itemId].Slot}~{slot}";
                                if (items[itemId].BisStatus != bisStatus)
                                    items[itemId].BisStatus = $"{items[itemId].BisStatus}/{bisStatus}";
                            }
                        }
                        itemIds.Add(itemId);
                    }
                }
            }
            return foundItem;
        });
        if (!foundAnchor)
        {
            int itemId = -1 * _rand.Next(10000, 99999);
            items.Add(itemId, new ItemSpec
            {
                ItemId = itemId,
                Name = "unknown",
                BisStatus = "unknown",
                Slot = slot,
                ItemOrder = itemOrderIndex
            });
            itemIds.Add(itemId);
        }
        return itemIds;
    }

    private void LoopThroughTable(IHtmlTableElement? table, Action<INode, IElement?, int, bool> action)
    {
        var itemOrderIndex = 0;
        var firstRow = false;
        var tableRows = table?.FirstChild?.ChildNodes;
        if (tableRows != null)
        {
            bool isTierList = false;
            foreach (var tableRow in tableRows)
            {
                var tierlistNumber = 0;
                if (!firstRow || tableRow.NodeName != "TR")
                {
                    if (tableRow.ChildNodes[0].TextContent.Contains("Rank"))
                    {   
                        tierlistNumber = 0;
                    }
                    if (tableRow.ChildNodes[1].TextContent.Contains("Rank"))
                    {
                        tierlistNumber = 1;
                    }
                    firstRow = true;
                    continue;
                }

                IElement? itemChild = null;

                for(int i = tierlistNumber + 1; i < tableRow.ChildNodes.Length; i++)
                {
                    var rowChild = tableRow.ChildNodes[i];
                    if (rowChild.NodeType == NodeType.Element)
                    {
                        if (rowChild.ChildNodes.Any(n => n.NodeName == "A" && ((IHtmlAnchorElement)n).PathName.Contains("/item=")))
                        {
                            itemChild = (IElement)rowChild;
                            break;
                        }
                    }
                }

                action(tableRow, itemChild, itemOrderIndex, isTierList);

                itemOrderIndex++;
            }
        }
    }

    private void LoopThroughMappings(IHtmlDocument doc, ClassGuideMapping specMapping, Action<IHtmlAnchorElement, string> foundEnchant, Action<IHtmlTableElement?, string, string> foundTable)
    {
        foreach (var guideMapping in specMapping.GuideMappings)
        {
            bool foundEnchantText = false;
            foreach (var htmlMapping in guideMapping.Value.SlotHtmlId.Split(";"))
            {
                var headerElement = doc.QuerySelector(htmlMapping);
                if (headerElement != null)
                {
                    var nextSibling = headerElement.NextSibling;
                    int elementCounter = 0;
                    while (nextSibling != null && (nextSibling is not IHtmlTableElement || nextSibling is IHtmlHeadingElement))
                    {
                        if (Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended.*for new").Success)
                            foundEnchantText = false;

                        //try to find enchant.
                        if (nextSibling is IHtmlAnchorElement && foundEnchantText)
                        {
                            foundEnchant(nextSibling as IHtmlAnchorElement, guideMapping.Key);
                        }

                        if (Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*enchant").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*enchants").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*armor").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*scope").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*inscription").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*tinker").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended bis.*runeforge").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "recommended shield enchant").Success ||
                            Regex.Match(nextSibling.TextContent.Trim().ToLower(), "bis.*enchant").Success)
                        {
                            foundEnchantText = true;
                        }

                        if (foundEnchantText)
                        {
                            Common.RecursiveBoxSearch(nextSibling, (anchorElement) => 
                            {
                                if (anchorElement != null)
                                    foundEnchant(anchorElement, guideMapping.Key);
                                return false;
                            });
                        }

                        nextSibling = nextSibling?.NextSibling;
                        elementCounter++;
                    }

                    foundEnchantText = false;
                    if (nextSibling is IHtmlTableElement)
                    {                            
                        foundTable(nextSibling as IHtmlTableElement, guideMapping.Key, htmlMapping);
                    }
                    else
                    {
                        throw new ParseException($"Failed to find table for {htmlMapping} after {elementCounter} hops");
                    }
                }
                else
                {
                    throw new ParseException($"Failed to find {htmlMapping}");
                }
            }
        }
    }
}