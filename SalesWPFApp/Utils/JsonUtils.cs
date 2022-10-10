using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using BusinessObject;

namespace SalesWPFApp.Utils;

public static class JsonUtils
{
    public static readonly string FilePath = "appsettings.json";

    public static Member? GetDefaultMember()
    {
        Member? member;
        try
        {
            var jsonString = File.ReadAllText(FilePath);
            using var doc = JsonDocument.Parse(jsonString);
            member = JsonSerializer.Deserialize<Member>(doc.RootElement.GetProperty("DefaultMember").ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return member;
    }

    public static void SaveDefaultMember(Member member)
    {
        try
        {
            var jsonObject = new JsonObject
            {
                ["DefaultMember"] = JsonSerializer.Serialize(member, new JsonSerializerOptions()
                {
                    WriteIndented = true
                })
            };
            var jsonString = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(FilePath, jsonString, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}