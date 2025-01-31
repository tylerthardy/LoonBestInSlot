﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddonManager.Models.GuideMappings;
internal abstract class SpecMapping
{
    public abstract string UrlBase { get; }
    public abstract string Class { get; }
    public abstract string Spec { get; }

    public abstract string GemsEnchantsUrl { get; }
    public abstract List<(string, GuideMapping)> GemsEnchants { get; }

    public abstract string PrePatchUrl {get; }    
    public abstract List<(string, GuideMapping)> PrePatch { get; }
    public abstract string Phase0Url { get; }
    public abstract List<(string, GuideMapping)> Phase0 { get; }
    public abstract string Phase1Url { get; }
    public abstract List<(string, GuideMapping)> Phase1 { get; }
    public abstract string Phase2Url { get; }
    public abstract List<(string, GuideMapping)> Phase2 { get; }
    public abstract string Phase3Url { get; }
    public abstract List<(string, GuideMapping)> Phase3 { get; }
    public abstract string Phase4Url { get; }
    public abstract List<(string, GuideMapping)> Phase4 { get; }
    public abstract string Phase5Url { get; }
    public abstract List<(string, GuideMapping)> Phase5 { get; }

    public List<ClassGuideMapping> ToList()
    {
        var list = new List<ClassGuideMapping>();

        list.Add(new ClassGuideMapping(UrlBase + GemsEnchantsUrl, Spec, Class, "GemsEnchants", GemsEnchants));

        list.Add(new ClassGuideMapping(UrlBase + Phase0Url, Spec, Class, "Phase0", Phase0));

        list.Add(new ClassGuideMapping(UrlBase + Phase1Url, Spec, Class, "Phase1", Phase1));

        // list.Add(new ClassGuideMapping(UrlBase + Phase2Url, Spec, Class, "Phase2", Phase2));

        // list.Add(new ClassGuideMapping(UrlBase + Phase3Url, Spec, Class, "Phase3", Phase3));

        // list.Add(new ClassGuideMapping(UrlBase + Phase4Url, Spec, Class, "Phase4", Phase4));

        //list.Add(new ClassGuideMapping(UrlBase + PrePatchUrl, Spec, Class, "PrePatch", PrePatch));

        return list;
    }
}
