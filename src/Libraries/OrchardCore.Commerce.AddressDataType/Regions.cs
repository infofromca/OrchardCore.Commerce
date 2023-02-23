using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OrchardCore.Commerce.AddressDataType;

public static class Regions
{
    /// <summary>
    /// Gets the list of regions.
    /// </summary>
    public static IList<Region> All { get; } =
        CultureInfo
            .GetCultures(CultureTypes.SpecificCultures)
            .Select(culture =>
            {
                // This sometimes throws "CultureNotFoundException: Culture is not supported." exception on Linux only.
                try { return new RegionInfo(culture.LCID); } // #spell-check-ignore-line
                catch (CultureNotFoundException) { return null; }
            })
            .Where(region =>
                region != null &&
                region.TwoLetterISORegionName.Length == 2 && // Filter out world and other 3-digit regions.
                !string.IsNullOrEmpty(region.EnglishName))
            .Distinct()
            .Select(region => new Region(region))
            .ToList();

    /// <summary>
    /// Gets two-letter regions codes mapped to region names.
    /// </summary>
    public static IDictionary<string, IDictionary<string, string>> Provinces { get; } =
        new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["US"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["AL"] = "Alabama",
                ["AK"] = "Alaska",
                ["AZ"] = "Arizona",
                ["AR"] = "Arkansas",
                ["CA"] = "California",
                ["CO"] = "Colorado",
                ["CT"] = "Connecticut",
                ["DE"] = "Delaware",
                ["DC"] = "District Of Columbia",
                ["FL"] = "Florida",
                ["GA"] = "Georgia",
                ["HI"] = "Hawaii",
                ["ID"] = "Idaho",
                ["IL"] = "Illinois",
                ["IN"] = "Indiana",
                ["IA"] = "Iowa",
                ["KS"] = "Kansas",
                ["KY"] = "Kentucky",
                ["LA"] = "Louisiana",
                ["ME"] = "Maine",
                ["MD"] = "Maryland",
                ["MA"] = "Massachusetts",
                ["MI"] = "Michigan",
                ["MN"] = "Minnesota",
                ["MS"] = "Mississippi",
                ["MO"] = "Missouri",
                ["MT"] = "Montana",
                ["NE"] = "Nebraska",
                ["NV"] = "Nevada",
                ["NH"] = "New Hampshire",
                ["NJ"] = "New Jersey",
                ["NM"] = "New Mexico",
                ["NY"] = "New York",
                ["NC"] = "North Carolina",
                ["ND"] = "North Dakota",
                ["OH"] = "Ohio",
                ["OK"] = "Oklahoma",
                ["OR"] = "Oregon",
                ["PA"] = "Pennsylvania",
                ["RI"] = "Rhode Island",
                ["SC"] = "South Carolina",
                ["SD"] = "South Dakota",
                ["TN"] = "Tennessee",
                ["TX"] = "Texas",
                ["UT"] = "Utah",
                ["VT"] = "Vermont",
                ["VA"] = "Virginia",
                ["WA"] = "Washington",
                ["WV"] = "West Virginia",
                ["WI"] = "Wisconsin",
                ["WY"] = "Wyoming",
            },
            ["CA"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "AB", "Alberta" },
                { "BC", "British Columbia" },
                { "MB", "Manitoba" },
                { "NB", "New Brunswick" },
                { "NL", "Newfoundland and Labrador" },
                { "NS", "Nova Scotia" },
                { "NT", "Northwest Territories" },
                { "NU", "Nunavut" },
                { "ON", "Ontario" },
                { "PE", "Prince Edward Island" },
                { "QC", "Quebec" },
                { "SK", "Saskatchewan" },
                { "YT", "Yukon" },
            },
        };
}
