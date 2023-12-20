﻿using AddonManager.Models;
using AngleSharp.Dom;

namespace AddonManager.FileManagers;

public static class ItemSpecFileManager
{
    public static void WriteItemSpec(string path, string className, string specName,
        Dictionary<int, List<EnchantSpec>> enchantsList,
        Dictionary<int, List<ItemSpec>> itemsList)
    {
        var itemSB = new StringBuilder();

        itemSB.AppendLine($"local spec1 = LBIS:RegisterSpec(LBIS.L[\"{className}\"], LBIS.L[\"{specName}\"], \"1\")");

        foreach (var phaseEnchants in enchantsList)
        {   
            itemSB.AppendLine();
            var enchants = phaseEnchants.Value;

            int count = 0;
            foreach (var enchant in enchants)
            {    
                itemSB.AppendLine($"LBIS:AddEnchant(spec{phaseEnchants.Key}, \"{enchant.EnchantId}\", LBIS.L[\"{enchant.Slot}\"]) --{enchant.Name}");
            }
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

    public static Tuple<Dictionary<int, List<EnchantSpec>>, Dictionary<int, List<ItemSpec>>> ReadGuide(string path)
    {
        var enchants = new Dictionary<int, List<EnchantSpec>>();
        var items = new Dictionary<int, List<ItemSpec>>();

        if (!System.IO.File.Exists(path))
            return new Tuple<Dictionary<int, List<EnchantSpec>>, Dictionary<int, List<ItemSpec>>>(
                new Dictionary<int, List<EnchantSpec>>(), 
                new Dictionary<int, List<ItemSpec>>()); 

        string[] itemSpecLines = System.IO.File.ReadAllLines(path);

        foreach (var itemSpecLine in itemSpecLines)
        {
            if (itemSpecLine.Contains("local spec"))
            {
                continue;
            }

            if (itemSpecLine.Contains("LBIS:AddEnchant(spec"))
            {
                var itemSplit = itemSpecLine.Replace("LBIS:AddEnchant(spec", "").Trim().Split('"');

                var enchantId = Int32.Parse(itemSplit[1]);
                var phase = Int32.Parse(itemSplit[0].Replace(", ", ""));

                if (!enchants.ContainsKey(phase))
                    enchants.Add(phase, new List<EnchantSpec>());

                var slot = itemSplit[3];
                enchants[phase].Add(new EnchantSpec
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
                    BisStatus = itemSplit[5]
                });
            }
        }

        return new Tuple<Dictionary<int, List<EnchantSpec>>, Dictionary<int, List<ItemSpec>>>(enchants, items);

    }
}
