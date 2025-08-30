using Newtonsoft.Json.Linq;

public static class JsonQueryEvaluator
{
    public static bool Evaluate(string json, string query)
    {
        var jObject = JObject.Parse(json);
        var flattened = FlattenJson(jObject);

        // Query'deki .'ları _ ile değiştiriyoruz
        string transformedQuery = query;
        foreach (var key in flattened.Keys)
        {
            if (key.Contains("."))
            {
                string safeKey = key.Replace(".", "_");
                transformedQuery = transformedQuery.Replace(key, safeKey);
            }
        }

        var expr = new NCalc.Expression(transformedQuery);

        foreach (var kvp in flattened)
        {
            string paramKey = kvp.Key.Replace(".", "_");
            expr.Parameters[paramKey] = kvp.Value;
        }

        var result = expr.Evaluate();

        return result is bool b && b;
    }

    // Nested JSON'u düzleştir (ör: user.department => "user.department")
    private static Dictionary<string, object> FlattenJson(JObject jObject, string prefix = "")
    {
        var dict = new Dictionary<string, object>();

        foreach (var prop in jObject.Properties())
        {
            string path = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";

            if (prop.Value.Type == JTokenType.Object)
            {
                var nested = FlattenJson((JObject)prop.Value, path);
                foreach (var kv in nested)
                    dict[kv.Key] = kv.Value;
            }
            else
            {
                object value = prop.Value.Type switch
                {
                    JTokenType.Boolean => prop.Value.ToObject<bool>(),
                    JTokenType.Integer => prop.Value.ToObject<int>(),
                    JTokenType.Float => prop.Value.ToObject<double>(),
                    _ => prop.Value.ToString()
                };
                dict[path] = value;
            }
        }

        return dict;
    }
}
