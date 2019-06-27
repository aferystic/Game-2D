using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum Quality {Quest, Common, Uncommon, Rare, Epic, Legendary };

public static class QualityColor
{
    static string color;
    public static string GetColor(Quality quality)
    {
        switch (quality)
        {
            case Quality.Common:
                color = "#888888";
                break;
            case Quality.Uncommon:
                color = "#009900";
                break;
            case Quality.Rare:
                color = "#0080FF";
                break;
            case Quality.Epic:
                color = "#9933FF";
                break;
            case Quality.Legendary:
                color = "#CC6600";
                break;
            case Quality.Quest:
                color = "#800000";
                break;
        }
        return color;
    }
}
