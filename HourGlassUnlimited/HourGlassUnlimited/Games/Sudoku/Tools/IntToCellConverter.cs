using HourGlassUnlimited.Games.Sudoku.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.Tools
{
    public class IntToCellConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Cell);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                Int64 intValue = (Int64)reader.Value;
                return new Cell { Value = intValue };
            }
            throw new JsonSerializationException("Expected integer for Cell conversion.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var cell = value as Cell;
            if (cell != null)
            {
                writer.WriteValue(cell.Value);
            }
            else
            {
                throw new JsonSerializationException("Expected Cell object.");
            }
        }
    }
}
