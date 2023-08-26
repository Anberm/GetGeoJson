using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GetGeoJson;

public static class GeoJson
{
    public static async Task GetAllGeo()
    {
        await Ergodicity("100000");
        Console.WriteLine("完成");
    }

    public static async Task Ergodicity(string adCode, bool hadChildren = true)
    {
        string json = await GetJson(adCode, hadChildren);
        var geo = JsonConvert.DeserializeObject<GeoDto>(json);
        if (geo != null)
        {
            SaveFile(json, adCode);
            foreach (var item in geo.Features)
            {
                if (item.Properties.ChildrenNum > 0)
                {
                    await Ergodicity(item.Properties.AdCode, item.Properties.ChildrenNum > 0);
                }

            }
        }
    }

    public static void SaveFile(string json, string name)
    {
        try
        {
            string path = $"{AppContext.BaseDirectory}/geojson";//获取根目录
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Console.WriteLine($"保存目录{path}成功");
            using (FileStream fs = new FileStream($"{path}/{name}.json", FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))//要保存的文件夹
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(json);

                }
            }
            Console.WriteLine($"保存{name}成功");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"保存{name}失败");

        }
    }


    public static async Task<string> GetJson(string adcode = "100000", bool hadChildren = false)
    {
        using HttpClient client = new HttpClient();
        string result = string.Empty;
        try
        {
            // 发送GET请求并获取响应
            string full = hadChildren ? "_full" : "";
            string url = $"https://geo.datav.aliyun.com/areas_v3/bound/{adcode}{full}.json";
            Console.WriteLine($"请求地址：{url}");
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // 确保响应成功

            // 读取响应内容
            result = await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"请求异常: {ex.Message}");
        }
        return result;

    }
}
