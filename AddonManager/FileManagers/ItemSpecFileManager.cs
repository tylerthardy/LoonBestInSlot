﻿using AddonManager.Models;
using AngleSharp.Dom;

namespace AddonManager.FileManagers;

public static class ItemSpecFileManager
{
    public static void WriteItemSpec(string path, string className, string specName, 
        List<GemSpec> gems, 
        List<EnchantSpec> enchants,
        Dictionary<int, List<ItemSpec>> itemsList)
    {
        var itemSB = new StringBuilder();

        itemSB.AppendLine($"local spec0 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"0\")");
        itemSB.AppendLine($"local spec1 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"1\")");
        // itemSB.AppendLine($"local spec2 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"2\")");
        // itemSB.AppendLine($"local spec3 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"3\")");
        // itemSB.AppendLine($"local spec4 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"4\")");
        // itemSB.AppendLine($"local spec5 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"5\")");
        //itemSB.AppendLine($"local spec99 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"99\")");

        itemSB.AppendLine();
        gems.Sort();
        foreach (var gem in gems)
        {
            string specString = $"spec{gem.Phase}";

            itemSB.AppendLine($"LBIS:AddGem({specString}, \"{gem.GemId}\", \"{gem.Quality}\", \"{gem.IsMeta.ToString()}\") --{gem.Name}");
        }

        itemSB.AppendLine();
        enchants.Sort();
        foreach (var enchant in enchants)
        {
            itemSB.AppendLine($"LBIS:AddEnchant(spec{itemsList.Keys.Max()}, \"{enchant.EnchantId}\", LBIS.L[\"{enchant.Slot}\"]) --{enchant.Name}");
        }
        foreach (var phaseItems in itemsList)
        {
            itemSB.AppendLine();
            var items = phaseItems.Value;
            items.Sort();

            foreach (var item in items)
            {
                itemSB.AppendLine($"LBIS:AddItem(spec{phaseItems.Key}, \"{item.ItemId}\", LBIS.L[\"{item.Slot}\"], \"{item.BisStatus}\") --{item.Name}");
            }
        }

        System.IO.File.WriteAllText(path, itemSB.ToString());
    }

    public static Tuple<List<GemSpec>, List<EnchantSpec>, Dictionary<int, List<ItemSpec>>> ReadGuide(string path)
    {
        var gems = new List<GemSpec>();
        var enchants = new List<EnchantSpec>();
        var items = new Dictionary<int, List<ItemSpec>>();

        string[] itemSpecLines = System.IO.File.ReadAllLines(path);

        int count = 0;        
        foreach (var itemSpecLine in itemSpecLines)
        {
            if (itemSpecLine.Contains("local spec"))
            {
                continue;
            }

            if (itemSpecLine.Contains("LBIS:AddGem(spec"))
            {
                var itemSplit = itemSpecLine.Replace("LBIS:AddGem(spec", "").Trim().Split('"');

                var gemId = Int32.Parse(itemSplit[1]);
                gems.Add(new GemSpec
                {
                    GemId = gemId,
                    Name = itemSplit[6].Replace(") --", ""),
                    Phase = Int32.Parse(itemSplit[0].Replace(", ", "")),
                    Quality = Int32.Parse(itemSplit[3]),
                    IsMeta = bool.Parse(itemSplit[5])
                });
            }

            if (itemSpecLine.Contains("LBIS:AddEnchant(spec"))
            {
                var itemSplit = itemSpecLine.Replace("LBIS:AddEnchant(spec", "").Trim().Split('"');

                var enchantId = Int32.Parse(itemSplit[1]);
                var slot = itemSplit[3];
                enchants.Add(new EnchantSpec
                {
                    EnchantId = enchantId,
                    Name = itemSplit[4].Replace("]) --", ""),
                    Slot = slot                    
                });
            }
            
            if (itemSpecLine.Contains("LBIS:AddItem(spec"))
            {
                var itemSplit = itemSpecLine.Replace("LBIS:AddItem(spec", "").Trim().Split('"');

                var itemId = Int32.Parse(itemSplit[1]);
                var phase = Int32.Parse(itemSplit[0].Replace(", ", ""));

                if (!items.ContainsKey(phase))
                    items.Add(phase, new List<ItemSpec>());

                items[phase].Add(new ItemSpec
                {
                    ItemId = itemId,
                    Slot = itemSplit[3],
                    Name = itemSplit[6].Replace(") --", ""),
                    BisStatus = itemSplit[5],
                    ItemOrder = count
                });
                count++;
            }
        }

        return new Tuple<List<GemSpec>, List<EnchantSpec>, Dictionary<int, List<ItemSpec>>>(gems, enchants, items);

    }
}
