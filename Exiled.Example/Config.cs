using Exiled.API.Features;
using Exiled.API.Interfaces;
using InventorySystem.Items.Firearms.Attachments;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Exiled.Example;

public sealed class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;

    public bool Debug { get; set; }

    [Description("This is a string config")]
    public string String { get; private set; } = "I'm a string!";

    [Description("This is an int config")]
    public int Int { get; private set; } = 1000;

    [Description("This is a float config")]
    public float Float { get; private set; } = 28.2f;

    [Description("This is a list of strings config")]
    public List<string> StringsList { get; private set; } = new() { "First element", "Second element", "Third element" };

    [Description("This is a list of ints config")]
    public List<int> IntsList { get; private set; } = new() { 1, 2, 3 };

    [Description("This is a dictionary of strings as key and int as value config")]
    public Dictionary<string, int> StringIntDictionary { get; private set; } = new()
    {
        { "First Key", 1 },
        { "Second Key", 2 },
        { "Third Key", 3 },
    };

    [Description("This is a dictionary of strings as key and Dictionary<string, int> as value config")]
    public Dictionary<string, Dictionary<string, int>> NestedDictionaries { get; private set; } = new()
    {
        {
            "First Key", new Dictionary<string, int>()
            {
                { "First Key", 1 },
                { "Second Key", 2 },
                { "Third Key", 3 },
            }
        },
        {
            "Second Key", new Dictionary<string, int>()
            {
                { "First Key", 4 },
                { "Second Key", 5 },
                { "Third Key", 6 },
            }
        },
        {
            "Third Key", new Dictionary<string, int>()
            {
                { "First Key", 7 },
                { "Second Key", 8 },
                { "Third Key", 9 },
            }
        },
    };

    [Description("This is a Vector3 config, the same can be done by using a Vector2 or a Vector4")]
    public Vector3 Vector3 { get; private set; } = new(1.3f, -2.5f, 3);

    [Description("This is a list of AttachmentNameTranslation config")]
    public List<AttachmentName> Attachments { get; private set; } = new()
    {
        AttachmentName.AmmoCounter,
        AttachmentName.DotSight,
        AttachmentName.RifleBody,
        AttachmentName.RecoilReducingStock,
        AttachmentName.StandardMagAP,
    };
}