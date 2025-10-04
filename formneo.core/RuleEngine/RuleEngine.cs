using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Reflection;
using System.Text.Json.Nodes;

namespace formneo.core.RuleEngine
{
    using System.Text.Json;
    using System.Reflection;

    public class RuleBase
    {
        public string Id { get; set; }
    }

    public class Rule : RuleBase
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string ValueSource { get; set; }
        public string Value { get; set; }
    }

    public class RuleGroup : RuleBase
    {
        public string Combinator { get; set; } // "and" / "or"
        public bool Not { get; set; } = false;
        public List<JsonElement> Rules { get; set; } = new List<JsonElement>();
    }

    public static class RuleEvaluator
    {
        public static bool IsDtoValidByRules<T>(T dto, string jsonRules)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Gelen JSON string doğrudan tek bir Rule mı yoksa RuleGroup mu?
                if (jsonRules.Contains("\"field\"") && !jsonRules.Contains("\"rules\""))
                {
                    // Tek bir rule
                    var singleRule = JsonSerializer.Deserialize<Rule>(jsonRules, options);
                    return EvaluateRule(singleRule, dto);
                }
                else
                {
                    // Rule grubu
                    var ruleGroup = JsonSerializer.Deserialize<RuleGroup>(jsonRules, options);
                    return EvaluateGroup(ruleGroup, dto, options);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rule değerlendirme hatası: {ex.Message}");
                return false;
            }
        }

        private static bool EvaluateGroup<T>(RuleGroup group, T dto, JsonSerializerOptions options)
        {
            if (group == null || group.Rules == null || !group.Rules.Any())
                return true;

            var results = new List<bool>();

            foreach (var ruleElement in group.Rules)
            {
                try
                {
                    // JsonElement'ı analiz et
                    if (ruleElement.TryGetProperty("rules", out JsonElement _))
                    {
                        // Bu bir RuleGroup
                        var subGroup = JsonSerializer.Deserialize<RuleGroup>(ruleElement.GetRawText(), options);
                        results.Add(EvaluateGroup(subGroup, dto, options));
                    }
                    else if (ruleElement.TryGetProperty("field", out JsonElement _))
                    {
                        // Bu bir Rule
                        var rule = JsonSerializer.Deserialize<Rule>(ruleElement.GetRawText(), options);
                        results.Add(EvaluateRule(rule, dto));
                    }
                    else
                    {
                        results.Add(false); // Tanımlanamayan element
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Kural değerlendirme hatası: {ex.Message}");
                    results.Add(false);
                }
            }

            bool result = group.Combinator?.ToLower() == "and"
                ? results.All(x => x)
                : results.Any(x => x);

            return group.Not ? !result : result;
        }

        private static bool EvaluateRule<T>(Rule rule, T dto)
        {
            if (rule == null || string.IsNullOrEmpty(rule.Field))
                return false;

            try
            {
                var prop = typeof(T).GetProperty(rule.Field,
                    BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);

                if (prop == null)
                {
                    Console.WriteLine($"Property bulunamadı: {rule.Field}");
                    return false;
                }

                var propValue = prop.GetValue(dto);

                // Null kontrolleri
                if (propValue == null && rule.Value == null)
                    return rule.Operator == "=";

                if (propValue == null || rule.Value == null)
                    return rule.Operator == "!=";

                // Enum kontrolü
                if (prop.PropertyType.IsEnum || (Nullable.GetUnderlyingType(prop.PropertyType)?.IsEnum ?? false))
                {
                    var enumType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (Enum.TryParse(enumType, rule.Value, true, out object enumValue))
                    {
                        return CompareValues(propValue, enumValue, rule.Operator);
                    }
                    return false;
                }

                // Diğer tip karşılaştırmaları
                return CompareValues(propValue, rule.Value, rule.Operator);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kural değerlendirme hatası: {ex.Message}");
                return false;
            }
        }

        private static bool CompareValues(object value1, object value2, string op)
        {
            var str1 = value1?.ToString();
            var str2 = value2?.ToString();

            return op?.ToLower() switch
            {
                "=" => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase),
                "==" => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase),
                "!=" => !string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase),
                "contains" => str1?.Contains(str2 ?? "", StringComparison.OrdinalIgnoreCase) ?? false,
                "startswith" => str1?.StartsWith(str2 ?? "", StringComparison.OrdinalIgnoreCase) ?? false,
                "endswith" => str1?.EndsWith(str2 ?? "", StringComparison.OrdinalIgnoreCase) ?? false,
                _ => false
            };
        }
    }
}
